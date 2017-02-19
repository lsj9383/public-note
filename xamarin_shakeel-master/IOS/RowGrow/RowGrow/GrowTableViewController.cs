using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace RowGrow
{
    public partial class GrowTableViewController : UITableViewController
    {
        public GrowTableViewController(IntPtr handle) : base(handle)
        {

        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            this.TableView.ReloadData();
        }

        public override void DidUpdateFocus(UIFocusUpdateContext context, UIFocusAnimationCoordinator coordinator)
        {
            base.DidUpdateFocus(context, coordinator);

            this.TableView.ReloadData();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.TableView.DataSource = new GrowDataSource();
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.CellAt(indexPath) as GrowCell;

            Console.WriteLine("===========================================");
            Console.WriteLine(String.Format("{4} : ({0}, {1}, {2}, {3})", cell.ViewGrid.Frame.X, cell.ViewGrid.Frame.Y, cell.ViewGrid.Frame.Width, cell.ViewGrid.Frame.Height, indexPath.Row));
            Console.WriteLine(indexPath.Row+" : Height = "+cell.Frame.Height);
            Console.WriteLine("===========================================");
        }

        public override nfloat EstimatedHeight(UITableView tableView, NSIndexPath indexPath)
        {
            Console.WriteLine(indexPath.Row+" : E Height");
            return 380;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            Console.WriteLine(indexPath.Row + " : R Height");
            Console.WriteLine("--------------------------------------------------------");
            return ((GrowDataSource)tableView.DataSource).Get(indexPath).MessageHeight;
        }
    }

    class GrowDataSource : UITableViewDataSource
    {
        List<Message> Data = new List<Message>();

        public GrowDataSource()
        {
            List<string> group1 = new List<string>() { "user1.jpg", "user1.jpg" };
            List<string> group2 = new List<string>() { "user1.jpg", "user1.jpg", "user1.jpg", "user3.jpg", "user3.jpg", "user3.jpg" };
            List<string> group3 = new List<string>() { "user2.jpg", "user2.jpg", "user2.jpg" };
            List<string> group4 = new List<string>() { "user2.jpg", "user2.jpg", "user2.jpg", "user4.jpg", "user5.jpg", "user6.jpg", "user7.jpg" };



            Data.Add(new Message("1", group1).SetContent("Factory method for creating a constraint.Factory method for creating a constraint.Factory method for creating a constraint.Factory method for creating a constraint."));
            Data.Add(new Message("2", null).SetContent("General User Experience (UX) coverage including controls, the designer and UX design principles."));
            Data.Add(new Message("3", null).SetContent("The  MonoTouch.Dialog ( MT.D) toolkit is an indispensable framework for rapid application UI development in Xamarin.iOS. MT.D makes it fast and easy to define complex application UIs using a declarative approach, rather than the tedium of navigation controllers, tables, etc. Additionally, MT.D has a flexible set of APIs that provide developers with a complete control or hands off approach, as well as additional features such as pull-to-refresh, background image loading, search support, and dynamic UI generation via JSON data. This guide introduces the different ways to work with MT.D and then dives deep into advanced usage."));
            Data.Add(new Message("4", group2).SetContent("We have built a designer for the iOS storyboard format which is fully integrated into Xamarin Studio. TThe iOS designer maintains full compatibility with the storyboard format, so that files can be edited in either Xcode or Xamarin Studio. Additionally, the editor supports advanced features, such as custom controls that render at design-time in the editor."));
            Data.Add(new Message("5", null).SetContent("This section covers working with constraints using the iOS Designer, including enabling and disabling Auto Layout and using the Constraints Toolbar."));
            Data.Add(new Message("6", null).SetContent("By default, no constraints are created or visible on the surface. Instead, they are automatically inferred from the frame information at compile time. To add constraints, we need to select an element on the design surface and add constraints to it. We can do that using the Constraint Toolbar."));
            Data.Add(new Message("7", group3).SetContent("Add constraints — This button adds a default set of constraints to the item. It will always add two position constraints (top/bottom + left/right). It will add size constraints if the element has no intrinsic size (e.g. UIButton has full intrinsic size, whereas UITextField only has an intrinsic height)."));
            Data.Add(new Message("8", null).SetContent("Remove constraints — This option will remove all constraints applied to the element and revert to the default compile-time constraint addition."));
            Data.Add(new Message("9", null).SetContent("Occasionally the element frame may get out of sync with newly added constraints (see the [frame misplacement] section for more information on this issue). If this happens, this button will automatically adjust the element frame to match the position the constraints are defining."));
            Data.Add(new Message("10", null).SetContent("In the previous section, we learned to add default constraints and remove constraints using the Constraints Toolbar. For more fine-tuned constraint editing, we can interact with constraints directly on the design surface. This section introduces the basics of surface-based constraint editing, including pin-spacing controls, drop areas, and working with different types of constraints."));
            Data.Add(new Message("11", group4).SetContent("We can double-click on an element to toggle pin-spacing controls and enter the constraint editing mode. The selection handles of the element in constraint mode look like this."));
            Data.Add(new Message("12", null).SetContent("The 4 T-shaped handles on each side of the element define the top, right, bottom, and left edges of the element for a constraint. The two I-shaped handles at the right and bottom of the element define height and width constraint respectively. The middle square handles both centerX and centerY constraints."));
            Data.Add(new Message("13", null).SetContent("The next three sections introduce working with different types of constraints."));
        }

        public Message Get(NSIndexPath indexPath)
        {
            return Data[indexPath.Row];
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("GrowCell") as GrowCell;
            cell.LblTitle.Text = Data[indexPath.Row].Title;
            cell.LblMessage.Text = Data[indexPath.Row].Content;
            cell.ViewGrid.Source = new ImageSource(Data[indexPath.Row].ImagesName);
            cell.FitCollectionSize();       //根据该cell的图像内容，调整其图像容器的大小
            
            Console.WriteLine(indexPath.Row + " : Message.Frame.Width = " + cell.LblMessage.Frame.Width);
            Console.WriteLine(indexPath.Row + " : Message.Frame.Height = " + cell.LblMessage.Frame.Height);
            Console.WriteLine(indexPath.Row + " : ViewGrid.Frame.Y = "+ cell.ViewGrid.Frame.Y);
            
            var imageCells = cell.ViewGrid.VisibleCells;
            if (imageCells.Length == 0)
            {
                Data[indexPath.Row].MessageHeight = cell.ViewGrid.Frame.Y;
            }
            else if (imageCells.Length <= 3)
            {
                Data[indexPath.Row].MessageHeight = cell.ViewGrid.Frame.Y + imageCells[0].Frame.Height;
            }
            else if (imageCells.Length <= 6)
            {
                Data[indexPath.Row].MessageHeight = cell.ViewGrid.Frame.Y + imageCells[3].Frame.Height;
            }
            else
            {
                Data[indexPath.Row].MessageHeight = cell.ViewGrid.Frame.Y + cell.ViewGrid.Frame.Height;
            }

            Console.WriteLine(indexPath.Row + " : Height = " + Data[indexPath.Row].MessageHeight);
            return cell;
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return Data.Count;
        }
    }

    public class ImageSource : UICollectionViewSource
    {
        List<string> ImagesString = new List<string>();

        public ImageSource(List<string> ImagesString)
        {
            this.ImagesString = ImagesString;
        }

        public override nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            if (ImagesString == null)
            {
                return 0;
            }
            else
            {
                return ImagesString.Count;
            }
        }

        public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = collectionView.DequeueReusableCell("ID_ImageCell", indexPath) as ImageCell;
            cell.CellInit();
            cell.ImageView.Image = UIImage.FromFile(ImagesString[indexPath.Row]);
            return cell;
        }
    }
}