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

namespace Alert
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton button1 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton button2 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton button3 { get; set; }

        [Action ("Button1_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void Button1_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (button1 != null) {
                button1.Dispose ();
                button1 = null;
            }

            if (button2 != null) {
                button2.Dispose ();
                button2 = null;
            }

            if (button3 != null) {
                button3.Dispose ();
                button3 = null;
            }
        }
    }
}