using System;
using Foundation;
using UIKit;
using System.Collections.Generic;
using System.Drawing;

namespace TableView
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
            List<string> prem1  = new List<string>() { "LIVERPOOL", "EVERTON", "ARSENAL" };
            List<string> prem2  = new List<string>() { "Comput", "Math", "China" };
            List<string> prem3  = new List<string>() { "Lambda", "Alpha", "Beta" };
            List<string> titles = new List<string>() { "ONE", "TWO", "THREE"};
            tableView.Source = new MyViewSource(prem1, prem2, prem3,  titles);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        private class MyViewSource : UITableViewSource
        {
            private List<string> Team1;
            private List<string> Team2;
            private List<string> Team3;
            private List<string> TeamTitles;

            public MyViewSource(List<string> prems1, List<string> prems2, List<string> prems3, List<string> Titles )
            {
                Team1 = prems1;
                Team2 = prems2;
                Team3 = prems3;
                TeamTitles = Titles;
            }

            public override nint NumberOfSections(UITableView tableView)
            {
                return 3;
            }

            public override nint RowsInSection(UITableView tableview, nint section)
            {   //设置每节中的行数
                switch (section)
                {
                    case 0:
                        return Team1.Count;
                    case 1:
                        return Team2.Count;
                    default:
                        return Team3.Count;
                }
            }

            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {   //设置每行中的内容
                UITableViewCell theCell = null;
                switch (indexPath.Section)
                {
                    case 0:
                        theCell = new UITableViewCell();
                        theCell.TextLabel.Text = Team1[indexPath.Row];
                        theCell.Accessory = UITableViewCellAccessory.Checkmark;
                        break;
                    case 1:
                        theCell = new UITableViewCell();
                        theCell.TextLabel.Text = Team2[indexPath.Row];
                        UISwitch myswitch = new UISwitch();
                        myswitch.Frame = new RectangleF(0, 0, 50, 20);
                        theCell.AccessoryView = myswitch;
                        break;
                    default:
                        theCell = (CustomCell)tableView.DequeueReusableCell(CustomCell.CELLID) ;
                        ((CustomCell)theCell).labelTitle.Text = Team3[indexPath.Row]; break;
                        
                }
                return theCell;
            }

            public override string TitleForHeader(UITableView tableView, nint section)
            {
                return TeamTitles[(int)section];
            }
        }
    }
}