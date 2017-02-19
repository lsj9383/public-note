using System;
using System.Drawing;
using UIKit;

namespace TabBarController
{
    public partial class ViewController1 : UIViewController
    {
        public ViewController1() : base("ViewController1", null)
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
            string message="";
            UILabel label = new UILabel();
            label.Frame = new RectangleF(10, 10, 100, 100);
            label.BackgroundColor = UIColor.Orange;
            label.Text = "First";
            label.TextAlignment = UITextAlignment.Center;

            UIButton button = new UIButton();
            button.Frame = new RectangleF(10, 170, 100, 30);
            button.BackgroundColor = UIColor.LightGray;
            button.TouchUpInside += (sender, e) => {
                if (this.NavigationController == null)
                {
                    message = "no nav";
                }
                else
                {
                    message = "have nav";
                }
                Console.WriteLine(this.ParentViewController.ToString());
                UIAlertView alert = new UIAlertView();
                alert.Title = "½á¹û";
                alert.Message = message;
                alert.AddButton("cancel");
                alert.Show();
            };


            this.View.AddSubview(label);
            this.View.AddSubview(button);
        }
    }
}