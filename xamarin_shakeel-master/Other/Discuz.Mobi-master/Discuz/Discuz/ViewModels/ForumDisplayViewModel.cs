using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using Discuz.Api;
using Discuz.Api.Entities;
using Discuz.Api.Methods;
using Discuz.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Discuz.ViewModels {
    /// <summary>
    /// 版块主题列表
    /// </summary>
    public class ForumDisplayViewModel : Screen {

        private int OldID = 0;

        public int ID {
            get;
            set;
        }

        public BindableCollection<ThreadSummaryViewModel> Datas {
            get;
            set;
        }

        /// <summary>
        /// 是否正在刷新
        /// </summary>
        public bool InRefresh {
            get;
            set;
        }

        /// <summary>
        /// 刷新命令
        /// </summary>
        public ICommand RefreshCmd {
            get;
            set;
        }

        /// <summary>
        /// 加载下一页
        /// </summary>
        public ICommand LoadMoreCmd {
            get;
            set;
        }

        public ICommand GotoTopCmd {
            get;
            set;
        }

        public ICommand GotoBottomCmd {
            get;
            set;
        }

        public ICommand OpenInBrowserCmd {
            get;
            set;
        }

        /// <summary>
        /// 从1开始
        /// </summary>
        public int Page = 1;

        private INavigationService NS = null;

        public ForumDisplayViewModel(INavigationService ns) {
            this.NS = ns;

            this.Datas = new BindableCollection<ThreadSummaryViewModel>();
            this.RefreshCmd = new Command(() => this.LoadData(true));
            this.LoadMoreCmd = new Command(() => this.LoadData(false));
            this.GotoTopCmd = new Command((l) => this.Scroll((ListView)l, true));
            this.GotoBottomCmd = new Command(l => this.Scroll((ListView)l, false));
            this.OpenInBrowserCmd = new Command(() => this.OpenInBrowser());
        }

        private void Scroll(ListView lst, bool topOrBottom) {
            if (lst == null || this.Datas == null || this.Datas.Count == 0)
                return;
            if (topOrBottom)
                lst.ScrollTo(this.Datas.FirstOrDefault(), ScrollToPosition.Start, true);
            else
                lst.ScrollTo(this.Datas.LastOrDefault(), ScrollToPosition.End, true);
        }

        protected override void OnActivate() {
            base.OnActivate();

            if (this.OldID != this.ID)
                this.LoadData(true);
        }

        private async void LoadData(bool isRefresh) {
            var hud = DependencyService.Get<IToast>();
            hud.Show("正在加载数据, 现在展示的不是最新数据");

            if (isRefresh)
                this.Page = 1;
            else
                this.Page++;

            this.InRefresh = true;
            this.NotifyOfPropertyChange(() => this.InRefresh);

            var method = new ForumDisplay() {
                ForumID = this.ID,
                Page = this.Page
            };
            var datas = await ApiClient.Execute(method);
            var vms = datas.Select(t => new ThreadSummaryViewModel(t, this.NS));

            if (isRefresh)
                this.Datas = new BindableCollection<ThreadSummaryViewModel>(vms);
            else {
                this.Datas.AddRange(vms);
            }

            //保持总量就 100条
            if (this.Datas.Count > 100) {
                this.Datas.RemoveRange(this.Datas.Take(this.Datas.Count - 100));
            }

            this.NotifyOfPropertyChange(() => this.Datas);
            this.OldID = this.ID;

            this.InRefresh = false;
            this.NotifyOfPropertyChange(() => this.InRefresh);

            hud.Close();
        }

        private void OpenInBrowser() {
            Device.OpenUri(new Uri(string.Format("http://bbs.blueidea.com/forum-{0}-1.html", this.ID)));
        }
    }
}
