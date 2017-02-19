using System;
using Foundation;
using UIKit;
using System.Collections.Generic;

namespace CollectionView
{
    public partial class ViewController : UIViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            CollectionView.Source = new CollectionSource(new List<string>() { "user1.jpg"});
        }
    }

    public class CollectionSource : UICollectionViewSource
    {
        List<string> photos;

        public CollectionSource(List<string> photos) { this.photos = photos; }

        public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = collectionView.DequeueReusableCell("ImageCell", indexPath) as ImageCell;
            cell.Initialize();
            cell.imageView.Image = UIImage.FromFile(photos[indexPath.Row]);
            return cell;
        }

        public override nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            if (photos == null)
            {
                return 0;
            }
            else
            {
                return photos.Count;
            }
        }
    }
}