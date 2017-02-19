using System;
using System.Drawing;
using UIKit;

namespace Button
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
            UIButton button = new UIButton();
            Boolean isYellow = false;
            button.Frame = new RectangleF(120, 261, 80, 30);
            button.BackgroundColor = UIColor.Cyan;
            button.TouchUpInside += (sender, e) => {
                if (isYellow)
                {
                    isYellow = !isYellow;
                    button.BackgroundColor = UIColor.LightGray;
                }
                else
                {
                    isYellow = !isYellow;
                    button.BackgroundColor = UIColor.Yellow;
                }
            };
            this.View.AddSubview(button);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}