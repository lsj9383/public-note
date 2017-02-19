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
    /// 版块详细信息，论坛版块列表的子视图
    /// </summary>
    public class ForumDetailViewModel : Screen {

        internal static readonly string AddFavoriteMessage = "AddFavorite";

        public Forum Data {
            get;
            private set;
        }

        private INavigationService NS {
            get;
            set;
        }

        public ICommand TapCommand {
            get;
            set;
        }

        public ICommand AddFavCommand {
            get;
            set;
        }

        public ForumDetailViewModel(Forum data, INavigationService ns) {
            this.Data = data;
            this.NS = ns;
            this.TapCommand = new Command(() => this.Show());
            this.AddFavCommand = new Command(() => this.AddToFavorite());
        }

        public void Show() {
            this.NS.For<ForumDisplayViewModel>()
                .WithParam(p => p.ID, this.Data.ID)
                .WithParam(p => p.DisplayName, this.Data.Name)
                .Navigate();
        }

        public async void AddToFavorite() {
            var r = await App.Current.MainPage.DisplayAlert("提示", "确认添收藏吗?", "确认", "取消");
            if (r) {
                MessagingCenter.Send(this, AddFavoriteMessage, new Tuple<int, string>(this.Data.ID, this.Data.Name));
            }
        }
    }
}
