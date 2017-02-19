using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace App1
{
    public class TwoButtonsPage : ContentPage
    {
        Button addButton, removeButton;
        StackLayout loggerLayout = new StackLayout();

        public TwoButtonsPage()
        {

            addButton = new Button { Text = "Add", HorizontalOptions = LayoutOptions.CenterAndExpand, BackgroundColor = Color.Yellow };
            addButton.Clicked += OnButtonClicked;

            removeButton = new Button { Text = "Remove", HorizontalOptions = LayoutOptions.CenterAndExpand, BackgroundColor = Color.Yellow };
            removeButton.Clicked += OnButtonClicked;

            Padding = new Thickness(5, Device.OnPlatform(20, 0, 0), 5, 0);
            Content = new StackLayout
            {
                BackgroundColor = Color.Blue,
                Children = {
                    new StackLayout {
                        Spacing = 0,
            //               Orientation = StackOrientation.Horizontal,
                        Children = {
                            addButton,
                            new Label { Text="hello", BackgroundColor = Color.Red},
                            removeButton,
                            new Label { Text="world", BackgroundColor = Color.Red},
                        }
                    },
                    new ScrollView {
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        Content = loggerLayout
                    }
                }
            };
        }

        private void OnButtonClicked(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            if (button == addButton)
            {
                loggerLayout.Children.Add(new Label { Text = "Button clicked at" + DateTime.Now.ToString() });
            }
            else
            {
                if (loggerLayout.Children.Count != 0)
                {
                    loggerLayout.Children.RemoveAt(0);
                }
                
            }
        }
    }
}
