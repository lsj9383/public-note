using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace App2
{
    public class VerticalOptionsDemoPage : ContentPage
    {
        public VerticalOptionsDemoPage()
        {
            var colors = new []
            {
                new { value = Color.White, name = "White",back = Color.Black},
                new { value = Color.Silver, name = "Silver" ,back = Color.Black},
                new { value = Color.Gray, name = "Gray",back = Color.Black },
                new { value = Color.Pink, name = "Black",back = Color.Black },
                new { value = Color.Red, name = "Red",back = Color.Black },
                new { value = Color.Maroon, name = "Maroon" ,back = Color.Black},
                new { value = Color.Yellow, name = "Yellow" ,back = Color.Black},
                new { value = Color.Olive, name = "Olive",back = Color.Black },
                new { value = Color.Lime, name = "Lime" ,back = Color.Black},
                new { value = Color.Green, name = "Green" ,back = Color.Black},
                new { value = Color.Aqua, name = "Aqua",back = Color.Black },
                new { value = Color.Teal, name = "Teal" ,back = Color.Black},
                new { value = Color.Blue, name = "Blue" ,back = Color.Black}
            };
            StackLayout stacklayout = new StackLayout();
            foreach(var color in colors)
            {
                stacklayout.Children.Add(CreateColorView(color.value, color.name));
            }

            Content = new ScrollView
            {
                Content = stacklayout
            };
        }
        View CreateColorView(Color color, string name)
        {
           
            return new Frame
            {
                OutlineColor = Color.Accent,
                Padding = new Thickness(5),
                Content = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Spacing = 15,
                    Children =
                    {
                        new BoxView
                        {
                            Color = color
                        },
                        new Label
                        {
                            Text = name,
                            FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                            FontAttributes = FontAttributes.Bold,
                            VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.StartAndExpand
                        },
                        new StackLayout
                        {
                            Children =
                            {
                                new Label
                                {
                                    Text = String.Format("{0:X2}-{1:X2}-{2:X2}",
                                    (int)(255 * color.R),
                                    (int)(255 * color.G),
                                    (int)(255 * color.B)),
                                    VerticalOptions = LayoutOptions.CenterAndExpand,
                                    IsVisible = color != Color.Default
                                },
                                new Label
                                {
                                    Text = String.Format("{0:F2}, {1:F2}, {2:F2}",
                                    color.Hue,
                                    color.Saturation,
                                    color.Luminosity),
                                    VerticalOptions = LayoutOptions.CenterAndExpand,
                                    IsVisible = color != Color.Default
                                }
                            },
                          HorizontalOptions = LayoutOptions.End
                       }
                    }
                }
            };
        }

    }
}
