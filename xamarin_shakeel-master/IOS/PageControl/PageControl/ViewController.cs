using System;
using System.Drawing;
using UIKit;

namespace PageControl
{
    public partial class ViewController : UIViewController
    {
        UIImageView page1;
        UIImageView page2;
        UIImageView page3;
        UIScrollView scrollView;
        UIPageControl pageControl;

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            

            scrollView = new UIScrollView();
            scrollView.Frame = new RectangleF(0, 0, 320, 495);
            scrollView.PagingEnabled = true;
            RectangleF pageFrame = (RectangleF)scrollView.Frame;
            scrollView.ContentSize = new SizeF(pageFrame.Width*3, pageFrame.Height);
            scrollView.Scrolled += delegate { Console.WriteLine("Scrolled!"); }; //滚动...            
            //            scrollView.DecelerationEnded += 
            pageControl = new UIPageControl();
            pageControl.Frame = new RectangleF(0, 540, 320, 37);
            pageControl.Pages = 3;          //设置页面控件的页数
            //            pageControl.ValueChanged += this      //发生页数变化时调用

            page1 = new UIImageView(pageFrame);
            page1.ContentMode = UIViewContentMode.ScaleAspectFit;
            page1.Image = UIImage.FromFile("1.jpg");
            pageFrame.X += (float)this.scrollView.Frame.Width;

            //
            page2 = new UIImageView(pageFrame);
            page2.ContentMode = UIViewContentMode.ScaleAspectFit;
            page2.Image = UIImage.FromFile("2.jpg");
            pageFrame.X += (float)this.scrollView.Frame.Width;

            page3 = new UIImageView(pageFrame);
            page3.ContentMode = UIViewContentMode.ScaleAspectFit;
            page3.Image = UIImage.FromFile("3.jpg");
            pageFrame.X += (float)this.scrollView.Frame.Width;

            scrollView.AddSubview(page1);
            scrollView.AddSubview(page2);
            scrollView.AddSubview(page3);
            this.View.AddSubview(scrollView);
            this.View.AddSubview(pageControl);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
            
        }
    }
}