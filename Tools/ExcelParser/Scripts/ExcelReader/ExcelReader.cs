using System;
using System.Collections.Generic;
using System.Text;

namespace ExcelParser {
    public class ExcelHelper {
        public static readonly string[] builtinTypeNames = new string[] { "int", "uint" };
    }

    public class Sheet { 
    }

    // 表格字段
    public class Field {
        public string name;
        public string type;

        public bool IsBuiltin { get; }
        public bool IsArray { get; }
    }

    public class ExcelReader {

    }
}
