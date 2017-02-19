using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace CollectionViews
{
    public class Employee
    {
        public string DisplayName { get; set; }
    }



    public class ListViewSource : ContentPage
    {
        public ListViewSource()
        {
            var listView = new ListView();

            ObservableCollection<Employee> employees = new ObservableCollection<Employee>();
            listView.ItemsSource = employees;

            employees.Add(new Employee { DisplayName = "Rob Finnerty" });
            employees.Add(new Employee { DisplayName = "Bill Wrestler" });
            employees.Add(new Employee { DisplayName = "Dr. Geri-Beth Hooper" });
            employees.Add(new Employee { DisplayName = "Dr. Keith Joyce-Purdy" });
            employees.Add(new Employee { DisplayName = "Sheri Spruce" });
            employees.Add(new Employee { DisplayName = "Burt Indybrick" });

            Content = listView;
        }
    }
}
