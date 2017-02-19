using System;
using System.Drawing;
using UIKit;

namespace Code_View
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
            UIView vv1 = new UIView();
            vv1.Frame = new RectangleF(0, 20, 320, 250);
            vv1.BackgroundColor = UIColor.Cyan;
            this.View.AddSubview(vv1);

            UIView vv2 = new UIView();
            vv2.Frame = new RectangleF(0, 300, 320, 250);
            vv2.BackgroundColor = UIColor.Orange;
            this.View.AddSubview(vv2);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}