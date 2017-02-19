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

namespace Discuz.Droid {
    public class MyUncaughtExceptionHandler : Java.Lang.Thread.IUncaughtExceptionHandler {
        public void UncaughtException(Java.Lang.Thread thread, Java.Lang.Throwable ex) {

        }

        public IntPtr Handle {
            get {
                return IntPtr.Zero;
            }
        }

        public void Dispose() {
            //throw new NotImplementedException();
        }
    }
}