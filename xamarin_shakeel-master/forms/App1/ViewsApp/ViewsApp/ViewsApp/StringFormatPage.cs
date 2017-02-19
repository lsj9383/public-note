using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace ViewsApp
{
    public class StringFormatPage : ContentPage
    {
        public StringFormatPage()
        {
            Label label = new Label();
            Slider slider = new Slider();

            label.BindingContext = slider;
            label.SetBinding(Label.TextProperty, "Value", stringFormat: "Slider Value Is {0:F3}");

            Content = new StackLayout
            {
                Children =
                {
                    label,
                    slider
                }
            };
        }
    }
}
