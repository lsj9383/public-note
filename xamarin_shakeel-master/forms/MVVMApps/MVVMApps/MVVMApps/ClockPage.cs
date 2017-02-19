using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace MVVMApps
{
    public class ClockPage : ContentPage
    {
        public ClockPage()
        {
            DateTimeViewModel dateVm= new DateTimeViewModel();

            Label lblTime = new Label();
            lblTime.SetBinding(Label.TextProperty, new Binding() { Source = dateVm, Path = "DateTime", StringFormat="This is {0}" });

            Content = lblTime;
        }
    }
}
