using Discuz.Api.Attributes;
using Discuz.Api.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discuz.Api.Methods {
    /// <summary>
    /// 版块主题列表
    /// </summary>
    public class ForumDisplay : MethodBase<IEnumerable<ThreadSummary>> {
        public override string Module {
            get {
                return "forumdisplay";
            }
        }

        /// <summary>
        /// fid
        /// </summary>
        [Param("fid", Required = true)]
        public int ForumID {
            get;
            set;
        }

        private int page = 1;
        [Param("page")]
        public int Page {
            get {
                return this.page;
            }
            set {
                this.page = value < 1 ? 1 : value;
            }
        }

        private int pageSize = 20;
        [Param("tpp")]
        public int PageSize {
            get {
                return this.pageSize;
            }
            set {
                this.pageSize = value < 1 ? 1 : value;
            }
        }

        protected override IEnumerable<ThreadSummary> Execute(string result) {
            var o = new {
                Variables = new {
                    forum_threadlist = Enumerable.Empty<ThreadSummary>(),
                    tpp = 0,
                    page = (int?)0
                }
            };

            o = JsonConvert.DeserializeAnonymousType(result, o);
            if (o.Variables != null) {
                this.Page = o.Variables.page ?? 1;
                this.PageSize = o.Variables.tpp;

                return o.Variables.forum_threadlist;
            } else {
                return Enumerable.Empty<ThreadSummary>();
            }
        }
    }
}
