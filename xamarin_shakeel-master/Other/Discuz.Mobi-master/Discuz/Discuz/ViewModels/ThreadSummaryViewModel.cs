using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using Discuz.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Discuz.ViewModels {
    /// <summary>
    /// 主题信息，主题列表的子视图
    /// </summary>
    public class ThreadSummaryViewModel : Screen {

        public ThreadSummary Data {
            get;
            private set;
        }

        public ICommand TapCommand {
            get;
            set;
        }

        private INavigationService NS = null;

        public ThreadSummaryViewModel(ThreadSummary data, INavigationService ns) {
            this.Data = data;
            this.NS = ns;
            this.TapCommand = new Command(() => this.Show());
        }

        private void Show() {
            this.NS.For<ViewThreadViewModel>()
                .WithParam(p => p.DisplayName, this.Data.Subject)
                .WithParam(p => p.ThreadID, this.Data.ID)
                .Navigate();
        }
    }
}
