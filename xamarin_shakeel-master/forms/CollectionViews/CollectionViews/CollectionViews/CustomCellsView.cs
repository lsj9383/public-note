using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace CollectionViews
{
    public class CustomCellsView : ContentPage
    {
        public CustomCellsView()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello Page" }
                }
            };
        }

        public class CustomCell : ViewCell
        {
            public CustomCell()
            {
                var image = new Image();
                StackLayout cellWrapper = new StackLayout();
                StackLayout horizontalLayout = new StackLayout();
                Label left = new Label();
                Label right = new Label();

                //set bindings
                left.SetBinding(Label.TextProperty, "title");
                right.SetBinding(Label.TextProperty, "subtitle");
                image.SetBinding(Image.SourceProperty, "image");

                //Set properties for desired design
                cellWrapper.BackgroundColor = Color.FromHex("#eee");
                horizontalLayout.Orientation = StackOrientation.Horizontal;
                right.HorizontalOptions = LayoutOptions.EndAndExpand;
                left.TextColor = Color.FromHex("#f35e20");
                right.TextColor = Color.FromHex("503026");

                //add views to the view hierarchy
                horizontalLayout.Children.Add(image);
                horizontalLayout.Children.Add(left);
                horizontalLayout.Children.Add(right);
                cellWrapper.Children.Add(horizontalLayout);
                View = cellWrapper;
            }
        }
    }
}
