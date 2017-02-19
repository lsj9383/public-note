using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Reflection;

namespace Discuz {
    public class ViewLocatorPage : ContentPage {

        public static readonly BindableProperty VMProperty = BindableProperty.Create<ViewLocatorPage, Screen>(p => p.VM, null, propertyChanged: VMChanged);

        public Screen VM {
            get {
                return (Screen)this.GetValue(VMProperty);
            }
            set {
                this.SetValue(VMProperty, value);
            }
        }


        private static void VMChanged(BindableObject bindable, object oldValue, object newValue) {
            if (newValue == null)
                return;

            var vm = (Screen)newValue;
            //var view = vm.GetView();
            var vmView = ViewLocator.LocateForModel(vm, null, null);
            if (vmView == null)
                throw new Exception("没有找到视图");
            ViewModelBinder.Bind(vm, vmView, null);

            var activator = vm as IActivate;
            if (activator != null)
                activator.Activate();

            var page = (ViewLocatorPage)bindable;
            if (null != (ContentPage)vmView) {
                var vp = (ContentPage)vmView;
                page.Content = vp.Content;
                if (vp.ToolbarItems != null)
                    foreach (var t in vp.ToolbarItems)
                        page.ToolbarItems.Add(t);
            } else if (null != (Xamarin.Forms.View)vmView) {
                page.Content = (Xamarin.Forms.View)vmView;
            }
        }

    }
}
