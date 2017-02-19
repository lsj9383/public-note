using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using Discuz.Api;
using Discuz.Api.Entities;
using Discuz.Api.Methods;
using Discuz.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Discuz.ViewModels {
    /// <summary>
    /// 论坛版块列表
    /// </summary>
    public class ForumIndexViewModel : Screen {

        public ObservableCollection<ListViewGroup<ForumDetailViewModel>> Datas {
            get;
            set;
        }

        private INavigationService NS {
            get;
            set;
        }

        public ICommand RefreshCmd {
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

        public ForumIndexViewModel(INavigationService ns) {
            this.LoadData();
            this.NS = ns;
            this.DisplayName = "论坛列表";

            this.RefreshCmd = new Command(() => this.LoadData());
            this.GotoTopCmd = new Command((l) => this.Scroll((ListView)l, true));
            this.GotoBottomCmd = new Command(l => this.Scroll((ListView)l, false));
            this.OpenInBrowserCmd = new Command(() => this.OpenInBrowser());
        }

        private void Scroll(ListView lst, bool topOrBottom) {
            if (lst == null || this.Datas == null || this.Datas.Count == 0)
                return;
            if (topOrBottom)
                lst.ScrollTo(this.Datas.First().FirstOrDefault(), this.Datas.First(), ScrollToPosition.Start, true);
            else
                lst.ScrollTo(this.Datas.Last().LastOrDefault(), this.Datas.Last(), ScrollToPosition.End, true);
        }

        private async void LoadData() {
            //var hud = DependencyService.Get<IToast>();
            //hud.Show("正在加载");

            var method = new ForumIndex();
            var catalogs = await ApiClient.Execute(method);

            var groups = catalogs.Select(c => new ListViewGroup<ForumDetailViewModel>(c.SubFourms.Select(s => new ForumDetailViewModel(s, this.NS))) {
                Title = c.Name
            });

            this.Datas = new ObservableCollection<ListViewGroup<ForumDetailViewModel>>(groups);
            this.NotifyOfPropertyChange(() => this.Datas);

            //hud.Close();
        }

        private void OpenInBrowser() {
            Device.OpenUri(new Uri("http://bbs.blueidea.com"));
        }
    }
}
