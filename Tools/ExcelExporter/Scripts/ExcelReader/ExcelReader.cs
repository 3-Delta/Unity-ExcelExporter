using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

using static ExcelExporter.SheetProcesser;

namespace ExcelExporter {
    [System.Flags]
    public enum EFieldIniType {
        None,  // 全部不导出
        Client,  // 导出client
        Server,   // 导出server
        ToEnum,  // 导出枚举
    }

    public enum ERowType {
        Note = 0, // 注释
        Ini = 1, // 导出配置
        FieldName = 2, // 字段名
        FieldType = 3, // 类型
    }

    // 表格字段
    public class Field {
        #region 辅助函数
        public const string ArrayExt = "_array";
        public static readonly List<string> BuiltinTypes = new List<string>() {
            "byte", "sbyte", "short", "ushort", "int", "uint", "long", "ulong",
            "bool", // 多个bool会使用int/byte/short等进行包装，因为一个bool其实占据一个byte的内存容量
            "float", // 其实是千分比的uint数值，为了帧同步
            "string"
        };

        public static bool IsBuiltin(string type) {
            return BuiltinTypes.Contains(type);
        }
        public static bool IsCellArray(string type) {
            return type.EndsWith(ArrayExt);
        }
        public static string GetType(string fieldName, out bool isCellArray) {
            isCellArray = IsCellArray(fieldName);
            if (isCellArray) {
                return fieldName.Substring(0, fieldName.Length - ArrayExt.Length);
            } else {
                return fieldName;
            }
        }
        #endregion

        public string name; // 例如：id, strDesc
        public string type; // 例如： int, int_array

        public bool isTypeArray {
            get {
                return IsCellArray(this.type);
            }
        }
        public string realType {
            get {
                return GetType(this.type, out bool isCellArray);
            }
        }

        public int beginIndex;
        public int endIndex;

        public EFieldIniType iniType = EFieldIniType.None;

        public bool IsArray {
            get {
                return this.endIndex - this.beginIndex >= 1;
            }
        }

        public bool IsBuiltinType {
            get {
                return IsBuiltin(this.realType);
            }
        }

        public bool IsFormType {
            get {
                // 所有sheet的名字必须Form开头
                return realType.StartsWith("Form");
            }
        }
    }

    public class Sheet {
        public readonly string filePath;
        public readonly string sheetName;
        public readonly EClassExportType classExportType = EClassExportType.CSharp;
        public readonly EDataExportType dataExportType = EDataExportType.Binary;

        public readonly StringBuilder loger = new StringBuilder();

        public Field id {
            get {
                return this.fields[0];
            }
        }
        public int idEnumIndex {
            get {
                int index = -1;
                this.HasField("idEnumName", "string", out index);
                return index;
            }
        }
        public int enableIndex {
            get {
                int index = -1;
                this.HasField("isEnable", "bool", out index);
                return index;
            }
        }

        public List<Field> FinalFields {
            get {
                List<Field> list = new List<Field>(this.fields);
                int index = this.enableIndex;
                if (index != -1) {
                    list.RemoveAt(index);
                }

                index = this.idEnumIndex;
                if ((this.id.iniType & EFieldIniType.ToEnum) == EFieldIniType.ToEnum) {
                    list.RemoveAt(index);
                }
                return list;
            }
        }

        // 是否存在非bool数组的bool字段
        public bool HasBool {
            get {
                return this.TryGetFields("bool", false, out List<Field> list);
            }
        }
        public readonly List<Field> fields = new List<Field>();

        public Sheet(string filePath, string sheetName, EClassExportType classExportType, EDataExportType dataExportType) {
            this.filePath = filePath;
            this.sheetName = sheetName;
            this.classExportType = classExportType;
            this.dataExportType = dataExportType;
        }

        public static string GetCellValue(ICell cell) {
            string value = null;
            if (cell != null) {
                switch (cell.CellType) {
                    case CellType.Error:
                    case CellType.Blank:
                        value = null;
                        break;
                    case CellType.Boolean:
                        value = cell.BooleanCellValue ? "1" : "0";
                        break;
                    case CellType.Numeric:
                        value = cell.NumericCellValue.ToString();
                        break;
                    case CellType.String:
                    default:
                        value = cell.StringCellValue;
                        break;
                }
            }
            return value;
        }

        public bool HasField(string fieldName, string fieldType, out int index) {
            index = -1;
            var list = this.fields;
            for (int i = 0, length = list.Count; i < length; ++i) {
                if (list[i].name.Equals(fieldName, StringComparison.OrdinalIgnoreCase) &&
                    list[i].type.Equals(fieldType, StringComparison.OrdinalIgnoreCase)) {
                    index = i;
                    return true;
                }
            }
            return false;
        }
        public bool TryGetFields(string fieldType, bool inFinalFields, out List<Field> outFields) {
            var list = inFinalFields ? this.FinalFields : this.fields;
            outFields = null;
            bool has = false;
            for (int i = 0, length = list.Count; i < length; ++i) {
                if (list[i].type.Equals(fieldType, StringComparison.OrdinalIgnoreCase)) {
                    has = true;

                    if (outFields == null) {
                        outFields = new List<Field>();
                    }
                    outFields.Add(list[i]);
                }
            }
            return has;
        }

        public void Read() {
            string ext = Path.GetExtension(this.filePath).ToLower();
            // FileShare.Read会存在：已经打开的文件不能被本进程访问的问题
            using (FileStream fileStream = new FileStream(this.filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
                IWorkbook workbook;
                if (ext == ".xlsx") {
                    workbook = new XSSFWorkbook(fileStream);
                } else if (ext == ".xls") {
                    workbook = new HSSFWorkbook(fileStream);
                } else {
                    workbook = null;
                }

                if (workbook != null) {
                    ISheet sheet = workbook.GetSheet(this.sheetName);
                    if (sheet != null) {
                        this.ReadHeader(sheet);
                        this.CorrectHeader();
                        this.GenerateDynamicSheet();
                        this.ReadLine(sheet);
                    } else {
                        Loger.Print(string.Format("{0}不存在名为{1}的sheet", this.filePath, this.sheetName));
                    }
                } else {
                    Loger.Print(string.Format("{0}不是excel文件t", this.filePath));
                }
            }
        }

        private bool ReadHeader(ISheet sheet) {
            if (sheet != null && sheet.LastRowNum >= (int)ERowType.FieldType) {
                #region 字段名
                void ReadFieldName() {
                    IRow row = sheet.GetRow((int)ERowType.FieldName);
                    int colCount = row.LastCellNum;
                    string[] cellValues = new string[colCount];

                    for (int i = 0, length = colCount; i < length; ++i) {
                        ICell cell = row.GetCell(i);
                        cellValues[i] = GetCellValue(cell);
                    }

                    if (cellValues[0] != "id") {
                        Loger.Print(string.Format("FilePath:{0} sheetName:{1} row:{2} col:{3} 不是id", this.filePath, sheet.SheetName, (int)ERowType.FieldName, 0));
                    }

                    string preFieldName = null;
                    int beginIndex = 0;
                    for (int i = 0, length = cellValues.Length; i < length; ++i) {
                        if (cellValues[i] == preFieldName) {
                            this.fields[this.fields.Count - 1].endIndex = i;
                        } else {
                            Field field = new Field();
                            field.name = cellValues[i];
                            field.beginIndex = beginIndex;
                            field.endIndex = i;

                            this.fields.Add(field);

                            preFieldName = cellValues[i];
                        }
                        beginIndex = i + 1;
                    }
                }

                ReadFieldName();
                #endregion

                #region 字段类型
                void ReadFieldType() {
                    IRow row = sheet.GetRow((int)ERowType.FieldType);
                    for (int i = 0, length = this.fields.Count; i < length; ++i) {
                        int cellIndex = this.fields[i].beginIndex;
                        ICell cell = row.GetCell(cellIndex);
                        if (cell != null) {
                            this.fields[i].type = GetCellValue(cell);
                        } else {
                            // 填写了name, 但是没有填写type
                            Loger.Print(string.Format("FilePath:{0} sheetName:{1} row:{2} col:{3} 类型有误", this.filePath, sheet.SheetName, (int)ERowType.FieldType, cellIndex));
                        }
                    }
                }

                ReadFieldType();
                #endregion

                #region 字段ini
                void ReadFieldIni() {
                    IRow row = sheet.GetRow((int)ERowType.Ini);
                    for (int i = 0, length = this.fields.Count; i < length; ++i) {
                        int cellIndex = this.fields[i].beginIndex;
                        ICell cell = row.GetCell(cellIndex);

                        EFieldIniType iniType = EFieldIniType.None;
                        if (cell != null) {
                            string v = GetCellValue(cell);
                            if (v.Contains("e")) {
                                iniType |= EFieldIniType.ToEnum;
                            }
                            if (v.Contains("c")) {
                                iniType |= EFieldIniType.Client;
                            }
                            if (v.Contains("s")) {
                                iniType |= EFieldIniType.Server;
                            }
                        }
                        this.fields[i].iniType = iniType;
                    }
                }

                ReadFieldIni();

                #endregion

                #region 字段继承
                #endregion

                return true;
            }

            Loger.Print(string.Format("FilePath:{0} sheetName:{1} 行数至少要{2}行", this.filePath, sheet.SheetName, (int)ERowType.FieldType));
            return false;
        }

        private void CorrectHeader() {
            if ((this.id.iniType & EFieldIniType.ToEnum) == EFieldIniType.ToEnum) {
                // id导出枚举
                if (this.idEnumIndex == -1) {
                    // 如果id标识为导出为枚举，但是没有类型为string的idEnumName字段，那么就矫正id不导出枚举
                    this.id.iniType &= (~EFieldIniType.ToEnum);
                }
            }
        }

        private void GenerateDynamicSheet() {
            foreach (var name in Enum.GetNames(typeof(EClassExportType))) {
                bool rlt = Enum.TryParse<EClassExportType>(name, out EClassExportType exportType);
                bool isZero = exportType == 0;
                if (rlt && !isZero && (this.classExportType & exportType) == exportType) {
                    string typeName = typeof(DynamicSheet).Namespace + "." + name + nameof(DynamicSheet);
                    Type type = Type.GetType(typeName);
                    DynamicSheet dynamicSheet = Activator.CreateInstance(type) as DynamicSheet;
                    dynamicSheet.Reset(this, null, null);
                    dynamicSheet.GenerateClass();

                    typeName = typeof(DynamicSheetLine).Namespace + "." + name + nameof(DynamicSheetLine);
                    type = Type.GetType(typeName);
                    DynamicSheetLine dynamicSheetLine = Activator.CreateInstance(type) as DynamicSheetLine;
                    dynamicSheetLine.Reset(this);
                    dynamicSheetLine.GenerateClass();

                    File.WriteAllText(Environment.CurrentDirectory + "1.txt", dynamicSheet.ToString() + dynamicSheetLine.ToString());
                }
            }
        }

        private void ReadLine(ISheet sheet) {
            for (int i = (int)ERowType.FieldType, length = sheet.LastRowNum; i < length; ++i) {
                IRow row = sheet.GetRow(i);
                for (int ii = 0, lengthII = this.fields.Count; ii < lengthII; ++ii) {
                    Field field = this.fields[ii];
                    for (int iii = field.beginIndex, lengthIII = field.endIndex + 1; iii < lengthIII; ++iii) {
                        ICell cell = row.GetCell(iii);
                        string v = GetCellValue(cell);
                    }
                }
                int colCount = row.LastCellNum;
                string[] cellValues = new string[colCount];
            }
        }
    }

    public class SheetProcesser {
        [System.Flags]
        public enum EClassExportType {
            None = 0,
            CSharp = 1,
            Cpp = 2,
        }

        [System.Flags]
        public enum EDataExportType {
            None = 0,
            Json = 1,
            Lua = 2,
            Binary = 4,
            Sqlite = 8,
            CSharp = 16,
            Cpp = 32,
        }

        public static void ReadSheets(IList<(string fileFullPath, string sheetName)> list, EClassExportType classExportType = EClassExportType.CSharp, EDataExportType dataExportType = EDataExportType.Binary) {
            for (int i = 0, length = list.Count; i < length; ++i) {
                ReadSheet(list[i].fileFullPath, list[i].sheetName, classExportType, dataExportType);
            }
        }

        public static void ReadSheet(string fileFullPath, string sheetName, EClassExportType classExportType = EClassExportType.CSharp, EDataExportType dataExportType = EDataExportType.Binary) {
            Sheet sh = new Sheet(fileFullPath, sheetName, classExportType, dataExportType);
            sh.Read();
        }
    }
}
