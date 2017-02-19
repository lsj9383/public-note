using CoreGraphics;
using Foundation;
using System;
using System.Drawing;
using UIKit;

namespace RowGrow
{
    public partial class GrowCell : UITableViewCell
    {
        public GrowCell (IntPtr handle) : base (handle)
        {
            
        }

        public UILabel LblTitle
        {
            get { return lblTitle; }
        }

        public UILabel LblMessage
        {
            get { return lblMessage; }
        }

        public UICollectionView ViewGrid
        {
            get { return viewGrid; }
        }

        public void FitCollectionSize()
        {
            UICollectionViewFlowLayout flowLayout = new UICollectionViewFlowLayout();
            flowLayout.MinimumLineSpacing = 10;
            flowLayout.ItemSize = new SizeF(90, 90);
            viewGrid.CollectionViewLayout = flowLayout;

            this.LayoutIfNeeded();
        }
    }
}