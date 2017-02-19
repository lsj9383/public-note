using Discuz.Controls;
using Discuz.WinPhone.Renders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;

[assembly: ExportRendererAttribute(typeof(Border), typeof(BorderRender))]
namespace Discuz.WinPhone.Renders {
    public class BorderRender : ViewRenderer<Border, System.Windows.Controls.Border> {

        protected override void OnElementChanged(ElementChangedEventArgs<Border> e) {
            base.OnElementChanged(e);
            SetNativeControl(new System.Windows.Controls.Border());
            UpdateControl();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e) {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == "Content") {
                PackChild();
            } else if (e.PropertyName == Border.StrokeProperty.PropertyName ||
                       e.PropertyName == Border.StrokeThicknessProperty.PropertyName ||
                       e.PropertyName == Border.CornerRadiusProperty.PropertyName ||
                       e.PropertyName == Border.PaddingProperty.PropertyName) {
                UpdateControl();
            }
        }

        // the base class is setting the background to the renderer when Control is null
        protected override void UpdateBackgroundColor() {
            if (Control != null) {
                Control.Background = (this.Element.BackgroundColor != Xamarin.Forms.Color.Default ? this.Element.BackgroundColor.ToBrush() : base.Background);
            }
        }

        private void PackChild() {
            if (Element.Content == null) {
                return;
            }
            if (Element.Content.GetRenderer() == null) {
                Element.Content.SetRenderer(RendererFactory.GetRenderer(Element.Content));
            }
            var renderer = Element.Content.GetRenderer() as System.Windows.UIElement;
            Control.Child = renderer;
        }

        private void UpdateControl() {
            Control.CornerRadius = new System.Windows.CornerRadius(Element.CornerRadius);
            Control.BorderBrush = Element.Stroke.ToBrush();
            Control.BorderThickness = Element.StrokeThickness.ToWinPhone();
            Control.Padding = Element.Padding.ToWinPhone();

            if (Element.IsClippedToBorder) {
                // var size = Control.Child.RenderSize;
                Control.Child.Clip = new RectangleGeometry() {
                    Rect = new System.Windows.Rect(0, 0, 400, 400), // just testing with some values for now
                    RadiusX = Element.CornerRadius,
                    RadiusY = Element.CornerRadius
                };
            }
        }
    }
}
