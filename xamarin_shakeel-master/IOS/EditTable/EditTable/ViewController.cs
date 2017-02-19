using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace EditTable
{
    public partial class ViewController : UIViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            tableView.Source = new myViewSource(new List<string>() { "one", "two", "three", "four", "five"});
            tableView.SetEditing(true, true);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        private class myViewSource : UITableViewSource
        {
            private List<string> items;

            public myViewSource(List<string> items)
            {
                this.items = items;
            }

            public override nint RowsInSection(UITableView tableview, nint section)
            {
                return items.Count;
            }

            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                UITableViewCell cell = new UITableViewCell();
                cell.TextLabel.Text = items[indexPath.Row];
                return cell;
            }

            public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
            {   //选中行触发
                UITableViewCell cell = tableView.CellAt(indexPath);
                if (cell.Accessory == UITableViewCellAccessory.None)
                {
                    cell.Accessory = UITableViewCellAccessory.Checkmark;
                }
                else
                {
                    cell.Accessory = UITableViewCellAccessory.None;
                    tableView.DeselectRow(indexPath, true);             //再次点击将会触发执行取消选中
                }
            }

            public override void RowDeselected(UITableView tableView, NSIndexPath indexPath)
            {   //编辑行触发
                UITableViewCell cell = tableView.CellAt(indexPath);
                if (cell.Accessory == UITableViewCellAccessory.Checkmark)
                {
                    cell.Accessory = UITableViewCellAccessory.None;
                }
            }

            public override UITableViewCellEditingStyle EditingStyleForRow(UITableView tableView, NSIndexPath indexPath)
            {
                return UITableViewCellEditingStyle.Delete;
            }

            public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
            {
                Console.WriteLine("Editing Style : "+editingStyle.ToString());
                if (editingStyle == UITableViewCellEditingStyle.Delete)
                {
                    items.RemoveAt(indexPath.Row);  //在数据源中删除要删除的数据
                    tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Automatic);
                }
                else if (editingStyle == UITableViewCellEditingStyle.Insert)
                {
                    string s = string.Format("Inserted item : {0}", items.Count + 1);
                    items.Insert(indexPath.Row, s);
                    tableView.InsertRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Automatic);
                }
            }
        }
    }
}