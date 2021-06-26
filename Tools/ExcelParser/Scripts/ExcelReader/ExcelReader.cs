using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace ExcelParser {
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
    }

    public class Sheet {
        public readonly string filePath;
        public readonly string sheetName;

        public readonly StringBuilder loger = new StringBuilder();
        public readonly DynamicSheetLine dynamicSheet = new CSharpDynamicSheetLine();

        public Field id {
            get {
                return this.fields[0];
            }
        }
        public readonly List<Field> fields = new List<Field>();

        public Sheet(string filePath, string sheetName) {
            this.filePath = filePath;
            this.sheetName = sheetName;
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
            for (int i = 0, length = this.fields.Count; i < length; ++i) {
                if (this.fields[i].name.Equals(fieldName, StringComparison.OrdinalIgnoreCase)) {
                    index = i;
                    return true;
                }
            }
            return false;
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
                if (this.HasField("idEnumName", "string", out int index)) {
                    // 如果id标识为导出为枚举，但是没有类型为string的idEnumName字段，那么就矫正id不导出枚举
                } else {
                    this.id.iniType &= (~EFieldIniType.ToEnum);
                }
            }
        }

        private void GenerateDynamicSheet() {
            List<Field> tmp = new List<Field>(this.fields);
            int index;
            if (this.HasField("isEnable", "bool", out index)) {
                tmp.RemoveAt(index);
            }
            if (this.HasField("idEnumName", "string", out index)) {
                tmp.RemoveAt(index);
            }
            this.dynamicSheet.GenerateBody(this.sheetName, tmp);
            //this.dynamicSheet.ToString();
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

    public class DynamicSheet {
        public virtual string bodyFormat { get; }

        #region Json
        public virtual void FromJson() { }
        public virtual void ToJson() { }
        #endregion

        #region Lua
        public virtual void FromLua() { }
        public virtual void ToLua() { }
        #endregion

        #region Binray
        public virtual void FromBinary() { }
        public virtual void ToBinary() { }
        #endregion

        #region Sqlite
        public virtual void FromSqlite() { }
        public virtual void ToSqlite() { }
        #endregion

        #region CSharp
        public virtual void FromCSharp() { }
        public virtual void ToCSharp() { }
        #endregion

        #region Cpp
        public virtual void FromCpp() { }
        public virtual void ToCpp() { }
        #endregion
    }

    public abstract class DynamicSheetLine {
        public const string BeginBracket = "{";
        public const string EndBracket = "}";

        public abstract string ClassSerializable { get; }
        public abstract string ClassTitle { get; }

        public abstract string ClassInheritTitleBegin { get; }
        public abstract string ClassSingleField { get; }
        public abstract string Class1DArrayField { get; }
        public abstract string Class2DArrayField { get; }
        public abstract string ClassCtorTitle { get; }
        public abstract string ClassFieldAssignment { get; }

        protected readonly StringBuilder stringBuilder = new StringBuilder();

        public string body {
            get {
                return stringBuilder.ToString();
            }
        }

        public abstract DynamicSheetLine GenerateBody(string sheetName, List<Field> fields, int alignmentLevel = 0);

        public virtual void Write(IList<string> exportTypes) {
            for (int i = 0, length = exportTypes.Count; i < length; ++i) {
            }
        }

    }

    public class SheetReader {
        public static void ReadSheets(IList<(string fileFullPath, string sheetName)> list) {
            for (int i = 0, length = list.Count; i < length; ++i) {
                ReadSheet(list[i].fileFullPath, list[i].sheetName);
            }
        }

        public static void ReadSheet(string fileFullPath, string sheetName) {
            Sheet sh = new Sheet(fileFullPath, sheetName);
            sh.Read();
        }
    }
}
