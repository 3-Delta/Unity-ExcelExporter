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
        None, // 全部不导出
        Client, // 导出client
        Server, // 导出server
        ToEnum, // 导出枚举
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
        public const string ARRAY_EXT = "_array";
        public const string ENUM_EXT = "_enum";
        public const string FORM_PRE = "Form";

        public static readonly List<string> BuiltinTypes = new List<string>() {
            "byte", "sbyte", "short", "ushort", "int", "uint", "long", "ulong",
            "bool", // 多个bool会使用int/byte/short等进行包装，因为一个bool其实占据一个byte的内存容量
            "float", // 其实是千分比的uint数值，为了帧同步
            "string"
        };

        public static bool IsBuiltin(string type) {
            return BuiltinTypes.Contains(type);
        }

        public static bool IsEnum(string type) {
            return type.EndsWith(ENUM_EXT);
        }

        public static bool IsCellArray(string type) {
            return type.EndsWith(ARRAY_EXT);
        }

        public static string GetType(string fieldName, out bool isCellArray, out bool isEnum) {
            isCellArray = IsCellArray(fieldName);
            string type = fieldName;
            if (isCellArray) {
                type = fieldName.Substring(0, fieldName.Length - ARRAY_EXT.Length);
            }

            isEnum = IsEnum(type);
            if (isEnum) {
                type = fieldName.Substring(0, fieldName.Length - ENUM_EXT.Length);
            }
            return type;
        }
        #endregion

        public string name; // 例如：id, strDesc
        public string type; // 例如： int, int_array

        public bool isTypeArray {
            get { return IsCellArray(type); }
        }

        public string realType {
            get { return GetType(type, out bool isCellArray, out bool isEnum); }
        }

        public int beginIndex;
        public int endIndex;

        public EFieldIniType iniType = EFieldIniType.None;

        public bool IsArray {
            get { return endIndex - beginIndex >= 1; }
        }

        public bool IsBuiltinType {
            get { return IsBuiltin(realType); }
        }

        public bool IsFormType {
            get {
                // 所有sheet的名字必须Form开头
                return realType.StartsWith(FORM_PRE);
            }
        }
        public bool IsFloatType {
            get {
                return realType.Equals("float", StringComparison.InvariantCultureIgnoreCase);
            }
        }

        public bool IsEnumType {
            get {
                GetType(type, out bool isCellArray, out bool isEnum);
                return isEnum;
            }
        }
    }

    public class Sheet {
        public readonly string filePath;
        public readonly string sheetName;
        public readonly EClassExportType classExportType = EClassExportType.CSharp;
        public readonly EPlatformExportType platformExportType = EPlatformExportType.Client;
        public readonly EDataExportType dataExportType = EDataExportType.Binary;

        public readonly StringBuilder loger = new StringBuilder();

        public Field id {
            get { return fields[0]; }
        }

        public int idEnumIndex {
            get {
                int index = -1;
                HasField("idEnumName", "string", out index);
                return index;
            }
        }

        public int enableIndex {
            get {
                int index = -1;
                HasField("isEnable", "bool", out index);
                return index;
            }
        }

        public List<Field> FinalFields {
            get {
                List<Field> list = new List<Field>(fields);
                int index = enableIndex;
                if (index != -1) {
                    list.RemoveAt(index);
                }

                index = idEnumIndex;
                if ((id.iniType & EFieldIniType.ToEnum) == EFieldIniType.ToEnum) {
                    list.RemoveAt(index);
                }

                return list;
            }
        }

        // 是否存在非bool数组的bool字段
        public bool HasBool {
            get { return TryGetFields("bool", false, out List<Field> list); }
        }

        public readonly IList<Field> fields = new List<Field>();

        public readonly IList<string> idNames = new List<string>();
        public readonly IList<string> idValues = new List<string>();

        public Sheet(string filePath, string sheetName, EPlatformExportType platformExportType, EClassExportType classExportType, EDataExportType dataExportType) {
            this.filePath = filePath;
            this.sheetName = sheetName;
            this.platformExportType = platformExportType;
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
            var list = fields;
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
            var list = inFinalFields ? FinalFields : fields;
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

        public void Read(ISheet sheet) {
            ReadHeader(sheet);
            CorrectHeader();
            CollectIdEnum(sheet);
            foreach (var name in Enum.GetNames(typeof(EClassExportType))) {
                bool rlt = Enum.TryParse<EClassExportType>(name, out EClassExportType exportType);
                bool isZero = exportType == 0;
                if (rlt && !isZero && (classExportType & exportType) == exportType) {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(GenerateDynamicSheet(name));

                    sb.AppendLine();
                    sb.Append(GenerateDynamicEnum(name));

                    File.WriteAllText(Environment.CurrentDirectory + "1.txt", sb.ToString());
                }
            }

            foreach (var name in Enum.GetNames(typeof(EDataExportType))) {
                bool rlt = Enum.TryParse<EDataExportType>(name, out EDataExportType exportType);
                bool isZero = exportType == 0;
                if (rlt && !isZero && (dataExportType & exportType) == exportType) {
                    //this.ReadLine(sheet, name);
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
                        Loger.Print(string.Format("FilePath:{0} sheetName:{1} row:{2} col:{3} 不是id", filePath, sheet.SheetName, (int)ERowType.FieldName, 0));
                    }

                    string preFieldName = null;
                    int beginIndex = 0;
                    for (int i = 0, length = cellValues.Length; i < length; ++i) {
                        if (cellValues[i] == preFieldName) {
                            fields[fields.Count - 1].endIndex = i;
                        }
                        else {
                            Field field = new Field();
                            field.name = cellValues[i];
                            field.beginIndex = beginIndex;
                            field.endIndex = i;

                            fields.Add(field);

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
                    for (int i = 0, length = fields.Count; i < length; ++i) {
                        int cellIndex = fields[i].beginIndex;
                        ICell cell = row.GetCell(cellIndex);
                        if (cell != null) {
                            fields[i].type = GetCellValue(cell);
                        }
                        else {
                            // 填写了name, 但是没有填写type
                            Loger.Print(string.Format("FilePath:{0} sheetName:{1} row:{2} col:{3} 类型有误", filePath, sheet.SheetName, (int)ERowType.FieldType, cellIndex));
                        }
                    }
                }

                ReadFieldType();
                #endregion

                #region 字段ini
                void ReadFieldIni() {
                    IRow row = sheet.GetRow((int)ERowType.Ini);
                    for (int i = 0, length = fields.Count; i < length; ++i) {
                        int cellIndex = fields[i].beginIndex;
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

                        fields[i].iniType = iniType;
                    }
                }

                ReadFieldIni();
                #endregion

                #region 字段继承
                #endregion

                return true;
            }

            Loger.Print(string.Format("FilePath:{0} sheetName:{1} 行数至少要{2}行", filePath, sheet.SheetName, (int)ERowType.FieldType));
            return false;
        }

        private void CorrectHeader() {
            if ((id.iniType & EFieldIniType.ToEnum) == EFieldIniType.ToEnum) {
                // id导出枚举
                if (idEnumIndex == -1) {
                    // 如果id标识为导出为枚举，但是没有类型为string的idEnumName字段，那么就矫正id不导出枚举
                    id.iniType &= (~EFieldIniType.ToEnum);
                }
            }
        }

        private void CollectIdEnum(ISheet sheet) {
            idNames.Clear();
            idValues.Clear();
            if ((id.iniType & EFieldIniType.ToEnum) == EFieldIniType.ToEnum) {
                for (int i = (int)ERowType.FieldType + 1, length = sheet.LastRowNum + 1; i < length; ++i) {
                    IRow row = sheet.GetRow(i);
                    int idColIndex = id.beginIndex;
                    int enumColIndex = idEnumIndex;
                    ICell idCell = row.GetCell(idColIndex);
                    ICell enumCell = row.GetCell(enumColIndex);
                    if (idCell != null && enumCell != null) {
                        string enumCellValues = GetCellValue(enumCell);
                        idNames.Add(enumCellValues);

                        string idCellValues = GetCellValue(idCell);
                        idValues.Add(idCellValues);
                    }
                    else {
                        Loger.Print(string.Format("FilePath:{0} sheetName:{1} 行{2} 列{3} 没有填写正确的信息", filePath, sheet.SheetName, i + 1, enumColIndex + 1));
                    }
                }
            }
        }

        private string GenerateDynamicSheet(string classExportName, int alignmentLevel = 0) {
            StringBuilder sb = new StringBuilder();

            string typeName = typeof(DynamicSheetUsing).Namespace + "." + classExportName + nameof(DynamicSheetUsing);
            Type type = Type.GetType(typeName);
            DynamicSheetUsing dynamicSheetUsing = Activator.CreateInstance(type) as DynamicSheetUsing;
            string content = dynamicSheetUsing.Generate();
            sb.Append(content);

            typeName = typeof(DynamicSheet).Namespace + "." + classExportName + nameof(DynamicSheet);
            type = Type.GetType(typeName);
            DynamicSheet dynamicSheet = Activator.CreateInstance(type) as DynamicSheet;
            dynamicSheet.Reset(this);
            content = dynamicSheet.GenerateClass(alignmentLevel);
            sb.Append(content);

            typeName = typeof(DynamicSheetLine).Namespace + "." + classExportName + nameof(DynamicSheetLine);
            type = Type.GetType(typeName);
            DynamicSheetLine dynamicSheetLine = Activator.CreateInstance(type) as DynamicSheetLine;
            dynamicSheetLine.Reset(this);
            content = dynamicSheetLine.Generate(alignmentLevel);
            sb.Append(content);

            return sb.ToString();
        }

        private string GenerateDynamicEnum(string classExportName, int alignmentLevel = 0) {
            string typeName = typeof(DynamicSheetEnum).Namespace + "." + classExportName + nameof(DynamicSheetEnum);
            Type type = Type.GetType(typeName);
            DynamicSheetEnum dynamicSheetEnum = Activator.CreateInstance(type) as DynamicSheetEnum;
            dynamicSheetEnum.Reset(this);
            return dynamicSheetEnum.Generate(idNames, idValues, alignmentLevel);
        }

        private void ReadLine(ISheet sheet) {
            //foreach (var name in Enum.GetNames(typeof(EClassExportType))) { }

            //for (int i = (int)ERowType.FieldType, length = sheet.LastRowNum; i < length; ++i) {
            //    IRow row = sheet.GetRow(i);
            //    for (int ii = 0, lengthII = fields.Count; ii < lengthII; ++ii) {
            //        Field field = fields[ii];
            //        for (int iii = field.beginIndex, lengthIII = field.endIndex + 1; iii < lengthIII; ++iii) {
            //            ICell cell = row.GetCell(iii);
            //            string cellValues = GetCellValue(cell);
            //        }
            //    }
            //}
        }
    }

    public class SheetProcesser {
        [System.Flags]
        public enum EPlatformExportType {
            None = 0,
            Client = 1,
            Server = 2,
        }

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

        public static void ReadSheets(IList<(string fileFullPath, string sheetName)> list, EPlatformExportType platformExportType = EPlatformExportType.Client, EClassExportType classExportType = EClassExportType.CSharp, EDataExportType dataExportType = EDataExportType.Binary) {
            for (int i = 0, length = list.Count; i < length; ++i) {
                ReadSheet(list[i].fileFullPath, list[i].sheetName, platformExportType, classExportType, dataExportType);
            }
        }

        public static void ReadSheet(string fileFullPath, string sheetName, EPlatformExportType platformExportType = EPlatformExportType.Client, EClassExportType classExportType = EClassExportType.CSharp, EDataExportType dataExportType = EDataExportType.Binary) {
            string ext = Path.GetExtension(fileFullPath).ToLower();
            // FileShare.Read会存在：已经打开的文件不能被本进程访问的问题
            using (FileStream fileStream = new FileStream(fileFullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
                IWorkbook workbook;
                if (ext == ".xlsx") {
                    workbook = new XSSFWorkbook(fileStream);
                }
                else if (ext == ".xls") {
                    workbook = new HSSFWorkbook(fileStream);
                }
                else {
                    workbook = null;
                }

                if (workbook != null) {
                    ISheet sheet = workbook.GetSheet(sheetName);
                    if (sheet != null) {
                        Sheet sh = new Sheet(fileFullPath, sheetName, platformExportType, classExportType, dataExportType);
                        sh.Read(sheet);
                    }
                    else {
                        Loger.Print(string.Format("{0}不存在名为{1}的sheet", fileFullPath, sheetName));
                    }
                }
                else {
                    Loger.Print(string.Format("{0}不是excel文件t", fileFullPath));
                }
            }
        }
    }
}
