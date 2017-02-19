using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Discuz.Api.Attributes {

    public enum EnumUseNameOrValue {
        Name,
        Value
    }


    /// <summary>
    /// 枚举参数
    /// </summary>
    public class EnumParamAttribute : ParamAttribute {

        private EnumUseNameOrValue Use;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="use">使用枚举的名称还是值</param>
        public EnumParamAttribute(string name, EnumUseNameOrValue use)
            : base(name) {

            this.Use = use;
        }

        public override Dictionary<string, string> GetParams(object obj, System.Reflection.PropertyInfo p) {
            var value = p.GetValue(obj, null);
            if (value != null) {
                var skv = value.GetType()
                        .GetRuntimeField(value.ToString())
                        .GetCustomAttributes(false)
                        .OfType<SpecifyNameValueAttribute>().FirstOrDefault();

                if (skv != null) {
                    value = this.Use == EnumUseNameOrValue.Name ? (object)skv.Name : skv.Value;
                } else {
                    value = this.Use == EnumUseNameOrValue.Name ? Enum.GetName(obj.GetType(), obj) : value;
                }
            }


            if (value == null && this.Required)
                return new Dictionary<string, string>(){
                    {this.Name, ""}
                };
            else if (value == null && !this.Required)
                return null;
            else
                return new Dictionary<string, string>(){
                    {this.Name, value.ToString()}
                };
        }

    }
}
