using System.Collections.Generic;
using System.IO;

using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace ExcelParser {
    public class ExcelHelper {
        public static readonly string[] builtinTypeNames = new string[] { "int", "uint" };
    }

    [System.Flags]
    public enum EFieldIniType {
        None,  // 全部不导出
        Client,  // 导出client
        Server,   // 导出server
        ToEnum,  // 导出枚举
    }

    // 表格字段
    public class Field {
        public string name;
        public string type;

        public int beginIndex;
        public int endIndex;

        public EFieldIniType iniType = EFieldIniType.None;

        public bool IsArray {
            get {
                return this.endIndex - this.beginIndex >= 1;
            }
        }
    }

    public class SheetReader {
        public enum ERowType {
            Note = 1, // 注释
            Ini = 2, // 导出配置
            FieldName = 3, // 字段名
            FieldType = 4, // 类型
        }

        public static void ReadSheets(IList<(string fileFullPath, string sheetName)> list) {
            for (int i = 0, length = list.Count; i < length; ++i) {
                ReadSheet(list[i].fileFullPath, list[i].sheetName);
            }
        }

        public static void ReadSheet(string fileFullPath, string sheetName) {
            string fileExt = Path.GetExtension(fileFullPath).ToLower();
            using (FileStream fileStream = new FileStream(fileFullPath, FileMode.Open, FileAccess.Read)) {
                IWorkbook workbook;

                if (fileExt == ".xlsx") {
                    workbook = new XSSFWorkbook(fileStream);
                } else if (fileExt == ".xls") {
                    workbook = new HSSFWorkbook(fileStream);
                } else {
                    workbook = null;
                }

                ISheet sheet = workbook.GetSheet(sheetName);
                if (sheet != null) {
                    bool rlt = ReadSheetHeader(fileFullPath, sheet, out List<Field> fieldList);
                    ReadSheetLine(fileFullPath, sheet, fieldList);
                } else {
                    Loger.Print(string.Format("{0}文件 不存在名为{1}的sheet", fileFullPath, sheetName));
                }
            }
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

        private static bool ReadSheetHeader(string fileFullPath, ISheet sheet, out List<Field> fieldList) {
            fieldList = new List<Field>();
            if (sheet != null && sheet.LastRowNum >= (int)ERowType.FieldType) {
                #region 字段名
                IRow row = sheet.GetRow((int)ERowType.FieldName);
                int colCount = row.LastCellNum;
                string[] cellValues = new string[colCount];

                for (int i = 0, length = colCount; i < length; ++i) {
                    ICell cell = row.GetCell(i);
                    cellValues[i] = GetCellValue(cell);
                }

                if (cellValues[0] != "id") {
                    Loger.Print(string.Format("FileName:{0} sheetName{1} row{2} col{3} 不是id", fileFullPath, sheet.SheetName, (int)ERowType.FieldName, 0));
                }

                string preFieldName = null;
                int beginIndex = 0;
                for (int i = 0, length = cellValues.Length; i < length; ++i) {
                    if (cellValues[i] == preFieldName) {
                        fieldList[fieldList.Count - 1].endIndex = i;
                    } else {
                        Field field = new Field();
                        field.name = cellValues[i];
                        field.beginIndex = beginIndex;
                        field.endIndex = i;

                        fieldList.Add(field);

                        preFieldName = cellValues[i];
                    }
                    beginIndex = i + 1;
                }
                #endregion

                #region 字段类型
                row = sheet.GetRow((int)ERowType.FieldType);
                for (int i = 0, length = fieldList.Count; i < length; ++i) {
                    int cellIndex = fieldList[i].beginIndex;
                    ICell cell = row.GetCell(cellIndex);
                    if (cell != null) {
                        fieldList[i].type = GetCellValue(cell);
                    } else {
                        Loger.Print(string.Format("FileName:{0} sheetName{1} row{2} col{3} 类型有误", fileFullPath, sheet.SheetName, (int)ERowType.FieldType, cellIndex));
                    }
                }
                #endregion

                #region 字段ini
                row = sheet.GetRow((int)ERowType.Ini);
                for (int i = 0, length = fieldList.Count; i < length; ++i) {
                    int cellIndex = fieldList[i].beginIndex;
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
                    fieldList[i].iniType = iniType;
                }
                #endregion

                #region 字段继承
                #endregion

                return true;
            }

            Loger.Print(string.Format("FileName:{0} sheetName{1} 行数至少要{2}行", fileFullPath, sheet.SheetName, (int)ERowType.FieldType));
            return false;
        }

        private static void ReadSheetLine(string fileFullPath, ISheet sheet, List<Field> fieldList) {
            for (int i = (int)ERowType.FieldType, length = sheet.LastRowNum; i < length; ++i) {
                IRow row = sheet.GetRow(i);
                for (int ii = 0, lengthII = fieldList.Count; ii < lengthII; ++ii) {
                    Field field = fieldList[ii];
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
}
