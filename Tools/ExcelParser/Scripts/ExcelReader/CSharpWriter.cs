using System;
using System.Collections.Generic;
using System.Text;

namespace ExcelParser {
    public class DynamicSheetOfCSharp : DynamicSheet {
        public override string bodyFormat { get; }
    }

    public class DynamicSheetLineOfCSharp : DynamicSheetLine {
        public override DynamicSheetLine GenerateBody(string sheetName, List<Field> fields, int alignmentLevel = 0) {
            this.stringBuilder.Clear();

            string trim = new string(' ', alignmentLevel * 4);

            this.stringBuilder.Append(trim);
            this.stringBuilder.AppendLine(ClassSerializable);
            this.stringBuilder.Append(trim);
            this.stringBuilder.AppendFormat(ClassTitleBegin, sheetName);
            this.stringBuilder.Append(BeginBracket);
            for (int i = 0, length = fields.Count; i < length; ++i) {
                this.stringBuilder.AppendLine();
                this.stringBuilder.Append(trim);
                Field field = fields[i];
                string realType = field.realType;
                if (field.IsArray) {
                    if (field.isTypeArray) {
                        this.stringBuilder.AppendFormat(Class2DArrayField, realType, field.name);
                    } else {
                        this.stringBuilder.AppendFormat(Class1DArrayField, realType, field.name);
                    }
                } else {
                    this.stringBuilder.AppendFormat(ClassSingleField, realType, field.name);
                }
            }
            this.stringBuilder.AppendLine();
            this.stringBuilder.Append(trim);
            this.stringBuilder.Append(EndBracket);
            this.stringBuilder.AppendLine();

            return this;
        }
    }

    public class CSharpWriter {

    }
}
