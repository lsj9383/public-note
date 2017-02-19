using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Discuz.Controls {
    public class Card : ContentView {

        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create<Border, double>(p => p.CornerRadius, 0);

        public double CornerRadius {
            get {
                return (double)base.GetValue(CornerRadiusProperty);
            }
            set {
                base.SetValue(CornerRadiusProperty, value);
            }
        }


    }
}
