using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using Discuz.Controls;
using Xamarin.Forms;
using Android.Graphics.Drawables.Shapes;
using Android.Graphics.Drawables;
using Discuz.Droid.Renders;
using Android.Graphics;

[assembly: ExportRenderer(typeof(CycleBox), typeof(CycleBoxRender))]
namespace Discuz.Droid.Renders {
    public class CycleBoxRender : VisualElementRenderer<CycleBox> {

        protected override void OnElementChanged(ElementChangedEventArgs<CycleBox> e) {
            base.OnElementChanged(e);
            this.Element.HorizontalOptions = LayoutOptions.Center;
            this.Element.VerticalOptions = LayoutOptions.Center;
        }


        public override void Draw(Canvas canvas) {
            var density = this.Context.Resources.DisplayMetrics.Density;
            var path = new Path();
            path.AddCircle(canvas.Width / 2, canvas.Height / 2, (float)this.Element.Radius * density, Path.Direction.Ccw);
            canvas.ClipPath(path, Region.Op.Intersect);
            canvas.DrawColor(this.Element.BackgroundColor.ToAndroid());

            base.Draw(canvas);
        }
    }
}