using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace App1
{
    public class GreetingsPage : ContentPage
	{
		public GreetingsPage()
        {
            this.Content = new Label
            {
                Text = "Greetings, Xamarin.Forms",
                HorizontalOptions = LayoutOptions.Center,
                //                VerticalOptions = LayoutOptions.Center,
                //HorizontalTextAlignment = TextAlignment.Center,
                //VerticalTextAlignment = TextAlignment.Center,
                BackgroundColor = Color.Red              
            };

//            Padding = new Thickness(0, 200, 0, 0);
        }
    }
}
