using Discuz.Api.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discuz.Api.Methods {
    /// <summary>
    /// 论坛版块列表
    /// </summary>
    public class ForumIndex : MethodBase<IEnumerable<ForumCatalog>> {
        public override string Module {
            get {
                return "forumindex";
            }
        }

        protected override IEnumerable<ForumCatalog> Execute(string result) {
            var o = new {
                Variables = new {
                    catlist = Enumerable.Empty<ForumCatalog>(),
                    forumlist = Enumerable.Empty<Forum>()
                }
            };

            o = JsonConvert.DeserializeAnonymousType(result, o);

            if (o.Variables != null) {
                foreach (var c in o.Variables.catlist) {
                    c.SubFourms = o.Variables.forumlist.Where(f => c.SubFourmIDs.Contains(f.ID));
                }

                return o.Variables.catlist;
            } else {
                return Enumerable.Empty<ForumCatalog>();
            }
        }
    }
}
