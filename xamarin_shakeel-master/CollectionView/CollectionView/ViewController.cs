using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace CollectionView
{
    public partial class ViewController : UIViewController
    {
        private List<UIImage> collectionData;

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            collectionData = new List<UIImage>();
            // Perform any additional setup after loading the view, typically from a nib.
            for (int i = 0; i < 50; i++)
            {
                collectionData.Add(UIImage.FromFile("red-user.png"));  //添加图像
            }
            collectionView.Source = new CollectionSource(this);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        private class CollectionSource : UICollectionViewSource
        {
            private ViewController parentController;
            private List<UIImage> collectionData;

            public CollectionSource(ViewController parent)
            {
                parentController = parent;
                collectionData = parent.collectionData;
            }
            
            public override nint GetItemsCount(UICollectionView collectionView, nint section)
            {
                return this.collectionData.Count;
            }

            public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
            {
                Console.WriteLine(indexPath.ToString());
                int rowIndex = indexPath.Row;
                ImageCell cell = (ImageCell)collectionView.DequeueReusableCell(ImageCell.CELLID, indexPath);
                cell.ImageView.Image = collectionData[rowIndex];
                return cell;
            }
        }
    }
}