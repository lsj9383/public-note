using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace ViewsApp
{
    public class GridCodeDemoPage : ContentPage
    {
        public GridCodeDemoPage()
        {
            Grid grid = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = new GridLength(100) },
                    new RowDefinition { Height = new GridLength(2, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
                }
            };

            grid.Children.Add(new Label
            {
                Text = "Grid Demo",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            });

            grid.Children.Add(new Label
            {
                Text = "Grid Demo",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            }, 1, 0);

            grid.Children.Add(new Label
            {
                Text = "Grid Demo",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            }, 0, 2, 1, 2);

            BoxView boxView1 = new BoxView { Color = Color.Green };
            Grid.SetRow(boxView1, 2);
            Grid.SetColumn(boxView1, 0);
            grid.Children.Add(boxView1);

            BoxView boxView2 = new BoxView { Color = Color.Red};
            Grid.SetRow(boxView2, 2);
            Grid.SetColumn(boxView2, 1);
            Grid.SetRowSpan(boxView2, 2);
            grid.Children.Add(boxView2);

            BoxView boxView3 = new BoxView { Color = Color.Blue, Opacity=0.5 };
            Grid.SetRow(boxView3, 3);
            Grid.SetColumn(boxView3, 0);
            Grid.SetRowSpan(boxView3, 2);
            grid.Children.Add(boxView3);

            Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0);
            Content = grid;
        }
    }
}
