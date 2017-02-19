using System;

using UIKit;

namespace Navigation
{
    public partial class MainViewController : UIViewController
    {
        public MainViewController() : base("MainViewController", null)
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
            button1.TouchUpInside += (sender, e) => {
                ViewController1 v1 = new ViewController1();
                v1.Title = "First View";
                this.NavigationController.PushViewController(v1, true);
            };

            button2.TouchUpInside += (sender, e) => {
                ViewController1 v2 = new ViewController1();
                v2.Title = "Second View";
                this.NavigationController.PushViewController(v2, true);
            };

            
        }
    }
}