using System;
using System.Collections.Generic;
using System.Text;

namespace ExcelExporter {
    public class CppDynamicSheet : DynamicSheet {

        public CppDynamicSheet() { }
        public CppDynamicSheet(Sheet sheet, string classFileNameNoExt, string dataFileNameNoExt) : base(sheet, classFileNameNoExt, dataFileNameNoExt) { }
    }

    public class CppDynamicSheetLine : DynamicSheetLine {
    }
}
