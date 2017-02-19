using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SimpleView
{
    class EmptyDataItem
    {
    }

    class HomeDataItem
    {
        public ImageSource ItemImage { get; set; }
        public string ItemTitle { get; set; }
        public string ItemDesc { get; set; }
    }

    class HomeModel
    {
        Dictionary<string, List<object>> datasource = new Dictionary<string, List<object>>();

        public void Add(string grp, object obj)
        {
            if (!datasource.ContainsKey(grp))
            {   //不存在当前组，则添加新组
                datasource.Add(grp, new List<object>());                
            }
            datasource[grp].Add(obj);
        }

        public List<object> Model()
        {
            var model = new List<object>();
            foreach (var grp in datasource)
            {
                model.Add(new EmptyDataItem());
                model.AddRange(grp.Value);  //将grp中的List添加到model的底部
            }
            return model;
        }
    }
}
