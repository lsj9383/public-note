using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace MVVMApps
{
    public class BtnPage : ContentPage
    {
        public BtnPage()
        {
            PowersViewModel vm = new PowersViewModel(2);

            Label label = new Label();

            Button btnIncrease = new Button
            {
                Text = "Increase",
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            Button btnDecrease = new Button
            {
                Text = "Decrease",
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            label.SetBinding(Label.TextProperty, new Binding { Source=vm, Path= "Power", StringFormat="Result = {0}" });
            btnIncrease.SetBinding(Button.CommandProperty, new Binding() { Source = vm, Path = "IncreaseExponentCommand" });
            btnDecrease.SetBinding(Button.CommandProperty, new Binding() { Source = vm, Path = "DecreaseExponentCommand" });

            Content = new StackLayout
            {
                Children = {
                    label,
                    btnIncrease,
                    btnDecrease
                }
            };
        }
    }
}
