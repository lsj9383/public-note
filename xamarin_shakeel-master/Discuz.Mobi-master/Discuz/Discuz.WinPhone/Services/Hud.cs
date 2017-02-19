using Discuz.Services;
using Discuz.WinPhone.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using Xamarin.Forms;

[assembly: Dependency(typeof(Hud))]
namespace Discuz.WinPhone.Services {
    public class Hud : IToast {

        private System.Windows.Controls.Grid Container = new System.Windows.Controls.Grid();

        private Popup Popup = null;

        public Hud() {
            this.Popup = new Popup() {
                Child = new Border() {
                    Background = new SolidColorBrush(Colors.Black),
                    Opacity = 0.6,
                    CornerRadius = new CornerRadius(10),
                    Padding = new System.Windows.Thickness(10),
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                    VerticalAlignment = System.Windows.VerticalAlignment.Center,
                    Child = this.Container,
                }
            };

            this.Container.SizeChanged += Container_SizeChanged;
        }

        void Container_SizeChanged(object sender, SizeChangedEventArgs e) {
            var size = System.Windows.Application.Current.RootVisual.RenderSize;
            this.Popup.HorizontalOffset = (size.Width - this.Container.ActualWidth) / 2;
            this.Popup.VerticalOffset = (size.Height - this.Container.ActualHeight) / 2;
        }

        public void ShowToast(string msg, int delay = 1000) {
            this.Popup.IsOpen = true;
            this.Container.Children.Clear();
            this.Container.Children.Add(new TextBlock() {
                Text = msg,
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 20
            });

            var v = Windows.Phone.Devices.Notification.VibrationDevice.GetDefault();
            v.Vibrate(TimeSpan.FromSeconds(0.1));

            Task.Delay(delay)
                .ContinueWith(t =>
                    Deployment.Current.Dispatcher.BeginInvoke(() => {
                        this.Popup.IsOpen = false;
                    })
                    );
        }

        public void Close() {
            this.Popup.IsOpen = false;
        }

        public void Show(string msg) {
            this.ShowToast(msg, 60000);
        }
    }
}
