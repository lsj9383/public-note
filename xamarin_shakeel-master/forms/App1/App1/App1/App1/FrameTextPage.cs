using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace App1
{
    public class FrameTextPage : ContentPage
    {
        public FrameTextPage()
        {
            Padding = new Thickness(20);
            BackgroundColor = Color.Aqua;
            Content = new Frame
            {
                OutlineColor = Color.Black,
                BackgroundColor = Color.Yellow,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Content = new Label
                {
                    Text = "I'v been framed",
                    FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
//                    HorizontalOptions = LayoutOptions.Center,
//                    VerticalOptions = LayoutOptions.Center
                    FontAttributes = FontAttributes.Italic,
                    TextColor = Color.Blue,
                    BackgroundColor = Color.Red
                }
            };
        }
    }
}
