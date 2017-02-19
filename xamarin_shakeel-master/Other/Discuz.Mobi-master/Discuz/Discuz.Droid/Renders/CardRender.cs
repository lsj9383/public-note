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
using Discuz.Droid.Renders;
using Android.Support.V7.Widget;


//[assembly: ExportRenderer(typeof(Card), typeof(CardRender))]
namespace Discuz.Droid.Renders {
    public class CardRender : VisualElementRenderer<Card> {

        protected override void OnElementChanged(ElementChangedEventArgs<Card> e) {
            base.OnElementChanged(e);

            var cv = new CardView(this.Context);
            
        }

    }
}