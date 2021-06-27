using System;
using System.Collections.Generic;
using System.Text;

namespace ExcelExporter {
    public class DynamicSheet {
        protected readonly StringBuilder stringBuilder = new StringBuilder();

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

        public virtual void GenerateClass(int alignmentLevel = 0) {

        }
        public virtual void GenerateData() {

        }

        public override string ToString() {
            return stringBuilder.ToString();
        }
    }

    public abstract class DynamicSheetLine {
        public const string BeginBracket = "{";
        public const string EndBracket = "}";

        protected Sheet sheet;
        protected readonly StringBuilder stringBuilder = new StringBuilder();

        public void Reset(Sheet sheet) {
            this.sheet = sheet;
        }

        public virtual DynamicSheetLine GenerateClass(int alignmentLevel = 0) {
            return this;

        }
        public virtual DynamicSheetLine GenerateData() {
            return this;
        }

        public virtual void Write(IList<string> exportTypes) {
            for (int i = 0, length = exportTypes.Count; i < length; ++i) {
            }
        }

        public override string ToString() {
            return stringBuilder.ToString();
        }
    }
}
