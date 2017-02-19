using System;
using System.Drawing;
using UIKit;

namespace MulText
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
            
            UITextView myTextView = new UITextView();
            myTextView.Frame = new RectangleF(9, 90, 302, 180);
            myTextView.BackgroundColor = UIColor.Orange;
            this.View.AddSubview(myTextView);

            UITextView myText = new UITextView();
            myText.Frame = new RectangleF(9, 330, 302, 180);
            myText.BackgroundColor = UIColor.LightGray;
            myText.Editable = false;
            this.View.AddSubview(myText);
            myText.Hidden = true;
            

            UIButton button = new UIButton();
            button.Frame = new RectangleF(137, 56, 46, 30);
            button.BackgroundColor = UIColor.LightGray;
            button.SetTitle("完成", UIControlState.Normal);
            this.View.AddSubview(button);
            button.TouchUpInside += (sender, e) => {
     //           myTextView.ResignFirstResponder();      //关闭键盘
                myText.Hidden = false;
                myText.Text = myTextView.Text;
            };
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}