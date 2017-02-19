using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discuz.Api.Entities {
    public class Forum {

        /// <summary>
        /// 版块ID
        /// </summary>
        [JsonProperty("fid")]
        public int ID {
            get;
            set;
        }

        /// <summary>
        /// 版块名称
        /// </summary>
        [JsonProperty("name")]
        public string Name {
            get;
            set;
        }


        /// <summary>
        /// 主题个数
        /// </summary>
        [JsonProperty("threads")]
        public int Threads {
            get;
            set;
        }

        /// <summary>
        ///  帖数
        /// </summary>
        [JsonProperty("posts")]
        public int Posts {
            get;
            set;
        }

        /// <summary>
        /// 今日贴子数
        /// </summary>
        [JsonProperty("todayposts")]
        public int TodayPosts {
            get;
            set;
        }

        [JsonProperty("description")]
        public string Description {
            get;
            set;
        }
    }
}
