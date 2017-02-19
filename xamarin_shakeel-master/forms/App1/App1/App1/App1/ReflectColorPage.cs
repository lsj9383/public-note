using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace App1
{
    public class ReflectColorPage : ContentPage
    {
        public ReflectColorPage()
        {
            //StackLayout stackLayout = new StackLayout { Orientation = StackOrientation.Horizontal};
            StackLayout stackLayout = new StackLayout();

            //typeof(color) 获取color属性的所有成员变量
            //FieldInfo是成员变量的描述信息
            foreach (FieldInfo info in typeof(Color).GetRuntimeFields())
            {
                if (info.GetCustomAttribute<ObsoleteAttribute>() != null)
                {
                    continue;
                }

                if (info.IsPublic && info.IsStatic && info.FieldType == typeof(Color))
                {
                    stackLayout.Children.Add(CreateColorLabel((Color)info.GetValue(null), info.Name));
                }
            }

            Padding = new Thickness(5, Device.OnPlatform(20, 5, 5), 5, 5);
            //            Content = new ScrollView { Orientation = ScrollOrientation.Horizontal, Content = stackLayout };
            Content = new ScrollView { Orientation = ScrollOrientation.Horizontal };   //也就是在StackLayout View的基础上再加一个ScrollView的装饰器，以实现滚动功能
        }

        private Label CreateColorLabel(Color color, string name)
        {
            Color backgroundColor = Color.Default;
            if (color != Color.Default)
            {
                double luminance = 0.3 * color.R + 0.59 * color.G + 0.11 * color.B;
                backgroundColor = luminance > 0.5 ? Color.Black : Color.White;
            }

            return new Label
            {
                Text = name,
                TextColor = color,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                BackgroundColor = backgroundColor
            };
        }
    }
}
