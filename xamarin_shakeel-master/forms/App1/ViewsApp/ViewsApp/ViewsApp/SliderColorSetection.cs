using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace ViewsApp
{
    public class SliderColorSetection : ContentPage
    {
        Slider redSlider;
        Slider greenSlider;
        Slider blueSlider;
        Label redLabel;
        Label greenLabel;
        Label blueLabel;
        BoxView boxView;

        public SliderColorSetection()
        {
            redSlider = new Slider() { Maximum = 255, Minimum = 0 };
            redSlider.ValueChanged += OnSliderValueChanged;
            redLabel = new Label() { FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)), HorizontalTextAlignment = TextAlignment.Center };

            greenSlider = new Slider() { Maximum = 255, Minimum = 0 };
            greenSlider.ValueChanged += OnSliderValueChanged;
            greenLabel = new Label() { FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)), HorizontalTextAlignment = TextAlignment.Center };

            blueSlider = new Slider() { Maximum = 255, Minimum = 0 };
            blueSlider.ValueChanged += OnSliderValueChanged;
            blueLabel = new Label() { FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)), HorizontalTextAlignment = TextAlignment.Center };

            boxView = new BoxView { VerticalOptions=LayoutOptions.FillAndExpand };
            Content = new StackLayout
            {
                Children =
                {
                    redSlider,
                    redLabel,
                    greenSlider,
                    greenLabel,
                    blueSlider,
                    blueLabel,
                    boxView
                }
            };
        }

        private void OnSliderValueChanged(object sender, EventArgs args)
        {
            if (sender == redSlider)
            {
                redLabel.Text = String.Format("Red = {0:X2}", (int)redSlider.Value);
            }
            else if (sender == greenSlider)
            {
                greenLabel.Text = String.Format("Green = {0:X2}", (int)greenSlider.Value);
            }
            else if (sender == blueSlider)
            {
                blueLabel.Text = String.Format("Blue = {0:X2}", (int)blueSlider.Value);
            }

            boxView.Color = Color.FromRgb(  (int)redSlider.Value,
                                            (int)greenSlider.Value,
                                            (int)blueSlider.Value);
        }
    }
}
