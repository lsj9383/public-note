using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Xamarin.Forms;

namespace App2
{
    public class GreetingPage : ContentPage
    {
        public GreetingPage()
        {
            StackLayout stackLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Spacing = 2
         
              
            };
            var colors = new[]
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
                new { value = Color.Blue, name = "Blue" ,back = Color.Black},
                new { value = Color.Navy, name = "Navy" ,back = Color.Black},
                new { value = Color.Pink, name = "Pink" ,back = Color.Black},
                new { value = Color.Fuchsia, name = "Fuchsia" ,back = Color.Black},
                new { value = Color.Purple, name = "Purple" ,back = Color.Black},
                new { value = Color.White, name = "White" ,back = Color.Black},
                new { value = Color.Silver, name = "Silver" ,back = Color.Black},
                new { value = Color.Gray, name = "Gray" ,back = Color.Black},
                new { value = Color.Black, name = "Black" ,back = Color.Black},
                new { value = Color.Red, name = "Red" ,back = Color.Black},
                new { value = Color.Maroon, name = "Maroon" ,back = Color.Black},
                new { value = Color.Yellow, name = "Yellow" ,back = Color.Black},
                new { value = Color.Olive, name = "Olive" ,back = Color.Black},
                new { value = Color.Lime, name = "Lime" ,back = Color.Black},
                new { value = Color.Green, name = "Green" ,back = Color.Black},
                new { value = Color.Aqua, name = "Aqua" ,back = Color.Black},
                new { value = Color.Teal, name = "Teal" ,back = Color.Black},
                new { value = Color.Blue, name = "Blue" ,back = Color.Black},
                new { value = Color.Navy, name = "Navy" ,back = Color.Black},
                new { value = Color.Pink, name = "Pink" ,back = Color.Black},
                new { value = Color.Fuchsia, name = "Fuchsia" ,back = Color.Black},
                new { value = Color.Purple, name = "Purple" ,back = Color.Black}
            };
           
            foreach (var color in colors)
            {
                stackLayout.Children.Add(
                new Label
                {
                    Text = color.name,
                    TextColor = color.value,
                    FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                    BackgroundColor = color.back
                });
            }
            Content = new ScrollView
            { 
                Orientation = ScrollOrientation.Vertical,
                Content = stackLayout
            };
       
        }
    }
}
