using System;
using System.IO;
using UIKit;

namespace FileOperation
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
            createButton.TouchUpInside += (sender, e) =>{
                string homePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                string filePath = Path.Combine(homePath, fileName.Text);
                Console.WriteLine("home : " + homePath);
                File.Create(filePath);

                UIAlertView alert = new UIAlertView();
                alert.Title = "information";
                alert.Message = "create success.";
                alert.AddButton("确认");
                alert.Show();
            };

            writeButton.TouchUpInside += (sender, e) =>
            {
                string homePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                string filePath = Path.Combine(homePath, fileName.Text);
                StreamWriter writer = null;
                writer = new StreamWriter(filePath);
                writer.Write(content.Text);
                writer.Close();                         //close 才能正确写入, 就算没有该文件也能进行创建

                UIAlertView alert = new UIAlertView();
                alert.Title = "Information";
                alert.Message = "write success";
                alert.AddButton("确认");
                alert.Show();
            };

            readButton.TouchUpInside += (sender, e) =>
            {
                string homePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                string filePath = Path.Combine(homePath, fileName.Text);
                
                if (File.Exists(filePath) == false)
                {
                    UIAlertView alert = new UIAlertView();
                    alert.Title = "Information";
                    alert.Message = "Error File";
                    alert.AddButton("确认");
                    alert.Show();
                    return;
                }
                StreamReader reader = new StreamReader(filePath);
                string s = reader.ReadToEnd();
                reader.Close();
                Console.WriteLine(s);
            };

            deleteButton.TouchUpInside += (sender, e) =>
            {
                string homePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                string filePath = Path.Combine(homePath, fileName.Text);

                if (File.Exists(filePath) == false)
                {
                    UIAlertView alort = new UIAlertView();
                    alort.Title = "Information";
                    alort.Message = "Error File";
                    alort.AddButton("确认");
                    alort.Show();
                    return;
                }

                File.Delete(filePath);

                UIAlertView alert = new UIAlertView();
                alert.Title = "Information";
                alert.Message = "delete success";
                alert.AddButton("确认");
                alert.Show();
            };

            dirButton.TouchUpInside += (sender, e) =>
            {
                string homePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                string[] files = Directory.GetFiles(homePath);
                string[] dirs = Directory.GetDirectories(homePath);
                
                Console.WriteLine("show dirs : ");
                foreach (string dir in dirs)
                {
                    Console.WriteLine(dir);
                }
                Console.WriteLine("show files : ");
                foreach (string file in files)
                {
                    Console.WriteLine(file);
                }
                UIAlertView alert = new UIAlertView();
                alert.Title = "Information";
                alert.Message = "display in Console";
                alert.AddButton("确认");
                alert.Show();
            };


            createDirButton.TouchUpInside += (sender, e) => {
                string homePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                string dirpath = Path.Combine(homePath, fileName.Text);

                Directory.CreateDirectory(dirpath);

                UIAlertView alert = new UIAlertView();
                alert.Title = "information";
                alert.Message = "create success.";
                alert.AddButton("确认");
                alert.Show();
            };

        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}