using Discuz.WinPhone.Renders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(TabbedPageRender))]
namespace Discuz.WinPhone.Renders {
    public class TabbedPageRender : TabbedPageRenderer {

        public TabbedPageRender() {
            this.HeaderTemplate = (System.Windows.DataTemplate)System.Windows.Application.Current.Resources["TabbedPageHeader1"];
            this.Style = (System.Windows.Style)System.Windows.Application.Current.Resources["MyPivot"];
        }

        protected override void OnSelectionChanged(System.Windows.Controls.SelectionChangedEventArgs e) {
            base.OnSelectionChanged(e);
            foreach (var p in this.Items) {
                var page = (Page)p;
                if (page != null) {
                    if (e.AddedItems.Contains(p)) {
                        page.IsEnabled = true;
                    } else {
                        page.IsEnabled = false;
                    }
                }
            }
        }
    }
}
