using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

using SimpleUI.UIComponent;

namespace SimpleUI
{
    public partial class FileTableViewController : UITableViewController
    {
        public FileTableViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            //
            //           SimpleAlert.Show("info", "first", "sure", (sender, e)=> { SimpleAlert.Show("info", "second", "ok"); });
            //nint index = SimpleAlert.Show("info", "first", 
            //    new List<string>() { "one", "tow", "three", "four", "five" }, (sender, e) => { Console.WriteLine("hi"); });
            //Console.WriteLine(index);
            nint index = SimpleAlert.BlockShow("info", "first",
                    new List<string>() { "one", "tow", "three", "four", "five" }, (sender, e) => { Console.WriteLine("hi"); });
            Console.WriteLine(index);
        }
    }
}