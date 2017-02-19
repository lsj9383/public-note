using CoreGraphics;
using System;

using UIKit;

namespace AutoTextView
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

            btnAddText.TouchUpInside += (sender, e) =>
            {

                TextViewMessage.Text += "hello";

                AutoFitTextContent(TextViewMessage);
            };

            TextViewMessage.Changed += (sender, e) =>
            {
                AutoFitTextContent(sender as UITextView);
            };
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        private void AutoFitTextContent(UITextView TextView)
        {
            CGSize constraintSize = new CGSize(TextView.Frame.Size.Width, float.MaxValue);
            CGSize size = TextView.SizeThatFits(constraintSize);
            TextView.Frame = new CGRect(TextView.Frame.X, TextView.Frame.Y, size.Width, size.Height);
        }
    }
}