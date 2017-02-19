using System;
using System.Drawing;
using UIKit;

namespace Image
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
            UIImageView ImageDis = new UIImageView();
            ImageDis.Frame = new RectangleF(0, 0, 320, 580);
            ImageDis.ContentMode = UIViewContentMode.ScaleAspectFit;        //设置图像视图的模式
            ImageDis.Image = UIImage.FromFile("2.jpg");
            this.View.AddSubview(ImageDis);

        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}