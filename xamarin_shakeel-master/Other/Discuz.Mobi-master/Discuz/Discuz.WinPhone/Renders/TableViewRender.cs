using Discuz.WinPhone.Renders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;


//[assembly: ExportRenderer(typeof(Xamarin.Forms.TableView), typeof(TableViewRender))]
namespace Discuz.WinPhone.Renders {
    public class TableViewRender : TableViewRenderer {

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.TableView> e) {
            base.OnElementChanged(e);

            this.Control.Loaded += Control_Loaded;
        }

        void Control_Loaded(object sender, RoutedEventArgs e) {
            var lst = (ListBox)VisualTreeHelper.GetChild(this.Control, 1);
            lst.ItemTemplate = null;
            lst.ItemTemplate = (System.Windows.DataTemplate)System.Windows.Application.Current.Resources["TableGroup1"];
        }

    }
}
