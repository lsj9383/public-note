using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace ViewsApp
{
    public class BindingPage : ContentPage
    {
        public BindingPage()
        {
            Label label = new Label
            {
                Text = "Binding Demo",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.Center
            };

            Slider slider1 = new Slider
            {
                VerticalOptions = LayoutOptions.Center
            };

            Slider slider2 = new Slider
            {
                VerticalOptions = LayoutOptions.Center,
                Maximum = 100
            };

            Binding binding = Binding.Create<Slider>( (src)=> src.Value );
            binding.Source = slider1;

            label.SetBinding(Label.OpacityProperty, new Binding { Source = slider1, Path = "Value" });
            label.SetBinding(Label.FontSizeProperty, binding);




//            label.SetBinding(Label.FontSizeProperty, new Binding { Source = slider2, Path = "Value" });

            Content = new StackLayout
            {
                Children = { label, slider1, slider2 }
            };
        }
    }
}
