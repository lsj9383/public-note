using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace Discuz.Controls {
    public class Border : ContentView {

        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create<Border, double>(p => p.CornerRadius, 0);
        public static readonly BindableProperty StrokeProperty = BindableProperty.Create<Border, Color>(p => p.Stroke, Color.Transparent);
        public static readonly BindableProperty StrokeThicknessProperty = BindableProperty.Create<Border, Thickness>(p => p.StrokeThickness, default(Thickness));
        public static readonly BindableProperty IsClippedToBorderProperty = BindableProperty.Create<Border, bool>(p => p.IsClippedToBorder, default(bool));

        public double CornerRadius {
            get {
                return (double)base.GetValue(CornerRadiusProperty);
            }
            set {
                base.SetValue(CornerRadiusProperty, value);
            }
        }



        public Color Stroke {
            get {
                return (Color)GetValue(StrokeProperty);
            }
            set {
                SetValue(StrokeProperty, value);
            }
        }


        public Thickness StrokeThickness {
            get {
                return (Thickness)GetValue(StrokeThicknessProperty);
            }
            set {
                SetValue(StrokeThicknessProperty, value);
            }
        }

        public bool IsClippedToBorder {
            get {
                return (bool)GetValue(IsClippedToBorderProperty);
            }
            set {
                SetValue(IsClippedToBorderProperty, value);
            }
        }

        // cross-platform way to take into account stroke thickness
        protected override void LayoutChildren(double x, double y, double width, double height) {
            x += StrokeThickness.Left;
            y += StrokeThickness.Top;

            width -= StrokeThickness.HorizontalThickness;
            height -= StrokeThickness.VerticalThickness;

            base.LayoutChildren(x, y, width, height);
        }
    }
}
