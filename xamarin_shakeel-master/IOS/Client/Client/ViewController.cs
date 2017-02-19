using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using UIKit;
using System.Threading.Tasks;

namespace Client
{
    public partial class ViewController : UIViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            open.TouchUpInside += async (sender, e) =>
            {
                HttpClient httpclient = new HttpClient();
                //HttpResponseMessage response = await httpclient.GetStringAsync(website.Text).ContinueWith(task =>
                //{
                //    return task.Result;
                //});

                string strURL = website.Text;

                HttpResponseMessage response = null;
                //await Task.Run(delegate()
                //{
                //    httpclient.GetAsync(strURL).ContinueWith(task =>
                //    {
                //        if (task.IsCompleted)
                //        {
                //            response = task.Result;
                //        }
                //        else
                //        {
                //            response = null;
                //        }
                //    });
                //});

                await httpclient.GetAsync(strURL).ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                        response = null;
                    }
                    else
                    {
                        response = task.Result;
                    }
                });

                if (response != null)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Linked");
                        new Alert("消息", "连接成功", "确定");
                        Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                    }
                    else
                    {
                        Console.WriteLine("not Link");
                        new Alert("消息", "连接失败", "确定");
                    }

                    EnumerateHeader(response.Headers);
                }
            };
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        private void EnumerateHeader(HttpHeaders headers)
        {
            foreach (var header in headers)
            {
                var s = "";
                foreach (var item in header.Value)
                {
                    s += item + "/";
                }
                Console.WriteLine("Key : "+header.Key+", Value : "+s);
            }
        }

        class Alert
        {
            public Alert(string Title, string Message, string ButtonMessage)
            {
                UIAlertView alert = new UIAlertView();
                alert.Title = Title;
                alert.Message = Message;
                alert.AddButton(ButtonMessage);
                alert.Show();
            }
        }
    }
}