using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace App1
{
    public class ButtonLoggerPage : ContentPage
    {
        StackLayout loggerLayout = new StackLayout();
        public ButtonLoggerPage()
        {
            Button button = new Button { Text="Log the Click Time" };
            button.Clicked += (sender, e) =>
            {
                loggerLayout.Children.Add(new Label { Text = "Button clicked at " + DateTime.Now.ToString("T")});
            };

            Content = new StackLayout
            {
                Children = {
                    button,
                    new ScrollView
                    {
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        Content = loggerLayout
                    }
                }
            };
        }
    }
}
