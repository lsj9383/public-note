using Discuz.Api.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discuz.Api.Entities {

    /// <summary>
    /// 附件
    /// </summary>
    public class Attachement {

        [JsonProperty("aid")]
        public int ID {
            get;
            set;
        }

        [JsonProperty("pid")]
        public int PostID {
            get;
            set;
        }

        [JsonProperty("tid")]
        public int Thread {
            get;
            set;
        }

        [JsonProperty("uid")]
        public int AuthorID {
            get;
            set;
        }


        [JsonProperty("dateline")]
        public string Date {
            get;
            set;
        }


        /// <summary>
        /// byte
        /// </summary>
        [JsonProperty("filesize")]
        public int Filesize {
            get;
            set;
        }

        [JsonProperty("filename")]
        public string FileName {
            get;
            set;
        }

        [JsonProperty("attachment")]
        public string Path2 {
            get;
            set;
        }

        [JsonProperty("url")]
        public string Path1 {
            get;
            set;
        }

        /// <summary>
        /// 不包含域名
        /// </summary>
        public string Url {
            get {
                return Path.Combine(Path1, Path2);
            }
        }

        [JsonProperty("imgalt")]
        public string Alt {
            get;
            set;
        }

        [JsonProperty("attachimg"), JsonConverter(typeof(BoolConverter))]
        public bool IsImage {
            get;
            set;
        }

        /// <summary>
        /// 下载次数
        /// </summary>
        [JsonProperty("downloads")]
        public int Downloads {
            get;
            set;
        }

        //dateline": "2015-9-21 23:53",
        //"filename": "830834950816677793.jpg",
        //"filesize": "94831",
        //"attachment": "data/attachment/forum/201509/21/235318iuvm1emu17rvx09z.jpg",
        //"remote": "0",
        //"description": "",
        //"readperm": "0",
        //"price": "0",
        //"isimage": "1",
        //"width": "720",
        //"thumb": "1",
        //"picid": "0",
        //"sha1": "",
        //"ext": "jpg",
        //"imgalt": "830834950816677793.jpg",
        //"attachicon": "<img src=\"static/image/filetype/image.gif\" border=\"0\" class=\"vm\" alt=\"\" />",
        //"attachsize": "92.61 KB",
        //"attachimg": "1",
        //"payed": "1",
        //"url": "data/attachment/forum/",
        //"dbdateline": "1442850798",
        //"downloads": "0"
    }
}
