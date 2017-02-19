using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace CollectionViews
{
    public class ListViewArray : ContentPage
    {
        public ListViewArray()
        {
            ListView listView = new ListView
            {
                ItemsSource = { }
            };

            BoxView box = new BoxView{ HeightRequest=100 };
            box.SetBinding(BoxView.ColorProperty, new Binding { Source=null, Path="SelectedItem"});

            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello Page" }
                }
            };
        }
    }
}
