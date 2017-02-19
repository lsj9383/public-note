using Caliburn.Micro;
using Discuz.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discuz.ViewModels {
    /// <summary>
    /// 主题的子视图，回贴详细
    /// </summary>
    public class PostDetailViewModel : Screen {

        public ThreadPost Data {
            get;
            set;
        }

        public PostDetailViewModel(ThreadPost post) {
            this.Data = post;
        }

    }
}
