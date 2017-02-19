using System;

using UIKit;

namespace ControllerLoadView
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
            label.Text = "Hello C#";
            // Perform any additional setup after loading the view, typically from a nib.
        }
    }
}