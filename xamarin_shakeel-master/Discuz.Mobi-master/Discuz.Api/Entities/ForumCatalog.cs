using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discuz.Api.Entities {
    /// <summary>
    /// 论坛目录
    /// </summary>
    public class ForumCatalog {

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("fid")]
        public int ID {
            get;
            set;
        }

        [JsonProperty("name")]
        public string Name {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("forums")]
        public List<int> SubFourmIDs {
            get;
            set;
        }


        public IEnumerable<Forum> SubFourms {
            get;
            set;
        }
    }
}
