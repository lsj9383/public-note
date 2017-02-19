using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace CollectionViews
{
    public class IOPage : ContentPage
    {
        public IOPage()
        {
            StackLayout mainStack = new StackLayout();
            StackLayout textStack = new StackLayout
            {
                Padding = new Thickness(5),
                Spacing = 10
            };

            Assembly assembly = GetType().GetTypeInfo().Assembly;
            string resource = "CollectionViews.Text.1.txt";
            using (Stream stream = assembly.GetManifestResourceStream(resource))
            {
                StreamReader reader = new StreamReader(stream);

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Label label = new Label
                    {
                        Text = line,
                        TextColor = Color.Black
                    };
                    textStack.Children.Add(label);
                }

                Content = new StackLayout
                {
                    Children =
                    {
                        new Label { Text="Title" },
                        new ScrollView { Content = textStack }
                    }
                };
            }
            
        }
    }
}