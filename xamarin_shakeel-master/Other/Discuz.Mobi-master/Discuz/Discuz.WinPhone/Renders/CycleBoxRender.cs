using Discuz.Controls;
using Discuz.WinPhone.Renders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;

[assembly: ExportRenderer(typeof(CycleBox), typeof(CycleBoxRender))]
namespace Discuz.WinPhone.Renders {
    public class CycleBoxRender : ViewRenderer<CycleBox, System.Windows.Controls.Border> {

        protected override void OnElementChanged(ElementChangedEventArgs<CycleBox> e) {
            base.OnElementChanged(e);

            this.SetNativeControl(new System.Windows.Controls.Border());
            this.UpdateControl();
        }


        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);

            this.Element.HorizontalOptions = LayoutOptions.Center;
            this.Element.VerticalOptions = LayoutOptions.Center;

            if (e.PropertyName.Equals(CycleBox.RadiusProperty.PropertyName, StringComparison.OrdinalIgnoreCase)) {
                this.UpdateControl();
            }
        }

        protected override void UpdateNativeWidget() {
            base.UpdateNativeWidget();
            this.UpdateControl();
        }

        protected override void UpdateBackgroundColor() {
            if (Control != null) {
                Control.Background = this.Element.BackgroundColor.ToBrush();
            }
        }

        private void UpdateControl() {
            this.Control.CornerRadius = new CornerRadius(this.Element.Radius);
            var render = this.Element.Content.GetRenderer() as UIElement;
            render.Clip = new EllipseGeometry {
                Center = new System.Windows.Point(this.Width / 2, this.Height / 2),
                RadiusX = this.Element.Radius,
                RadiusY = this.Element.Radius
            };
        }
    }
}
