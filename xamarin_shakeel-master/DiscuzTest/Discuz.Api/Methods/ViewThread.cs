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
    /// 主题详细（POST LIST）
    /// </summary>
    public class ViewThread : MethodBase<IEnumerable<ThreadPost>> {
        public override string Module {
            get {
                return "viewthread";
            }
        }

        [Param("tid", Required = true)]
        public int ThreadID {
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
        [Param("ppp")]
        public int PageSize {
            get {
                return this.pageSize;
            }
            set {
                this.pageSize = value < 1 ? 1 : value;
            }
        }

        protected override IEnumerable<ThreadPost> Execute(string result) {
            var o = new {
                Variables = new {
                    postlist = Enumerable.Empty<ThreadPost>(),
                    ppp = 0
                }
            };

            o = JsonConvert.DeserializeAnonymousType(result, o);
            if (o.Variables != null) {
                this.PageSize = o.Variables.ppp;
                return o.Variables.postlist;
            } else
                return Enumerable.Empty<ThreadPost>();
        }
    }
}
