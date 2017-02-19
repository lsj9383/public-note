using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace SimpleView
{
    public class App : Application
    {
        public App()
        {
            MainPage = new TabbedPage
            {
                Children =
                {
                    new NavigationPage(new PageHome()) { Title="主页", Icon = new FileImageSource() { File="drawable/red_home.png" } },
                    new NavigationPage(new PageCommunity()) { Title="社区", Icon = new FileImageSource() { File="drawable/red_star.png" } },
                    new NavigationPage(new PageMe()) { Title="我的", Icon = new FileImageSource() { File="drawable/red_user.png" } }
                }
            };
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
