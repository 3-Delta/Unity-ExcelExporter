namespace ExcelExporter {
    public class DynamicSheet {
        protected Sheet sheet;

        public string classFileNameNoExt { get; protected set; }
        public string dataFileNameNoExt { get; protected set; }

        public DynamicSheet() { }
        public DynamicSheet(Sheet sheet, string classFileNameNoExt, string dataFileNameNoExt) {
            this.sheet = sheet;
            this.classFileNameNoExt = classFileNameNoExt;
            this.dataFileNameNoExt = dataFileNameNoExt;
        }
        public void Reset(Sheet sheet, string classFileNameNoExt, string dataFileNameNoExt) {
            this.sheet = sheet;
            this.classFileNameNoExt = classFileNameNoExt;
            this.dataFileNameNoExt = dataFileNameNoExt;
        }

        public virtual string GenerateClassTitle(int alignmentLevel = 0) {
            return null;
        }
        public virtual string GenerateClass(int alignmentLevel = 0) {
            return null;
        }
        public virtual string GenerateEnum(int alignmentLevel = 0) {
            return null;
        }
        public virtual string GenerateData() {
            return null;
        }
    }

    public abstract class DynamicSheetLine {
        public const string BeginBracket = "{";
        public const string EndBracket = "}";

        protected Sheet sheet;

        public void Reset(Sheet sheet) {
            this.sheet = sheet;
        }

        public virtual string GenerateClass(int alignmentLevel = 0) {
            return null;

        }
        public virtual string GenerateData() {
            return null;
        }
    }
}
