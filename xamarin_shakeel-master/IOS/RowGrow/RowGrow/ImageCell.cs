using CoreGraphics;
using Foundation;
using System;
using UIKit;

namespace RowGrow
{
    public partial class ImageCell : UICollectionViewCell
    {
        public ImageCell (IntPtr handle) : base (handle)
        {
        }

        public UIImageView ImageView
        {
            get;
            private set;
        }

        public void CellInit()
        {
            this.ImageView = new UIImageView(this.ContentView.Bounds);
            this.ImageView.Frame = new CGRect(0, 0, this.Frame.Width, this.Frame.Height);
            this.ImageView.ContentMode = UIViewContentMode.ScaleAspectFit;
            this.ContentView.AddSubview(this.ImageView);
        }
    }
}