using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Discuz.ViewModels {
    public class FavoriteViewModel : Screen {

        public static readonly string RemoveFavorite = "RemoveFavorite";

        public BindableCollection<KeyValuePair<int, string>> Datas {
            get;
            set;
        }

        public object Selected {
            get;
            set;
        }

        public ICommand RemoveCmd {
            get;
            set;
        }

        public FavoriteViewModel() {
            this.Datas = new BindableCollection<KeyValuePair<int, string>>();
            this.RemoveCmd = new Command(id => {
                MessagingCenter.Send(this, RemoveFavorite, (int)id);
                var data = this.Datas.FirstOrDefault(p => p.Key == (int)id);
                this.Datas.Remove(data);
                this.NotifyOfPropertyChange(() => this.Datas);
            });
        }

        protected override void OnActivate() {
            base.OnActivate();

            var favorites = PropertiesHelper.GetObject<SortedDictionary<int, string>>("Favorites") ?? new SortedDictionary<int, string>();
            this.Datas.AddRange(favorites.ToList());
            this.NotifyOfPropertyChange(() => this.Datas);
        }

        public void Remove() {

        }
    }
}
