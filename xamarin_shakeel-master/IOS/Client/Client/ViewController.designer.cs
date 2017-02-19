// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Client
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblStatus { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton open { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField website { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (lblStatus != null) {
                lblStatus.Dispose ();
                lblStatus = null;
            }

            if (open != null) {
                open.Dispose ();
                open = null;
            }

            if (website != null) {
                website.Dispose ();
                website = null;
            }
        }
    }
}