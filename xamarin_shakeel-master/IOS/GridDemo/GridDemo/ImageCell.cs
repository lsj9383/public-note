using Foundation;
using System;
using UIKit;

namespace GridDemo
{
    public partial class ImageCell : UICollectionViewCell
    {
        public const string CELLID = "ImageCell";

        public ImageCell (IntPtr handle) : base (handle)
        {
            Initialize();
        }

        public UIImageView ImageView { get; private set; }

        private void Initialize()
        {
            ImageView = new UIImageView(this.ContentView.Bounds);
            this.ImageView.ContentMode = UIViewContentMode.ScaleAspectFit;
            this.ContentView.AddSubview(this.ImageView);        //一个cell里面，添加了一个ImageView
        }
    }
}