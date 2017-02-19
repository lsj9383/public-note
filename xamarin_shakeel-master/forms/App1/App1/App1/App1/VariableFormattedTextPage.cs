using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace App1
{
    public class VariableFormattedTextPage : ContentPage
    {
        public VariableFormattedTextPage()
        {
            /*
            FormattedString formattedString = new FormattedString();
            formattedString.Spans.Add(new Span { Text = "I " });
            formattedString.Spans.Add(new Span { Text = "LOVE ",FontSize=Device.GetNamedSize(NamedSize.Large,typeof(Label)), FontAttributes = FontAttributes.Bold});
            formattedString.Spans.Add(new Span { Text = "Xamarin.Forms!" });
            Content = new Label
            {
                FormattedText = formattedString,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            };
            */
            Content = new Label
            {
                FormattedText = new FormattedString
                {
                    Spans =
                    {
                        new Span { Text = "I " },
                        new Span { Text = "LOVE ",FontSize=Device.GetNamedSize(NamedSize.Large,typeof(Label)), FontAttributes = FontAttributes.Bold},
                        new Span { Text = "Xamarin.Forms!" }
                    }
                },
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            };
            
        }
    }
}
