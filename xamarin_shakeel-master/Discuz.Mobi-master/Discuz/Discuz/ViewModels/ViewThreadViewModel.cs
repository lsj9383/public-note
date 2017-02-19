using Caliburn.Micro;
using Discuz.Api;
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
    /// 主题详细 （POST LIST)
    /// </summary>
    public class ViewThreadViewModel : Screen {

        private int OldID = 0;

        public int ThreadID {
            get;
            set;
        }


        public BindableCollection<PostDetailViewModel> Datas {
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

        public ViewThreadViewModel() {
            this.RefreshCmd = new Command(() => this.LoadData(true));
            this.LoadMoreCmd = new Command(() => this.LoadData(false));
            this.GotoTopCmd = new Command((l) => this.Scroll((ListView)l, true));
            this.GotoBottomCmd = new Command(l => this.Scroll((ListView)l, false));
            this.OpenInBrowserCmd = new Command(() => this.OpenInBrowser());
        }

        protected override void OnActivate() {
            base.OnActivate();

            this.Datas = new BindableCollection<PostDetailViewModel>();

            if (this.OldID != this.ThreadID)
                this.LoadData(true);
        }

        private void Scroll(ListView lst, bool topOrBottom) {
            if (lst == null || this.Datas == null || this.Datas.Count == 0)
                return;
            if (topOrBottom)
                lst.ScrollTo(this.Datas.FirstOrDefault(), ScrollToPosition.Start, true);
            else
                lst.ScrollTo(this.Datas.LastOrDefault(), ScrollToPosition.End, true);
        }

        private async void LoadData(bool isRefresh) {
            var hud = DependencyService.Get<IToast>();
            hud.Show("正在加载, 现在展示的不是最新数据");

            if (isRefresh)
                this.Page = 1;
            else
                this.Page++;

            this.InRefresh = true;
            this.NotifyOfPropertyChange(() => this.InRefresh);

            var method = new ViewThread() {
                ThreadID = this.ThreadID,
                Page = this.Page
            };
            var datas = await ApiClient.Execute(method);
            var vms = datas.Select(d => new PostDetailViewModel(d));

            if (isRefresh)
                this.Datas = new BindableCollection<PostDetailViewModel>(vms);
            else {
                this.Datas.AddRange(vms);
            }

            this.NotifyOfPropertyChange(() => this.Datas);
            this.OldID = this.ThreadID;

            this.InRefresh = false;
            this.NotifyOfPropertyChange(() => this.InRefresh);
            hud.Close();
        }

        private void OpenInBrowser() {
            Device.OpenUri(new Uri(string.Format("http://bbs.blueidea.com/thread-{0}-1-1.html", this.ThreadID)));
        }
    }
}
