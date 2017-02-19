using Discuz.Api.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discuz.Api.Entities {
    /// <summary>
    /// 回贴
    /// </summary>
    public class ThreadPost {

        [JsonProperty("pid")]
        public int ID {
            get;
            set;
        }

        /// <summary>
        /// 主题ID
        /// </summary>
        [JsonProperty("tid")]
        public int ThreadID {
            get;
            set;
        }

        /// <summary>
        /// 是否是主题贴
        /// </summary>
        [JsonProperty("first"), JsonConverter(typeof(BoolConverter))]
        public bool IsFirst {
            get;
            set;
        }

        [JsonProperty("author")]
        public string Author {
            get;
            set;
        }

        [JsonProperty("authorid")]
        public int AuthorID {
            get;
            set;
        }

        /// <summary>
        /// 发表日期的显示文字
        /// </summary>
        [JsonProperty("dateline")]
        public string Date {
            get;
            set;
        }

        [JsonProperty("message")]
        public string Content {
            get;
            set;
        }

        /// <summary>
        /// ???
        /// </summary>
        [JsonProperty("anonymous")]
        public string Anonymous {
            get;
            set;
        }

        [JsonProperty("attachment")]
        public int Attachment {
            get;
            set;
        }

        [JsonProperty("attachments")]
        public Dictionary<string, Attachement> Attachments {
            get;
            set;
        }

        public string AuthorImg {
            get {
                return string.Format("http://center.blueidea.com/avatar.php?uid={0}&size=small", this.AuthorID);
            }
        }

        //"status": "0",
        //"username": "liangyuan99",
        //"adminid": "-1",
        //"groupid": "5",
        //"memberstatus": "0",
        //"number": "-1",
        //"dbdateline": "1340331008"
    }
}
