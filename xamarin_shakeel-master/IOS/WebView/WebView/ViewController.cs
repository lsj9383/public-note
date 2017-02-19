using Foundation;
using System;

using UIKit;

namespace WebView
{
    public partial class ViewController : UIViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            NSUrl url = new NSUrl("http://www.baidu.com");
            NSUrlRequest urlRequest = new NSUrlRequest(url);
            this.WebView.LoadRequest(urlRequest);
        }
    }
}