using Foundation;
using System;
using UIKit;

namespace CollectionView
{
    public partial class ImageCell : UICollectionViewCell
    {
        public ImageCell (IntPtr handle) : base (handle)
        {
        }

        public UIImageView imageView{ get; private set; }

        public void Initialize()
        {
            this.imageView = new UIImageView(this.ContentView.Bounds);
            this.imageView.ContentMode = UIViewContentMode.ScaleAspectFit;
            this.ContentView.AddSubview(this.imageView);
        }
    }
}