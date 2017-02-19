using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Discuz {
    public class UniPadding {

        //改成 Thickness 后, Xaml 运行报错啊,
        public static readonly BindableProperty PaddingProperty = BindableProperty.CreateAttached<UniPadding, int>(
               bindable => UniPadding.GetPadding(bindable),
               0,
               BindingMode.OneWay,
               propertyChanged: (b, o, n) => PaddingChanged(b, n, o)
               );


        private static int GetPadding(BindableObject bo) {
            return (int)bo.GetValue(UniPadding.PaddingProperty);
        }

        private static void PaddingChanged(BindableObject bindable, object oldvalue, object newvalue) {
            var e = (Element)bindable;
            if (e != null) {
                e.ChildAdded += e_ChildAdded;
            }
        }

        static void e_ChildAdded(object sender, ElementEventArgs e) {
            var padding = UniPadding.GetPadding((BindableObject)sender);
            
        }
    }
}
