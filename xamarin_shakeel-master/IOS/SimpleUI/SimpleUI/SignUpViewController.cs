using Foundation;
using System;
using UIKit;
using System.Collections.Generic;
using SimpleUI.UIComponent;

namespace SimpleUI
{
    public partial class SignUpViewController : UIViewController
    {
        public Dictionary<string, string> UserKeys;

        public SignUpViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.Title = "注册";
            SureButton.TouchUpInside += SureEvent;
        }

        private void SureEvent(object sender, EventArgs e)
        {
            string user = UserName.Text;
            string password = Password.Text;
            string twice = TwicePassword.Text;

            //check input
            if (user.Equals("") || password.Equals("") || twice.Equals(""))
            {
                SimpleAlert.Show("错误", "请输入完整", "确定");
                return ;
            }

            if (!password.Equals(twice))
            {
                SimpleAlert.Show("错误", "两次密码应该一样", "确定");
                return;
            }

            //finish
            SimpleSqlite.InsertInto("personal", "Database", "SimpleUI.db3", "UPS", new List<string>() { user, password });
            this.NavigationController.PopViewController(true);
        }
    }
}