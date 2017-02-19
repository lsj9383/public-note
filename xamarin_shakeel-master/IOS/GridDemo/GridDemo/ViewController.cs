using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace GridDemo
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
            // Perform any additional setup after loading the view, typically from a nib.

            collectionData = new List<UIImage>();
            for (int i = 0; i < 50; i++)
            {
                collectionData.Add(UIImage.FromFile("user1.jpg"));
            }

            this.collectionView.Source = new CollectionSource(this);
        }

        private class CollectionSource : UICollectionViewSource
        {   //网格空间的数据源(Model)，控制器从该实例中获取需要显示的数据。

            private ViewController parentController;
            
            public CollectionSource(ViewController parentController)
            {
                this.parentController = parentController;
            }

            public override nint GetItemsCount(UICollectionView collectionView, nint section)
            {   //设置网格数
                return this.parentController.collectionData.Count;
            }

            public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
            {   //获取每个网格中的具体实例
                int rowIndex = indexPath.Row;
                ImageCell cell = collectionView.DequeueReusableCell(ImageCell.CELLID, indexPath) as ImageCell;
                cell.ImageView.Image = this.parentController.collectionData[rowIndex];

                return cell;
            }
        }
    }
}