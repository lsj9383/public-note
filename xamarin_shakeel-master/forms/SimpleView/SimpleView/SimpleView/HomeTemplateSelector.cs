using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SimpleView
{

    class EmptyCell : ViewCell
    {
        public EmptyCell()
        {
            View = new ContentView() { HeightRequest = 45 };
            View.GestureRecognizers.Add(new TapGestureRecognizer());
        }
    }

    class HomeCell : ViewCell
    {
        public HomeCell()
        {
            var image = new Image();
            var labelTitle = new Label();
            var labelDescription = new Label();

            //set bindings
            image.SetBinding(Image.SourceProperty, "ItemImage");
            labelTitle.SetBinding(Label.TextProperty, "ItemTitle");
            labelDescription.SetBinding(Label.TextProperty, "ItemDesc");

            //layout
            image.Aspect = Aspect.AspectFill;
            image.WidthRequest = 48;
            image.HeightRequest = 48;

            labelTitle.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            labelTitle.VerticalOptions = LayoutOptions.Center;
            labelTitle.VerticalTextAlignment = TextAlignment.Center;
            labelTitle.FontAttributes = FontAttributes.Bold;

            labelDescription.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label));

            var leftLayout = new StackLayout
            {
                Children =
                {
                    labelTitle,
                    labelDescription
                }
            };

            var mainLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    image,
                    leftLayout
                }
            };

            View = mainLayout;
        }
    }

    class HomeTemplateSelector : DataTemplateSelector
    {
        private DataTemplate m_dataTemplateEmptyCell;
        private DataTemplate m_dataTemplateHomeCell;

        public HomeTemplateSelector()
        {
            m_dataTemplateEmptyCell = new DataTemplate(typeof(EmptyCell));
            m_dataTemplateHomeCell = new DataTemplate(typeof(HomeCell));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is EmptyDataItem)
            {
                return m_dataTemplateEmptyCell;
            }
            else if (item is HomeDataItem)
            {
                return m_dataTemplateHomeCell;
            }
            else
            {
                return null;
            }
        }
    }
}
