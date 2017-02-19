using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using Discuz.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Discuz.ViewModels {
    public class SettingViewModel : Screen {

        private static readonly string ImageLoaderCachePath = "ImageLoaderCache";

        public ICommand EditFavoriteCmd {
            get;
            set;
        }

        public ICommand ClearCacheCmd {
            get;
            set;
        }

        public ICommand CalcCacheSizeCmd {
            get;
            set;
        }

        private IStorage Storage = null;

        public string CacheSize {
            get;
            set;
        }

        public SettingViewModel(INavigationService ns) {
            this.DisplayName = "设置";
            this.CacheSize = "未计算";

            this.Storage = DependencyService.Get<IStorage>();

            this.EditFavoriteCmd = new Command(() => {
                ns.For<FavoriteViewModel>()
                    .Navigate();
            });

            this.ClearCacheCmd = new Command(async () => {
                await this.Storage.Clear(ImageLoaderCachePath);
            });

            this.CalcCacheSizeCmd = new Command(async () => {
                await this.CalcCacheSize();
            });
        }

        public async Task CalcCacheSize() {
            var size = await this.Storage.GetSize(ImageLoaderCachePath);
            if (size > 1048576) //M
                this.CacheSize = string.Format("{0} M", size / 1048576);
            else if (size > 1024) //k
                this.CacheSize = string.Format("{0} K", size / 1024);
            else
                this.CacheSize = string.Format("{0} Byte", size);

            this.NotifyOfPropertyChange(() => this.CacheSize);
        }
    }
}
