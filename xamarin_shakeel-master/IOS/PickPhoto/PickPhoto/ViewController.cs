using System;

using UIKit;

namespace PickPhoto
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
//            imagePicker.FinishedPickingMedia += ImagePicker_FinishedPickingMedia;
//            imagePicker.Canceled += ImagePicker_Cancelled;

            // 设置图像来源
            imagePicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
            // 触摸按钮
            btnSelect.TouchUpInside += async delegate {
                await this.PresentViewControllerAsync(this.imagePicker, true);
            };
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        private async void ImagePicker_FinishedPickingMedia(object sender, UIImagePickerMediaPickedEventArgs e)
        {   //图像选择后调用的方法
            UIImage pickedImage = e.Info[UIImagePickerController.OriginalImage] as UIImage;
            imgView.Image = pickedImage;       //将选择的图像显示在视图中
            await imagePicker.DismissViewControllerAsync(true);
        }

        private async void ImagePicker_Cancelled(object sender, EventArgs e)
        {
            await this.imagePicker.DismissViewControllerAsync(true);
        }
    }
}