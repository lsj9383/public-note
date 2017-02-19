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

namespace AutoTextView
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnAddText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView TextViewMessage { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnAddText != null) {
                btnAddText.Dispose ();
                btnAddText = null;
            }

            if (TextViewMessage != null) {
                TextViewMessage.Dispose ();
                TextViewMessage = null;
            }
        }
    }
}