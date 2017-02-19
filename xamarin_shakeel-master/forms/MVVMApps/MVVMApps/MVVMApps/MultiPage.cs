using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace MVVMApps
{
    public class MultiPage : ContentPage
    {
        public MultiPage()
        {
            SimpleMultiplierViewModel vm = new SimpleMultiplierViewModel();
            Slider sliderLeft = new Slider();
            Slider sliderRight = new Slider();
            Entry entryLeft = new Entry();
            Label lblResult1 = new Label();
            Label lblResult2 = new Label();
            Label lblResult3 = new Label();

            entryLeft.SetBinding(Entry.TextProperty, new Binding() { Source=vm, Path= "Multiplicand", Mode=BindingMode.OneWayToSource });
            sliderLeft.SetBinding(Slider.ValueProperty, new Binding() { Source = vm, Path = "Multiplicand" });
            sliderRight.SetBinding(Slider.ValueProperty, new Binding() { Source = vm, Path = "Multiplier" });
            lblResult1.SetBinding(Label.TextProperty, new Binding() { Source = vm, Path = "Multiplicand", StringFormat = "{0}" });
            lblResult2.SetBinding(Label.TextProperty, new Binding() { Source = vm, Path = "Multiplier", StringFormat = " x {0}" });
            lblResult3.SetBinding(Label.TextProperty, new Binding() { Source = vm, Path = "Product", StringFormat = " = {0}" });

            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Children =
                {
                    entryLeft,
                    sliderLeft,
                    sliderRight,

                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Spacing = 0,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalOptions = LayoutOptions.Center,
                        Children =
                        {
                            lblResult1,
                            lblResult2,
                            lblResult3
                        }
                    }
                    
                }
            };
        }
    }
}
