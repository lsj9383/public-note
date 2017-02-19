using System;
using System.Drawing;
using UIKit;

namespace Text
{
    public partial class ViewController : UIViewController
    {
        //控制器中直接添加两个成员，这样方便在控制器的事件处理中引用该成员。
        UITextField tf1;
        UITextField tf2;

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            tf1 = new UITextField();
            tf1.BorderStyle = UITextBorderStyle.RoundedRect;    //设置文本框边框
            tf1.Frame = new RectangleF(54, 150, 211, 30);
            tf1.Placeholder = "账号";                             //设置占位符

            tf2 = new UITextField();
            tf2.BorderStyle = UITextBorderStyle.RoundedRect;
            tf2.Frame = new RectangleF(54, 220, 211, 30);
            tf2.SecureTextEntry = true;

            this.View.AddSubview(tf1);
            this.View.AddSubview(tf2);

            button.TouchUpInside += this.ButtonChange_TouchUpInside;

        }

        private void ButtonChange_TouchUpInside(object sender, EventArgs e)
        {
            if (tf1.Text.Length != 0 && tf2.Text.Length != 0)
            {
                this.View.BackgroundColor = UIColor.Orange;
            }
            else
            {
                this.View.BackgroundColor = UIColor.Red;
            }
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}