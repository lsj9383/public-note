using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Caliburn.Micro;
using Xamarin.Forms.Platform.WinPhone;
using System.Reflection;
using System.ComponentModel;
using System.Windows.Media;

namespace Discuz.WinPhone {
    public partial class MainPage : FormsApplicationPage, INotifyPropertyChanged {

        public event PropertyChangedEventHandler PropertyChanged;
        private Platform Platform;

        public MainPage() {
            InitializeComponent();

            SupportedOrientations = SupportedPageOrientation.PortraitOrLandscape;

            global::Xamarin.Forms.Forms.Init();
            var app = new Discuz.App(IoC.Get<PhoneContainer>());
            this.LoadApplication(app);

            //菜单栏最小化
            this.ApplicationBar.Opacity = 0.75;
            this.ApplicationBar.Mode = ApplicationBarMode.Minimized;
            //this.ApplicationBar.BackgroundColor = Color.FromArgb(255, 27, 117, 146);//FF1B7592
            this.DataContext = this;
        }

        new protected void LoadApplication(Xamarin.Forms.Application application) {
            //Xamarin.Forms.Application.Current = application;
            typeof(Xamarin.Forms.Application).GetProperty("Current", BindingFlags.Static | BindingFlags.Public)
                .SetValue(Xamarin.Forms.Application.Current, application);


            application.PropertyChanged += new PropertyChangedEventHandler(this.ApplicationOnPropertyChanged);
            typeof(FormsApplicationPage).GetField("application", BindingFlags.NonPublic | BindingFlags.Instance)
                .SetValue(this, application);

            //application.SendStart();
            application.GetType().GetMethod("SendStart", BindingFlags.NonPublic | BindingFlags.Instance)
                .Invoke(application, null);

            this.SetMainPage();

            var mp = (Xamarin.Forms.NavigationPage)application.MainPage;
            mp.BackgroundColor = Xamarin.Forms.Color.FromHex("EFEFEF");
            mp.PropertyChanged += MainPage_PropertyChanged;
            this.Title = mp.Title;
        }

        void MainPage_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            if (e.PropertyName.Equals("CurrentPage")) {
                this.Title = ((Xamarin.Forms.NavigationPage)sender).CurrentPage.Title;
                this.PropertyChanged(this, new PropertyChangedEventArgs("Title"));
            }
        }


        //不会执行
        private void ApplicationOnPropertyChanged(object sender, PropertyChangedEventArgs args) {
            if (!(args.PropertyName == "MainPage"))
                return;
            this.SetMainPage();
        }

        private void SetMainPage() {
            if (this.Platform == null) {
                this.Platform = new Platform((PhoneApplicationPage)this);
            }

            this.Platform.SetPage(Xamarin.Forms.Application.Current.MainPage);
            if (this.mainBody.Content != null && this.mainBody.Content.Equals(this.Platform))
                return;
            this.mainBody.Content = (UIElement)this.Platform;
        }
    }
}
