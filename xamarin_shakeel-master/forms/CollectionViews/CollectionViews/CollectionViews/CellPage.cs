using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace CollectionViews
{
    public class CellPage : ContentPage
    {
        ObservableCollection<Employee> employees = new ObservableCollection<Employee>();
        public CellPage()
        {
            var EmployeeView = new ListView();
            EmployeeView.ItemsSource = employees;

            employees.Add(new Employee { DisplayName = "Rob Finnerty" });
            employees.Add(new Employee { DisplayName = "Bill Wrestler" });
            employees.Add(new Employee { DisplayName = "Dr. Geri-Beth Hooper" });
            employees.Add(new Employee { DisplayName = "Dr. Keith Joyce-Purdy" });
            employees.Add(new Employee { DisplayName = "Sheri Spruce" });
            employees.Add(new Employee { DisplayName = "Burt Indybrick" });

            var DataTemplate = new DataTemplate(typeof(TextCell));          //选择一个单元格模板
            DataTemplate.SetBinding(TextCell.TextProperty, "DisplayName");  //设置单元格与ItemSource中每个Item的数据绑定
            EmployeeView.ItemTemplate = DataTemplate;

           
            Content = EmployeeView;
        }
    }
    
}
