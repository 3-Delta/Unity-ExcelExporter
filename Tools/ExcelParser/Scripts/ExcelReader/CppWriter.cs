using System;
using System.Collections.Generic;
using System.Text;

namespace ExcelParser {
    public class DynamicSheetOfCpp : DynamicSheet {
        public override string bodyFormat { get; }
    }

    public class DynamicSheetLineOfCpp : DynamicSheetLine {
        public override DynamicSheetLine GenerateBody(string sheetName, List<Field> fields, int alignmentLevel = 0) {
            throw new NotImplementedException();
        }
    }

    public class CppWriter {

    }
}
