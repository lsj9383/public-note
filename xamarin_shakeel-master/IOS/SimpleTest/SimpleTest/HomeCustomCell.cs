using Foundation;
using System;
using UIKit;

namespace SimpleTest
{
    public partial class HomeCustomCell : UITableViewCell
    {
        static public string Identifier = "Id_HomeCustomCell";
        public HomeCustomCell (IntPtr handle) : base (handle)
        {
        }

       public UILabel LabelTitle
        {
            get
            {
                return labelTitle;
            }
        }

        public UILabel LabelDescp
        {
            get
            {
                return labelDescp;
            }
        }

        public UIImageView ImageView
        {
            get
            {
                return imgView;
            }
        }

        public UIImageView ImageStatue
        {
            get
            {
                return imageStatue;
            }
        }
    }
}