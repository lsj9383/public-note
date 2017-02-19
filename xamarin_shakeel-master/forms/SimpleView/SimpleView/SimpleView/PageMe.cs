using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace SimpleView
{
    public class PageMe : ContentPage
    {
        public PageMe()
        {
            Title = "我的";
            Content = new Label
            {
                Text = "无内容",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };
        }
    }
}
