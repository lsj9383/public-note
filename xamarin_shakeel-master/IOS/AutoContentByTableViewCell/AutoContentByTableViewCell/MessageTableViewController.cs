using CoreGraphics;
using Foundation;
using System;
using UIKit;

namespace AutoContentByTableViewCell
{
    public partial class MessageTableViewController : UITableViewController
    {
        public MessageTableViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            
            this.TableView.DataSource = new MessageDataSource();
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            /*
            var Cell = (tableView.DataSource as MessageDataSource).CacheCell;
            Console.WriteLine(Cell.TextViewMessage.Frame.ToString());
            Cell.TextSizeAutoFit();
            if (tableView.CellAt(indexPath) != null)
            {
                Console.WriteLine("Hello...");
            }
            */
            Console.WriteLine(indexPath.ToString() + " GetHeightForRow");
            return 200;
        }

    }

    public class MessageDataSource : UITableViewDataSource
    {
        public MessageCell CacheCell;

        public MessageDataSource()
        {
        }


        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            MessageCell cell = tableView.DequeueReusableCell("MessageCell") as MessageCell;

            cell.LblMessage.Text = "1234567";
            cell.Initial();
            CacheCell = cell;
            CGSize size = new CGSize(cell.LblMessage.Frame.Width, float.MaxValue);
            CGSize auto_size = cell.LblMessage.SizeThatFits(size);
            cell.LblMessage.Frame = new CGRect(cell.LblMessage.Frame.X, cell.LblMessage.Frame.Y, size.Width, auto_size.Height);
            var label = cell.LblMessage;
            CGRect txtFrame = label.Frame;
            label.Frame = new CGRect(10, 100, 300, 0);
            
            /*
             CGRect txtFrame = label.frame;
     
    label.frame = CGRectMake(10, 100, 300,
                             txtFrame.size.height =[label.text boundingRectWithSize:
                                                    CGSizeMake(txtFrame.size.width, CGFLOAT_MAX)
                                                                            options:NSStringDrawingUsesLineFragmentOrigin | NSStringDrawingUsesFontLeading
                                                                         attributes:[NSDictionary dictionaryWithObjectsAndKeys:label.font,NSFontAttributeName, nil] context:nil].size.height);
    label.frame = CGRectMake(10, 100, 300, txtFrame.size.height);
     
    [self.view addSubview:label];
             */


            return cell;
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return 1;
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return 1;
        }
    }
}