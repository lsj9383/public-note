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
using Discuz.Services;
using Xamarin.Forms;
using Discuz.Droid.Services;

[assembly: Dependency(typeof(Hud))]
namespace Discuz.Droid.Services {
    public class Hud : IToast {

        private Toast toast = null;

        public void ShowToast(string msg, int delay = 1000) {
            var vibrator = (Vibrator)Xamarin.Forms.Forms.Context.GetSystemService(Context.VibratorService);
            vibrator.Vibrate(100);

            //java.lang.NullPointerException: Attempt to invoke virtual method 'android.content.res.Resources$Theme android.content.Context.getTheme()' on a null object reference
            //XHUD.HUD.ShowToast(msg, timeoutMs: delay);
            this.toast = Toast.MakeText(Forms.Context, msg, ToastLength.Short);
            this.toast.Show();
        }


        public void Show(string msg) {
            var vibrator = (Vibrator)Xamarin.Forms.Forms.Context.GetSystemService(Context.VibratorService);
            vibrator.Vibrate(100);
            //XHUD.HUD.ShowToast(msg, true, 60000);
            this.toast = Toast.MakeText(Forms.Context, msg, ToastLength.Long);
            this.toast.Show();
        }

        public void Close() {
            //XHUD.HUD.Dismiss();
            this.toast.Cancel();
        }
    }
}