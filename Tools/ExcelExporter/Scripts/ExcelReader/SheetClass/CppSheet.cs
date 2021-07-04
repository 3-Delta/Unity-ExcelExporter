using System;
using System.Collections.Generic;
using System.Text;

namespace ExcelExporter {
    public class CppDynamicSheet : DynamicSheet {

        public CppDynamicSheet() { }
        public CppDynamicSheet(Sheet sheet) : base(sheet) { }
    }

    public class CppDynamicSheetLine : DynamicSheetLine {
    }
}
