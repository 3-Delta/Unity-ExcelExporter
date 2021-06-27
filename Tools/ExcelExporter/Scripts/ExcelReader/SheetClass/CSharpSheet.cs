using System;
using System.Collections.Generic;

namespace ExcelExporter {
    public class CSharpDynamicSheet : DynamicSheet {
        public const string body =
@"#Tab#[System.Serializable]
#Tab#public partial class #ClsName# : /*FormBase<#ClsName#>*/ {
#Tab#    public readonly Dictionary<uint, #ClsName#Line> dict = new Dictionary<uint, #ClsName#Line>();
#Tab#    // public bool hasLoaded { get; protected set; } = false;

#Tab#    public override void LoadFromBinary() {
#Tab#        if(hasLoaded) {
#Tab#            return;
#Tab#        }

#Tab#        using (BinaryOperator.BinaryOperator oper = new BinaryOperator.BinaryOperator(#Path#)) {
#Tab#            dict.Clear();
#Tab#            for (int i = 0, length = oper.ReadInt(); i < length; ++i) {
#Tab#                var line = new #ClsName#Line();
#Tab#                line.Read(oper);
#Tab#                dict.Add(line.id, line);
#Tab#            }
#Tab#        }
            
#Tab#        hasLoaded = true;
#Tab#    }

#Tab#    public bool TryGet(uint id, out #ClsName#Line line) {
#Tab#        line = null;
#Tab#        return dict.TryGetValue(id, out #ClsName#Line line);
#Tab#    }

#Tab#    public void Get(IList<uint> ids, out IList<#ClsName#Line> list) {
#Tab#        list = new IList<#ClsName#Line>();
#Tab#        for (int i = 0, length = ids.Count; i < length; ++ i) {
#Tab#            TryGet(ids[i], out out #ClsName#Line line);
#Tab#            list.Add(line);
#Tab#        }
#Tab#    }

#Tab#    public void Get(IList<IList<uint>> ids, out IList<IList<#ClsName#Line>> list) {
#Tab#        list = new List<IList<#ClsName#Line>>();
#Tab#        for (int i = 0, length = ids.Count; i < length; ++ i) {
#Tab#            var ls = new List<#ClsName#Line>();
#Tab#            for (int j = 0, lengthJ = ids[i].Count; j < lengthJ; ++j) {
#Tab#                TryGet(ids[i][j], out out #ClsName#Line line); 
#Tab#                ls.Add(line);
#Tab#            }
#Tab#            list.Add(ls);
#Tab#        }
#Tab#    }
#Tab#}

";
        public CSharpDynamicSheet() { }
        public CSharpDynamicSheet(Sheet sheet, string classFileNameNoExt, string dataFileNameNoExt) : base(sheet, classFileNameNoExt, dataFileNameNoExt) { }

        public override void GenerateClass(int alignmentLevel = 0) {
            this.stringBuilder.Clear();

            string trim = new string(' ', alignmentLevel * 4);
            this.stringBuilder.Append(body);
            this.stringBuilder.Replace("#Tab#", trim);
            this.stringBuilder.Replace("#ClsName#", this.sheet.sheetName);
            this.stringBuilder.Replace("#Path#", this.dataFileNameNoExt);
        }
        public override void GenerateData() {

        }
    }

    public class CSharpDynamicSheetLine : DynamicSheetLine {
        public static readonly Dictionary<string, Type> NAME2TYPE = new Dictionary<string, Type>() {
            { "bool", typeof(bool)},
            { "byte", typeof(byte)},
            { "sbyte", typeof(sbyte)},
            { "short", typeof(short)},
            { "ushort", typeof(ushort)},
            { "int", typeof(int)},
            { "uint", typeof(uint)},
            { "long", typeof(long)},
            { "ulong", typeof(ulong)},
            { "float", typeof(float)},
            { "string", typeof(string)},
        };

        public override DynamicSheetLine GenerateClass(int alignmentLevel = 0) {
            this.stringBuilder.Clear();

            string trim = new string(' ', alignmentLevel * 4);

            this.stringBuilder.Append(trim);
            this.stringBuilder.AppendLine("[System.Serializable]");
            this.stringBuilder.Append(trim);
            this.stringBuilder.AppendFormat("public partial class {0}Line ", this.sheet.sheetName);
            this.stringBuilder.Append(BeginBracket);

            #region 成员变量
            for (int i = 0, length = this.sheet.fields.Count; i < length; ++i) {
                this.stringBuilder.AppendLine();
                this.stringBuilder.Append(trim);
                Field field = this.sheet.fields[i];
                string realType = field.realType;
                if (field.IsArray) {
                    if (field.isTypeArray) {
                        if (field.IsFormType) {
                            this.stringBuilder.AppendFormat("    public readonly IList<IList<{0}Line>> {1};", realType, field.name);
                        } else {
                            this.stringBuilder.AppendFormat("    public readonly IList<IList<{0}>> {1};", realType, field.name);
                        }
                    } else {
                        if (field.IsFormType) {
                            this.stringBuilder.AppendFormat("    public readonly IList<{0}Line> {1};", realType, field.name);
                        } else {
                            this.stringBuilder.AppendFormat("    public readonly IList<{0}> {1};", realType, field.name);
                        }
                    }
                } else {
                    if (field.IsFormType) {
                        this.stringBuilder.AppendFormat("    public readonly {0}Line {1};", realType, field.name);
                    } else {
                        this.stringBuilder.AppendFormat("    public readonly {0} {1};", realType, field.name);
                    }
                }
            }
            #endregion

            this.stringBuilder.AppendLine();
            this.stringBuilder.AppendLine();
            this.stringBuilder.Append(trim);
            this.stringBuilder.AppendFormat("    public {0}Line(BinaryOperator oper) ", this.sheet.sheetName);
            this.stringBuilder.Append(BeginBracket);

            #region 成员读取赋值
            for (int i = 0, length = this.sheet.fields.Count; i < length; ++i) {
                this.stringBuilder.AppendLine();
                this.stringBuilder.Append(trim);
                this.stringBuilder.Append("        ");

                Field field = this.sheet.fields[i];
                string realType = field.realType;
                bool isBuiltinType = field.IsBuiltinType;
                bool isCellArray = field.isTypeArray;

                string realTypeName = realType;
                if (isBuiltinType) {
                    Type type = NAME2TYPE[realTypeName];
                    realTypeName = type.Name;
                }

                string readBuiltin = "this.{0} = oper.Read{1}();";
                string readCustom = "this.{0} = oper.Read<{1}>();";
                string readForm = "{0}.Instance.TryGet(oper.ReadUInt32(), out this.{1});";

                string readBuiltinArray = "this.{0} = oper.Read{1}Array();";
                string readCustomArray = "this.{0} = oper.ReadArray<{1}>();";
                string readFormArray = "{0}.Instance.Get(oper.ReadUInt32Array(), out IList<{1}Line> this.{2});";

                string readBuiltinArray2 = "this.{0} = oper.Read{1}Array2();";
                string readCustomArray2 = "this.{0} = oper.ReadArray2<{1}>();";
                string readFormArray2 = "{0}.Instance.Get(oper.ReadUInt32Array2(), out IList<IList<{1}Line>> this.{2});";

                if (field.IsArray) {
                    if (isCellArray) {
                        if (field.IsFormType) {
                            this.stringBuilder.AppendFormat(readFormArray2, realTypeName, realTypeName, field.name);
                        } else { 
                            this.stringBuilder.AppendFormat(isBuiltinType ? readBuiltinArray2 : readCustomArray2, field.name, realTypeName);
                        }
                    } else {
                        if (field.IsFormType) {
                            this.stringBuilder.AppendFormat(readFormArray, realTypeName, realTypeName, field.name);
                        } else {
                            this.stringBuilder.AppendFormat(isBuiltinType ? readBuiltinArray : readCustomArray, field.name, realTypeName);
                        }
                    }
                } else {
                    if (field.IsFormType) {
                        this.stringBuilder.AppendFormat(readForm, realTypeName, field.name, field.name);
                    } else {
                        this.stringBuilder.AppendFormat(isBuiltinType ? readBuiltin : readCustom, field.name, realTypeName);
                    }
                }
            }
            #endregion

            this.stringBuilder.AppendLine();
            this.stringBuilder.Append(trim);
            this.stringBuilder.Append("    ");
            this.stringBuilder.Append(EndBracket);

            this.stringBuilder.AppendLine();
            this.stringBuilder.AppendLine();
            this.stringBuilder.Append(trim);
            this.stringBuilder.Append("    public void WriteTo(BinaryOperator oper) ");
            this.stringBuilder.Append(BeginBracket);

            #region 成员Write
            for (int i = 0, length = this.sheet.fields.Count; i < length; ++i) {
                this.stringBuilder.AppendLine();
                this.stringBuilder.Append(trim);
                this.stringBuilder.Append("        ");

                Field field = this.sheet.fields[i];
                string realType = field.realType;
                bool isBuiltinType = field.IsBuiltinType;
                bool isCellArray = field.isTypeArray;

                string realTypeName = realType;
                if (isBuiltinType) {
                    Type type = NAME2TYPE[realTypeName];
                    realTypeName = type.Name;
                }

                string writeBuiltin = "oper.Write{0}(this.{1});";
                string writeCustom = "oper.Write<{0}>(this.{1})";
                string writeForm = "oper.Write<{0}Line>(this.{1})";

                string writeBuiltinArray = "oper.Write{0}Array(this.{1});";
                string writeCustomArray = "oper.WriteArray<{0}>(this.{1});";
                string writeFormArray = "oper.WriteArray<{0}Line>(this.{1});";

                string writeBuiltinArray2 = "oper.Write{0}Array2(this.{1});";
                string writeCustomArray2 = "oper.WriteArray2<{0}>(this.{1});";

                if (field.IsArray) {
                    if (isCellArray) {
                        this.stringBuilder.AppendFormat(isBuiltinType ? writeBuiltinArray2 : writeCustomArray2, realTypeName, field.name);
                    } else {
                        if (field.IsFormType) {
                            this.stringBuilder.AppendFormat(writeFormArray, realTypeName, field.name);
                        } else {
                            this.stringBuilder.AppendFormat(isBuiltinType ? writeBuiltinArray : writeCustomArray, realTypeName, field.name);
                        }
                    }
                } else {
                    if (field.IsFormType) {
                        this.stringBuilder.AppendFormat(writeForm, realTypeName, field.name);
                    } else {
                        this.stringBuilder.AppendFormat(isBuiltinType ? writeBuiltin : writeCustom, realTypeName, field.name);
                    }
                }
            }
            #endregion

            this.stringBuilder.AppendLine();
            this.stringBuilder.Append(trim);
            this.stringBuilder.Append("    ");
            this.stringBuilder.Append(EndBracket);
            this.stringBuilder.AppendLine();

            this.stringBuilder.Append(trim);
            this.stringBuilder.Append(EndBracket);
            this.stringBuilder.AppendLine();

            return this;
        }
    }
}
