using System.Collections.Generic;
using System.Text;

namespace ExcelExporter {
    public class ClassHelper {
        public const string BeginBracket = "{";
        public const string EndBracket = "}";
    }

    public abstract class DynamicSheetUsing {
        public virtual string body { get; }

        public string Generate() {
            return body;
        }
    }

    public abstract class DynamicSheet {
        protected Sheet sheet;

        public DynamicSheet() { }

        public DynamicSheet(Sheet sheet) {
            this.sheet = sheet;
        }
        public DynamicSheet Reset(Sheet sheet) {
            this.sheet = sheet;
            return this;
        }

        public virtual string GenerateClass(int alignmentLevel = 0) {
            return null;
        }

        public virtual string GenerateData() {
            return null;
        }
    }

    public abstract class DynamicSheetLine {
        protected Sheet sheet;

        public DynamicSheetLine() { }
        public DynamicSheetLine(Sheet sheet) {
            this.sheet = sheet;
        }

        public DynamicSheetLine Reset(Sheet sheet) {
            this.sheet = sheet;
            return this;
        }

        public virtual string Generate(int alignmentLevel = 0) {
            return null;
        }
    }

    public abstract class DynamicSheetEnum {
        public const string END_OF_THIS =
@"
#Tab#    // EndOfThis";
        protected Sheet sheet;
        public virtual string body { get; }

        public DynamicSheetEnum() { }
        public DynamicSheetEnum(Sheet sheet) {
            this.sheet = sheet;
        }
        public DynamicSheetEnum Reset(Sheet sheet) {
            this.sheet = sheet;
            return this;
        }

        public string Generate(IList<string> idNames, IList<string> idValues, int alignmentLevel = 0) {
            StringBuilder sb = new StringBuilder();

            string trim = new string(' ', alignmentLevel * 4);
            sb.Append(body);
            sb.Replace("#Tab#", trim);
            sb.Replace("#ClsName#", sheet.sheetName);

            for (int i = 0, length = idNames.Count; i < length; ++i) {
                string name = idNames[i];
                string value = idValues[i];
                sb.Replace("// EndOfThis", string.Format(@"{0} = {1},
#Tab#    // EndOfThis", name, value));
            }

            sb.Replace("#Tab#", trim);
            return sb.ToString();
        }
    }
}
