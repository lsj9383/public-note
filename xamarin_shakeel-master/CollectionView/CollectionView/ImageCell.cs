using Foundation;
using System;
using UIKit;

namespace CollectionView
{
    public partial class ImageCell : UICollectionViewCell
    {
        public const string CELLID = "ImageCell";
        public ImageCell(IntPtr handle) : base(handle)
        {
            this.Initialize();  //初始化，主要是针对cell中的部件进行的
        }

        public UIImageView ImageView { get; private set; }

        public void Initialize()
        {
            this.ImageView = new UIImageView(this.ContentView.Bounds);              //初始化一个UIImageView，也可以通过拖拉实现添加UIImageView
            this.ImageView.ContentMode = UIViewContentMode.ScaleAspectFit;
            this.ContentView.AddSubview(this.ImageView);
        }
    }
}