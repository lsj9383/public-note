using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace ViewsApp
{
    public class BindingModePage : ContentPage
    {
        public BindingModePage()
        {
            Label label = new Label
            {
                Text = "hello"
            } ;

            Slider slider = new Slider
            {
                Maximum = 100
            };

            slider.SetBinding(Slider.ValueProperty, new Binding { Source=label, Path="FontSize"});

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
