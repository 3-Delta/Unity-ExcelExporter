using System;
using System.Collections.Generic;
using System.Text;

namespace ExcelExporter {
    public class CSharpDynamicSheetUsing : DynamicSheetUsing {
        public override string body
        {
            get {
                return
@"using System;
using System.Collections;
using System.Collections.Generic;

";
            }
        }
    }

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

#Tab#        using (BinaryOperator.BinaryOperator oper = new BinaryOperator.BinaryOperator(#DataFilePath#)) {
#Tab#            dict.Clear();
#Tab#            for (int i = 0, length = oper.ReadInt(); i < length; ++i) {
#Tab#                var line = new #ClsName#Line(oper);
#Tab#                if(!dict.ContainsKey(line.id)) {
#Tab#                    dict.Add(line.id, line);
#Tab#                } else {
#Tab#                    Debug.LogError($#Mark##ClsName# has exist id {line.id}.#Mark#);
#Tab#                }
#Tab#            }
#Tab#        }
            
#Tab#        hasLoaded = true;
#Tab#    }

#Tab#    public override void LoadFromJson() {
#Tab#        if(hasLoaded) {
#Tab#            return;
#Tab#        }

#Tab#        using (System.IO.StreamReader reader = new System.IO.StreamReader(#DataFilePath#)) {
#Tab#            try {
#Tab#                string json = reader.ReadToEnd();
#Tab#                dict = JsonConvert.DeserializeObject<Dictionary<uint, #ClsName#Line>>(json);
#Tab#            } catch(Exception ex) { 
#Tab#                Debug.LogError(#ReadJsonFileFailed#);
#Tab#            }
#Tab#        }

#Tab#        hasLoaded = true;
#Tab#    }

#Tab#    public override void LoadFromCs() {
#Tab#        // dict = #ClsName#Data.Instance.dict;
#Tab#    }

#Tab#    public bool TryGet(uint id, out #ClsName#Line line) {
#Tab#        line = null;
#Tab#        return dict.TryGetValue(id, out #ClsName#Line line);
#Tab#    }

#Tab#    public void Get(IList<uint> ids, out IList<#ClsName#Line> list) {
#Tab#        list = new List<#ClsName#Line>();
#Tab#        for (int i = 0, length = ids.Count; i < length; ++ i) {
#Tab#            TryGet(ids[i], out out #ClsName#Line line);
#Tab#            list.Add(line);
#Tab#        }
#Tab#    }

#Tab#    public void Get(IList<IList<uint>> ids, out IList<IList<#ClsName#Line>> list) {
#Tab#        list = new List<IList<#ClsName#Line>>();
#Tab#        for (int i = 0, length = ids.Count; i < length; ++ i) {
#Tab#            Get(ids[i], out IList<#ClsName#Line> ls);
#Tab#            list.Add(ls);
#Tab#        }
#Tab#    }
#Tab#}

";
        public CSharpDynamicSheet() { }
        public CSharpDynamicSheet(Sheet sheet) : base(sheet) { }

        public override string GenerateClass(int alignmentLevel = 0) {
            StringBuilder sb = new StringBuilder();

            string trim = new string(' ', alignmentLevel * 4);
            sb.Append(body);
            sb.Replace("#Tab#", trim);
            sb.Replace("#Mark#", "\"");
            sb.Replace("#ClsName#", sheet.sheetName);
            string filePath = string.Format("\"{0}/{1}\"", sheet.classExportType.ToString(), sheet.sheetName);
            sb.Replace("#DataFilePath#", filePath);
            sb.Replace("#ReadJsonFileFailed#", string.Format("\"Read File:{0} Failed!\"", sheet.sheetName));

            return sb.ToString();
        }

        public override string GenerateData() {
            return null;
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

        public override string Generate(int alignmentLevel = 0) {
            StringBuilder sb = new StringBuilder();
            string trim = new string(' ', alignmentLevel * 4);

            sb.Append(trim);
            sb.AppendLine("[System.Serializable]");
            sb.Append(trim);
            sb.AppendFormat("public partial class {0}Line ", sheet.sheetName);
            sb.Append(ClassHelper.BeginBracket);

            IList<Field> fields = sheet.FinalFields;
            #region 成员变量
            for (int i = 0, length = fields.Count; i < length; ++i) {
                sb.AppendLine();
                sb.Append(trim);
                Field field = fields[i];
                string realType = field.realType;
                if (field.IsArray) {
                    if (field.isTypeArray) {
                        if (field.IsFormType) {
                            sb.AppendFormat("    public readonly IList<IList<{0}Line>> {1};", realType, field.name);
                        }
                        else {
                            sb.AppendFormat("    public readonly IList<IList<{0}>> {1};", realType, field.name);
                        }
                    }
                    else {
                        if (field.IsFormType) {
                            sb.AppendFormat("    public readonly IList<{0}Line> {1};", realType, field.name);
                        }
                        else {
                            sb.AppendFormat("    public readonly IList<{0}> {1};", realType, field.name);
                        }
                    }
                }
                else {
                    if (field.IsFormType) {
                        sb.AppendFormat("    public readonly {0}Line {1};", realType, field.name);
                    }
                    else {
                        sb.AppendFormat("    public readonly {0} {1};", realType, field.name);
                    }
                }
            }

            sb.AppendLine();
            #endregion

            #region 成员读取赋值
            sb.AppendLine();
            sb.Append(trim);
            sb.AppendFormat("    public {0}Line(BinaryOperator oper) ", sheet.sheetName);
            sb.Append(ClassHelper.BeginBracket);
            for (int i = 0, length = fields.Count; i < length; ++i) {
                sb.AppendLine();
                sb.Append(trim);
                sb.Append("        ");

                Field field = fields[i];
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
                string readEnum = "this.{0} = ({1})oper.ReadInt32();";
                string readFloat = "this.{0} = oper.ReadUInt32() * 0.001f;";
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
                            sb.AppendFormat(readFormArray2, realTypeName, realTypeName, field.name);
                        }
                        else {
                            sb.AppendFormat(isBuiltinType ? readBuiltinArray2 : readCustomArray2, field.name, realTypeName);
                        }
                    }
                    else {
                        if (field.IsFormType) {
                            sb.AppendFormat(readFormArray, realTypeName, realTypeName, field.name);
                        }
                        else {
                            sb.AppendFormat(isBuiltinType ? readBuiltinArray : readCustomArray, field.name, realTypeName);
                        }
                    }
                }
                else {
                    if (field.IsFormType) {
                        sb.AppendFormat(readForm, realTypeName, field.name, field.name);
                    }
                    else if (field.IsEnumType) {
                        sb.AppendFormat(readEnum, field.name, realTypeName);
                    }
                    else if (field.IsFloatType) {
                        sb.AppendFormat(readFloat, field.name);
                    }
                    else {
                        sb.AppendFormat(isBuiltinType ? readBuiltin : readCustom, field.name, realTypeName);
                    }
                }
            }

            sb.AppendLine();
            sb.Append(trim);
            sb.Append("    ");
            sb.Append(ClassHelper.EndBracket);
            sb.AppendLine();
            #endregion

            //#region 成员Write
            //sb.AppendLine();
            //sb.Append(trim);
            //sb.Append("    public void WriteTo(BinaryOperator oper) ");
            //sb.Append(ClassHelper.BeginBracket);
            //for (int i = 0, length = fields.Count; i < length; ++i) {
            //    sb.AppendLine();
            //    sb.Append(trim);
            //    sb.Append("        ");

            //    Field field = fields[i];
            //    string realType = field.realType;
            //    bool isBuiltinType = field.IsBuiltinType;
            //    bool isCellArray = field.isTypeArray;

            //    string realTypeName = realType;
            //    if (isBuiltinType) {
            //        Type type = NAME2TYPE[realTypeName];
            //        realTypeName = type.Name;
            //    }

            //    string writeBuiltin = "oper.Write{0}(this.{1});";
            //    string writeCustom = "oper.Write<{0}>(this.{1})";
            //    string writeEnum = "oper.WriteInt32((int)this.{0})";
            //    string writeFloat = "oper.WriteUIn32((uint)(this.{0} * 1000f));";
            //    string writeForm = "oper.Write<{0}Line>(this.{1})";

            //    string writeBuiltinArray = "oper.Write{0}Array(this.{1});";
            //    string writeCustomArray = "oper.WriteArray<{0}>(this.{1});";
            //    string writeFormArray = "oper.WriteArray<{0}Line>(this.{1});";

            //    string writeBuiltinArray2 = "oper.Write{0}Array2(this.{1});";
            //    string writeCustomArray2 = "oper.WriteArray2<{0}>(this.{1});";

            //    if (field.IsArray) {
            //        if (isCellArray) {
            //            sb.AppendFormat(isBuiltinType ? writeBuiltinArray2 : writeCustomArray2, realTypeName, field.name);
            //        }
            //        else {
            //            if (field.IsFormType) {
            //                sb.AppendFormat(writeFormArray, realTypeName, field.name);
            //            }
            //            else {
            //                sb.AppendFormat(isBuiltinType ? writeBuiltinArray : writeCustomArray, realTypeName, field.name);
            //            }
            //        }
            //    }
            //    else {
            //        if (field.IsFormType) {
            //            sb.AppendFormat(writeForm, realTypeName, field.name);
            //        }
            //        else if (field.IsEnumType) {
            //            sb.AppendFormat(writeEnum, field.name);
            //        }
            //        else if (field.IsFloatType) {
            //            sb.AppendFormat(writeFloat, field.name);
            //        }
            //        else {
            //            sb.AppendFormat(isBuiltinType ? writeBuiltin : writeCustom, realTypeName, field.name);
            //        }
            //    }
            //}

            //sb.AppendLine();
            //sb.Append(trim);
            //sb.Append("    ");
            //sb.Append(ClassHelper.EndBracket);
            //#endregion

            sb.AppendLine();
            sb.Append(trim);
            sb.Append(ClassHelper.EndBracket);
            sb.AppendLine();

            return sb.ToString();
        }
    }

    public class CSharpDynamicSheetEnum : DynamicSheetEnum {
        public override string body
        {
            get {
                return
@"#Tab#public eunm E#ClsName#Id {
#Tab#    // EndOfThis
#Tab#}

";
            }
        }

        public CSharpDynamicSheetEnum() { }
        public CSharpDynamicSheetEnum(Sheet sheet) : base(sheet) { }
    }
}
