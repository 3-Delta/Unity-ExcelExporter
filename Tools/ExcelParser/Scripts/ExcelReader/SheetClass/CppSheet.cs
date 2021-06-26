using System;
using System.Collections.Generic;
using System.Text;

namespace ExcelParser {
    public class CppDynamicSheet : DynamicSheet {
        public override string bodyFormat { get; }
    }

    public class CppDynamicSheetLine : DynamicSheetLine {
        public override string ClassSerializable { get; }
        public override string ClassTitle { get; }
        public override string ClassInheritTitleBegin { get; }
        public override string ClassSingleField { get; }
        public override string Class1DArrayField { get; }
        public override string Class2DArrayField { get; }
        public override string ClassCtorTitle { get; }
        public override string ClassFieldAssignment { get; }

        public override DynamicSheetLine GenerateBody(string sheetName, List<Field> fields, int alignmentLevel = 0) {
            throw new NotImplementedException();
        }
    }
}
