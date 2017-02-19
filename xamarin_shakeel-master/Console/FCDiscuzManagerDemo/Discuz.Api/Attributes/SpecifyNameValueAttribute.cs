using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discuz.Api.Attributes {

    /// <summary>
    /// 枚举参数,指定 Key Value
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class SpecifyNameValueAttribute : Attribute {

        /// <summary>
        /// 指定枚举参数的 Name, 用以替代 枚举的名称
        /// </summary>
        public string Name {
            get;
            set;
        }

        /// <summary>
        /// 指定枚举参数的 Value, 用以替代 枚举的值
        /// </summary>
        public int Value {
            get;
            set;
        }

    }
}
