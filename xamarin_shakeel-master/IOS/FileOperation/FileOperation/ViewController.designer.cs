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

namespace FileOperation
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField content { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton createButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton createDirButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton deleteButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton dirButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField fileName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton readButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton writeButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (content != null) {
                content.Dispose ();
                content = null;
            }

            if (createButton != null) {
                createButton.Dispose ();
                createButton = null;
            }

            if (createDirButton != null) {
                createDirButton.Dispose ();
                createDirButton = null;
            }

            if (deleteButton != null) {
                deleteButton.Dispose ();
                deleteButton = null;
            }

            if (dirButton != null) {
                dirButton.Dispose ();
                dirButton = null;
            }

            if (fileName != null) {
                fileName.Dispose ();
                fileName = null;
            }

            if (readButton != null) {
                readButton.Dispose ();
                readButton = null;
            }

            if (writeButton != null) {
                writeButton.Dispose ();
                writeButton = null;
            }
        }
    }
}