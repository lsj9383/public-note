using System;
using System.Drawing;
using UIKit;

namespace TabBarController
{
    public partial class ViewController2 : UIViewController
    {
        public ViewController2() : base("ViewController2", null)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.
            UILabel label = new UILabel();
            label.Frame = new RectangleF(10, 10, 100, 100);
            label.BackgroundColor = UIColor.Orange;
            label.Text = "Second";
            label.TextAlignment = UITextAlignment.Center;
            this.View.AddSubview(label);
        }
    }
}