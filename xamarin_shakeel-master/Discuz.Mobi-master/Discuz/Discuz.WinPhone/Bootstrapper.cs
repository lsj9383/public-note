using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Discuz.WinPhone {
    public class Bootstrapper : PhoneBootstrapperBase {

        private PhoneContainer container = null;

        public Bootstrapper() {
            this.Initialize();
        }

        protected override void OnUnhandledException(object sender, System.Windows.ApplicationUnhandledExceptionEventArgs e) {
            //base.OnUnhandledException(sender, e);
            e.Handled = true;
        }

        protected override void Configure() {
            base.Configure();

            this.container = new PhoneContainer();
            this.container.RegisterPhoneServices(this.RootFrame);
        }

        protected override IEnumerable<Assembly> SelectAssemblies() {
            return new[]
            {
                GetType().Assembly,
                typeof (Discuz.App).Assembly
            };
        }

        protected override void BuildUp(object instance) {
            container.BuildUp(instance);
        }

        protected override IEnumerable<object> GetAllInstances(Type service) {
            return container.GetAllInstances(service);
        }

        protected override object GetInstance(Type service, string key) {
            return container.GetInstance(service, key);
        }
    }
}
