using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Discuz.Controls {
    public class CycleBox : ContentView {

        /// <summary>
        /// 半径
        /// </summary>
        public static readonly BindableProperty RadiusProperty = BindableProperty.Create<CycleBox, double>(c => c.Radius, 40);

        public double Radius {
            get {
                return (Double)base.GetValue(RadiusProperty);
            }
            set {
                base.SetValue(RadiusProperty, value);
            }
        }

    }
}
