using System;
using System.Collections.Generic;
using System.Text;

namespace ExcelParser {
    public class CSharpDynamicSheet : DynamicSheet {
        public override string bodyFormat { get; }
    }

    public class CSharpDynamicSheetLine : DynamicSheetLine {
        public override string ClassSerializable { get { return "System.Serializable"; } }
        public override string ClassTitle { get { return "public partial class {0} "; } }

        public override string ClassInheritTitleBegin { get { return "public class {0} /*: FormBase<{1}>*/{"; } }
        public override string ClassSingleField { get { return "    public readonly {0} {1};"; } }
        public override string Class1DArrayField { get { return "    public readonly List<{0}> {1};"; } }
        // 为什么不用 【】数组呢？因为表达不出复杂二维数组的结构
        public override string Class2DArrayField { get { return "    public readonly List<List<{0}>> {1};"; } }

        public override string ClassCtorTitle { get { return "public {0}(DataReader reader)"; } }
        public override string ClassFieldAssignment { get { return "    {0} = reader.Read();"; } }

        public override DynamicSheetLine GenerateBody(string sheetName, List<Field> fields, int alignmentLevel = 0) {
            this.stringBuilder.Clear();

            string trim = new string(' ', alignmentLevel * 4);

            this.stringBuilder.Append(trim);
            this.stringBuilder.AppendLine(ClassSerializable);
            this.stringBuilder.Append(trim);
            this.stringBuilder.AppendFormat(ClassTitle, sheetName);
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
}
