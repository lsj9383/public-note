using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace MVVMApps
{
    public class ColorPage : ContentPage
    {
        public ColorPage()
        {
            ColorViewModel vm = new ColorViewModel();

            BoxView box = new BoxView {Color = Color.Red, HeightRequest=100, WidthRequest=100};

            Slider sldrRed = new Slider();
            Slider sldrGreen = new Slider();
            Slider sldrBlue = new Slider();

            Label lblRed = new Label();
            Label lblGreen = new Label();
            Label lblBlue = new Label();

            box.SetBinding(BoxView.ColorProperty, new Binding { Source = vm, Path="Color" });
           

            sldrRed.SetBinding(Slider.ValueProperty, new Binding { Source = vm, Path = "Red" });
            sldrGreen.SetBinding(Slider.ValueProperty, new Binding { Source = vm, Path = "Green" });
            sldrBlue.SetBinding(Slider.ValueProperty, new Binding { Source = vm, Path = "Blue" });

            lblRed.SetBinding(Label.TextProperty, new Binding { Source = box, Path = "Color.R"});
            lblGreen.SetBinding(Label.TextProperty, new Binding { Source = box, Path = "Color.G" });
            lblBlue.SetBinding(Label.TextProperty, new Binding { Source = box, Path = "Color.B" });

            Content = new StackLayout
            {
                Children = {
                    box,
                    sldrRed,
                    lblRed,

                    sldrGreen,
                    lblGreen,

                    sldrBlue,
                    lblBlue,
                }
            };
        }
    }
}
