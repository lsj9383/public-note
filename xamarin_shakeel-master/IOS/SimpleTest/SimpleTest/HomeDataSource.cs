using System;
using System.Collections.Generic;
using System.Text;
using Foundation;
using UIKit;

namespace SimpleTest
{
    class HomeCustomData
    {
        public string Title { get; private set; }
        public string Descp { get; private set; }
        
        public HomeCustomData(string aTitle, string aDescp)
        {
            Title = aTitle;
            Descp = aDescp;
        }
    }

    class HomeDataSource : UITableViewDataSource
    {
        private List<string> Titles;
        private Dictionary<string, List<object>> DataSource;
        bool isDisplayTitles = false;

        public HomeDataSource(bool isDisplayTitles)
        {
            this.isDisplayTitles = isDisplayTitles;
            Titles = new List<string>();
            DataSource = new Dictionary<string, List<object>>();
        }

        public void Add(string Key, object item)
        {
            if (!DataSource.ContainsKey(Key))
            {
                Titles.Add(Key);
                DataSource.Add(Key, new List<object>());
            }

            List<object> items;
            DataSource.TryGetValue(Key, out items);
            items.Add(item);
        }

        public object Get(NSIndexPath indexPath)
        {
            int Section = indexPath.Section;
            int Row = indexPath.Row;
            string Key = Titles[Section];
            List<object> Items = null;
            DataSource.TryGetValue(Key, out Items);

            object Item = Items[Row];

            return Item;
        }

        public override nint NumberOfSections(UITableView tableView)
        {   //组数
            return Titles.Count;
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return DataSource[Titles[(int)section]].Count;
        }

        public override string TitleForHeader(UITableView tableView, nint section)
        {
            return null;
        }

        

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            //取得某组的某个数据
            object Item = Get(indexPath);

            if (Item is string)
            {
                UITableViewCell Cell = new UITableViewCell();
                Cell.TextLabel.Text = Item as string;
                return Cell;
            }
            else if (Item is HomeCustomData)
            {
                HomeCustomCell Cell = tableView.DequeueReusableCell(HomeCustomCell.Identifier) as HomeCustomCell;
                Cell.LabelDescp.Text = ((HomeCustomData)Item).Descp;
                Cell.LabelTitle.Text = ((HomeCustomData)Item).Title;
                Cell.ImageView.Image = UIImage.FromFile("red-star.png");
                return Cell;
            }
            else
            {
                UITableViewCell Cell = new UITableViewCell();
                Cell.TextLabel.Text = "error";
                return Cell;
            }

        }
    }
}
