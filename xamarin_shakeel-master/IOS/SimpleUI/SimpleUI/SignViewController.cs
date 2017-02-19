using Foundation;
using System;
using System.Collections.Generic;
using System.Collections;
using UIKit;

using SimpleUI.UIComponent;

namespace SimpleUI
{
    public partial class SignViewController : UIViewController
    {
        private Dictionary<string, string> UserKeys = new Dictionary<string, string>();

        public SignViewController (IntPtr handle) : base (handle)
        {
            //initial
            InitSystem();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            SignImage.Image = UIImage.FromFile("2.jpg");
            SignInButton.TouchUpInside += SignInEvent;
            SignUpButton.TouchUpInside += SignUpEvent;
        }

        private void InitSystem()
        {
            UserKeys.Add("root", "root");
            SimpleSqlite sqlite = new SimpleSqlite("personal", "Database");
            sqlite.QueryColumnsNumber("SimpleUI.db3", "UPS");

            if (sqlite.CreateDatabase("simpleUI.db3"))
            {
                sqlite.CreateTable("simpleUI.db3", "UPS",
                    new Dictionary<string, string>()
                    {   { "USER","VARCHAR(20)" },
                        { "PASSWORD", "VARCHAR(20)"} });

                sqlite.InsertInto("SimpleUI.db3", "UPS", new List<string>() { "root", "root"});
                sqlite.InsertInto("SimpleUI.db3", "UPS", new List<string>() { "root2", "root2" });
            }
        }

        public void AddUser(string user, string password)
        {
            UserKeys.Add(user, password);
        }

        private void SignInEvent(object sender, EventArgs e)
        {   //按下登录触发的事件
            string input = UserName.Text;

            //check input
            if (input.Equals(""))
            {
                SimpleAlert.Show("Error", "请输入用户名", "确定");
                return;
            }

            //check result
            if (Access(input, Password.Text))
            {
                SimpleAlert.Show("提示", "登录成功", "确定");
                UIStoryboard storyboard = UIStoryboard.FromName("MainBody", NSBundle.MainBundle);
                UIViewController MainBodyController = storyboard.InstantiateInitialViewController() as UIViewController;
                this.NavigationController.PushViewController(MainBodyController, true);
            }
            else
            {
                SimpleAlert.Show("Error", "用户名或密码错误", "确定");
            }
        }

        private void SignUpEvent(object sender, EventArgs e)
        {   //按下注册触发的事件

            UIStoryboard storyboard = UIStoryboard.FromName("Sign", NSBundle.MainBundle);
            SignUpViewController viewController = storyboard.InstantiateViewController("SignUpViewController") as SignUpViewController;
            viewController.UserKeys = UserKeys;     //传入参数
            this.NavigationController.PushViewController(viewController, true);
        }

        private bool Access(string User, string Password)
        {
            SimpleSqlite sqlite = new SimpleSqlite("personal", "Database");
            List<Dictionary<string, string>> result 
                = sqlite.QueryFrom("simpleUI.db3", "UPS", new List<string>() { "*" }, String.Format("USER='{0}'", User));
            if (result == null)
            {   //数据库出错
                return false;
            }

            if (result.Count == 0)
            {   //不存在该用户
                return false;
            }

            string correctPassword = null;
            result[0].TryGetValue("PASSWORD", out correctPassword);
            if (correctPassword == null)
            {   //数据库出错
                return false;
            }

            if (correctPassword != Password)
            {   //密码错误
                return false;
            }

            return true;
        }
    }
}