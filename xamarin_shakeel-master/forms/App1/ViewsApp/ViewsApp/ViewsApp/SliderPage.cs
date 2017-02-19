using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace ViewsApp
{
    public class SliderPage : ContentPage
    {
        Slider slider;
        Label label;
        public SliderPage()
        {
            slider = new Slider
            {
                Maximum = 100,
                Minimum = 1,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            label = new Label
            {
                Text = String.Format("Slider = {0}", slider.Value),
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            slider.ValueChanged += (sender, args) =>
            {
                label.Text = String.Format("Slider = {0}", args.NewValue);
            };

            Content = new StackLayout
            {
                Children = {
                    slider,
                    label
                }
            };
        }
    }
}
