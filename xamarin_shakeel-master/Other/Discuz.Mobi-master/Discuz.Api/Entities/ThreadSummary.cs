using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discuz.Api.Entities {
    /// <summary>
    /// 贴子
    /// </summary>
    public class ThreadSummary {

        [JsonProperty("tid")]
        public int ID {
            get;
            set;
        }

        /// <summary>
        /// 阅读权限
        /// </summary>
        [JsonProperty("readperm")]
        public int ReadPermission {
            get;
            set;
        }

        /// <summary>
        /// 作者
        /// </summary>
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

        [JsonProperty("subject")]
        public string Subject {
            get;
            set;
        }

        /// <summary>
        /// 主题发表时间的文字表现
        /// </summary>
        [JsonProperty("dateline")]
        public string Date {
            get;
            set;
        }

        /// <summary>
        /// 最后回复时间的文字表现
        /// </summary>
        [JsonProperty("lastpost")]
        public string LastPostDateString {
            get;
            set;
        }

        /// <summary>
        /// 最后回复人
        /// </summary>
        [JsonProperty("lastposter")]
        public string LastPoster {
            get;
            set;
        }

        /// <summary>
        /// 查看次数
        /// </summary>
        [JsonProperty("views")]
        public int Views {
            get;
            set;
        }

        /// <summary>
        /// 回复数
        /// </summary>
        [JsonProperty("replies")]
        public int Replies {
            get;
            set;
        }

        /// <summary>
        /// 不知道是干什么的
        /// </summary>
        [JsonProperty("digest")]
        public string Digest {
            get;
            set;
        }

        /// <summary>
        /// 附件数
        /// </summary>
        [JsonProperty("attachment")]
        public int Attachment {
            get;
            set;
        }

        public string AuthorImg {
            get {
                return string.Format("http://center.blueidea.com/avatar.php?uid={0}&size=small", this.AuthorID);
            }
        }
    }
}
