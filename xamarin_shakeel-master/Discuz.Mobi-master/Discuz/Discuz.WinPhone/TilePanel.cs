using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Discuz.WinPhone {
    public class TilePanel : Panel {
        #region DependencyProperty setup
        // Dependency properties for Tile Width, Height and Image
        public static readonly DependencyProperty TileWidthProperty =
            DependencyProperty.Register("TileWidth", typeof(double), typeof(TilePanel),
                  new PropertyMetadata(double.NaN, new PropertyChangedCallback(TileWidthPropertyChanged)));

        public static readonly DependencyProperty TileHeightProperty =
            DependencyProperty.Register("TileHeight", typeof(double), typeof(TilePanel),
                      new PropertyMetadata(double.NaN, new PropertyChangedCallback(TileHeightPropertyChanged)));

        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof(ImageBrush), typeof(TilePanel), new PropertyMetadata(null,
                                                new PropertyChangedCallback(ImagePropertyChanged)));

        public double TileWidth {
            get {
                return (double)GetValue(TileWidthProperty);
            }
            set {
                SetValue(TileWidthProperty, value);
            }
        }

        public double TileHeight {
            get {
                return (double)GetValue(TileHeightProperty);
            }
            set {
                SetValue(TileHeightProperty, value);
            }
        }

        public ImageBrush Image {
            get {
                return (ImageBrush)GetValue(ImageProperty);
            }
            set {
                SetValue(ImageProperty, value);
            }
        }

        #endregion

        private Size _lastFinalSize;
        private ImageBrush _imgBrush;
        private bool _needsUpdate = false;

        public TilePanel() {
            this.SizeChanged += new SizeChangedEventHandler(TilePanelSizeChanged);
        }

        /// <summary>
        /// Called when the size of the panel changes.
        /// </summary>
        /// <param name="sender">This panel</param>
        /// <param name="e">The new size information</param>
        protected virtual void TilePanelSizeChanged(object sender, SizeChangedEventArgs e) {
            if (e.NewSize != _lastFinalSize) {
                _lastFinalSize = e.NewSize;

                // adjust the clipping rectangle based on the new size
                RectangleGeometry rg = new RectangleGeometry();
                rg.Rect = new Rect(new Point(), _lastFinalSize);
                this.Clip = rg;

                TileAdjustmentNeededAsync();
            }
        }

        /// <summary>
        /// Signal that an adjustment to the tiles is needed,
        /// then makes the call asynchronously.
        /// By asynchronously making this call, it can help prevent
        /// unnecessary work by the panel.
        /// </summary>
        protected void TileAdjustmentNeededAsync() {
            // by doing this sync, we can queue up several request
            // but then really only handle the last one.
            _needsUpdate = true;
            // async call the adjust tiles
            this.Dispatcher.BeginInvoke(delegate {
                AdjustTiles();
            });
        }

        private static void TileHeightPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            TilePanel tilePanel = d as TilePanel;
            if (tilePanel != null) {
                Debug.WriteLine("TileHeight Changed: {0}", e.NewValue);
                double val = (double)e.NewValue;
                if (val == double.NaN || val <= 0.0) {
                    throw new ArgumentOutOfRangeException("TileHeight");
                }

                tilePanel.TileAdjustmentNeededAsync();
            }
        }

        private static void TileWidthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            TilePanel tilePanel = d as TilePanel;
            if (tilePanel != null) {
                Debug.WriteLine("TileWidth Changed: {0}", e.NewValue);
                double val = (double)e.NewValue;
                if (val == double.NaN || val <= 0.0) {
                    throw new ArgumentOutOfRangeException("TileWidth");
                }

                tilePanel.TileAdjustmentNeededAsync();
            }
        }

        private static void ImagePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            // put code here to handle the property changed for Image
            TilePanel tilePanel = d as TilePanel;
            if (tilePanel != null) {
                Debug.WriteLine("ImagePropertyChanged");
                tilePanel._imgBrush = e.NewValue as ImageBrush;
                // remove all of the existing tiles as they no longer are valid
                tilePanel.Children.Clear();
                tilePanel.TileAdjustmentNeededAsync();
            }
        }

        /// <summary>
        /// Perform the standard Arrange. Here though, all of the tiles
        /// are placed based on the tilesize specified.
        /// </summary>
        /// <param name="finalSize">the actual size that the panel has been given</param>
        /// <returns>The size taken (which is always what it was given).</returns>
        protected override Size ArrangeOverride(Size finalSize) {
            UIElementCollection children = Children;
            double tw = TileWidth;
            double th = TileHeight;
            double x = 0, y = 0;

            foreach (UIElement uie in Children) {
                if (x >= finalSize.Width) {
                    x = 0;
                    y += th;
                }

                uie.Arrange(new Rect(x, y, tw, th));
                x += tw;
            }

            return finalSize;
        }

        /// <summary>
        /// Adjusts the quantity of the tiles based on the 
        /// size of the panel and immediately calls
        /// UpdateLayout so the new tiles can be properly 
        /// layed out.
        /// </summary>
        protected virtual void AdjustTiles() {
            Debug.WriteLine("AdjustTiles - Update Needed: {0}", _needsUpdate);
            // seems that it was already done ...
            if (!_needsUpdate) {
                return;
            }
            _needsUpdate = false;

            // if no image brush is set ... all the children are cleared!
            if (_imgBrush == null) {
                this.Children.Clear();
                return;
            }

            double tw = TileWidth;
            double th = TileHeight;

            // determine how many we actually need
            int totalNeededX = (int)Math.Ceiling(this.ActualWidth / tw);
            int totalNeededY = (int)Math.Ceiling(this.ActualHeight / th);
            int totalNeeded = totalNeededX * totalNeededY;

            // there may be too many 
            if (totalNeeded < this.Children.Count) {
                while (this.Children.Count > totalNeeded) {
                    this.Children.RemoveAt(this.Children.Count - 1);
                }
            }
                // or too few ...
            else if (this.Children.Count < totalNeeded) {
                while (this.Children.Count < totalNeeded) {
                    Rectangle r = new Rectangle();
                    r.Width = tw;
                    r.Height = th;
                    r.Fill = _imgBrush;
                    this.Children.Add(r);
                }
            }

            this.UpdateLayout();
        }
    }
}
