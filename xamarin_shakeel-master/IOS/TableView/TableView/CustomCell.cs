using Foundation;
using System;
using UIKit;

namespace TableView
{
    public partial class CustomCell : UITableViewCell
    {
        public const string CELLID = "CustomCell";
        public CustomCell(IntPtr handle) : base (handle)
        {
        }
        
        public UILabel labelTitle
        {
            get
            {
                return LabelTitle;
            }
        }

        public UIImageView imgView
        {
            get
            {
                return ImageView;
            }
        }
    }
}