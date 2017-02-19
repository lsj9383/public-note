using CoreGraphics;
using Foundation;
using System;
using UIKit;

namespace AutoContentByTableViewCell
{
    public partial class MessageCell : UITableViewCell
    {
        public MessageCell (IntPtr handle) : base (handle)
        {
        }

        public void Initial()
        {
            lblMessage.LineBreakMode = UILineBreakMode.WordWrap;
            lblMessage.Lines = 0;
        }

        public UILabel LblMessage
        {
            get { return lblMessage; }
        }
    }
}