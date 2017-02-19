using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace CollectionViews
{
    public class SimpleListView : ContentPage
    {
        public SimpleListView()
        {
            ListView listView = new ListView { SeparatorVisibility=SeparatorVisibility.None};

            listView.ItemsSource = new List<Color>
            {
                Color.Aqua, Color.Black,    Color.Blue, Color.Fuchsia,
                Color.Gray, Color.Green,    Color.Lime, Color.Maroon,
                Color.Navy, Color.Olive,    Color.Pink, Color.Purple,
                Color.Red,  Color.Silver,   Color.Teal, Color.White,
                Color.Yellow
            };

            Content = listView;
        }
    }
}
