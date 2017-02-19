using System;

using UIKit;

namespace Camera
{
    public partial class ViewController : UIViewController
    {
        UIImagePickerController imagePicker;

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            imagePicker = new UIImagePickerController();
            btn.TouchUpInside += async delegate
            {
                if (UIImagePickerController.IsSourceTypeAvailable(UIImagePickerControllerSourceType.Camera))
                {

                    imagePicker.SourceType = UIImagePickerControllerSourceType.Camera;
                    await this.PresentViewControllerAsync(this.imagePicker, true);
                }
                else
                {
                    UIAlertView alertView = new UIAlertView();
                    alertView.Title = "对不起, 此设备没有相机功能";
                    alertView.AddButton("Cancel");
                    alertView.Show();
                }
            };
            
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}