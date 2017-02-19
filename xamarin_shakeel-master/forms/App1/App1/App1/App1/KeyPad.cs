using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace App1
{
    public class KeyPad : ContentPage
    {
        Label displayLabel;
        Button backspaceButton;

        public KeyPad()
        {
            StackLayout mainStack = new StackLayout { VerticalOptions=LayoutOptions.Center, HorizontalOptions=LayoutOptions.Center};

            displayLabel = new Label
            {
                FontSize = Device.GetNamedSize((NamedSize.Large), typeof(Label)),
                VerticalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.End
            };
            mainStack.Children.Add(displayLabel);

            backspaceButton = new Button
            {
                Text = "\u21E6",
                FontSize = Device.GetNamedSize((NamedSize.Large), typeof(Button)),
                IsEnabled = false
            };
            backspaceButton.Clicked += OnBackspaceButtonClicked;
            mainStack.Children.Add(backspaceButton);

            StackLayout rowStack = null;
            for (int num = 1; num <= 10; num++)
            {
                //每一行开始，创建一个新的按键
                if ((num - 1) % 3 == 0)
                {
                    rowStack = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal
                    };
                    mainStack.Children.Add(rowStack);
                }

                //创建按钮并添加到行中
                Button digitButton = new Button
                {
                    Text = (num % 10).ToString(),
                    FontSize = Device.GetNamedSize((NamedSize.Large), typeof(Button)),
                    StyleId = 1.ToString()
                };
                digitButton.Clicked += OnDigitButtonClicked;
                if (num == 10)
                {
                    digitButton.HorizontalOptions = LayoutOptions.FillAndExpand;
                }
                rowStack.Children.Add(digitButton);
            }

            Content = mainStack;
        }

        private void OnDigitButtonClicked(object sender, EventArgs args)
        {
            var view = (View)sender;

            displayLabel.Text += view.StyleId;
            backspaceButton.IsEnabled = true;
        }

        private void OnBackspaceButtonClicked(object sender, EventArgs args)
        {
            string text = displayLabel.Text;
            displayLabel.Text = text.Substring(0, text.Length - 1);
            backspaceButton.IsEnabled = displayLabel.Text.Length > 0;
        }
    }
}
