using System;
using System.Drawing;
using UIKit;

namespace Scroll
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
            UIScrollView scrollView = new UIScrollView();
            scrollView.Frame = new RectangleF(0, 0, 320, 568);
            scrollView.ContentSize = new SizeF(320, 2000);              //滚动范围，一直拉到了2000高度
            scrollView.Scrolled += delegate { Console.WriteLine("开始滚动"); };
            scrollView.DecelerationEnded += delegate { Console.WriteLine("滚动结束"); };
            
            float y = 10;
            for (float i = 1; i < 21; i++)
            {
                UILabel label = new UILabel();
                label.Frame = new RectangleF(0, y, 320, 50);
                label.BackgroundColor = UIColor.Cyan;
                label.Text = String.Format("{0}", i);
                scrollView.AddSubview(label);
                y += 100;
            }
            
            scrollView.BackgroundColor = UIColor.Orange;
            this.View.AddSubview(scrollView);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}