using System;
using System.Drawing;
using UIKit;

namespace Alert
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
            UIButton button1 = new UIButton();
            button1.Frame = new RectangleF(20, 20, 100, 30);
            button1.BackgroundColor = UIColor.LightGray;
            button1.TouchUpInside += (sender, e) => {
                UIAlertView alertView = new UIAlertView();
                alertView.Title = "提示";
                alertView.Message = "内存不足";
                alertView.AddButton("前去清理");
                alertView.Show();
            };

            UIButton button2 = new UIButton();
            button2.Frame = new RectangleF(20, 70, 100, 30);
            button2.BackgroundColor = UIColor.Yellow;
            button2.TouchUpInside += (sender, e) => {
                UIAlertView alertView = new UIAlertView();
                alertView.Title = "谢谢";
                alertView.Message = "亲如果你对我们的上平满意，请点亮五颗星!";
                alertView.AddButton("前往评论");
                alertView.AddButton("暂不评论");
                alertView.AddButton("残忍拒绝");
                alertView.Show();
            };

            UIButton button3 = new UIButton();
            button3.Frame = new RectangleF(20, 120, 100, 30);
            button3.BackgroundColor = UIColor.Orange;
            button3.TouchUpInside += (sender, e) => {
                UIAlertView alertView = new UIAlertView();
                alertView.Title = "登录";
                alertView.AlertViewStyle = UIAlertViewStyle.LoginAndPasswordInput;
                alertView.AddButton("前往");
                alertView.Show();
            };

            UIButton button4 = new UIButton();
            button4.Frame = new RectangleF(20, 170, 100, 30);
            button4.BackgroundColor = UIColor.Green;
            button4.TouchUpInside += (sender, e) => {
                UIAlertView alertView = new UIAlertView();
                alertView.Title = "请按下按钮";
                alertView.AddButton("Hello");
                alertView.AddButton("World");
                alertView.Show();
                alertView.Dismissed += (alert_sender, alert_e) => {
                    if (alert_e.ButtonIndex == 0)
                    {
                        button4.SetTitle("hello", UIControlState.Normal);
                    }
                    else
                    {
                        button4.SetTitle("world", UIControlState.Normal);
                    }
                };
            };

            this.View.AddSubview(button1);
            this.View.AddSubview(button2);
            this.View.AddSubview(button3);
            this.View.AddSubview(button4);

        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        
    }
}