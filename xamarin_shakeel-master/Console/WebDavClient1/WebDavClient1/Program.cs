using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebDAVClient;
using System;
using Manager;

namespace WebDavClient1
{
    class Program
    {
        static async void Test1()
        {
            IClient c = new Client(new NetworkCredential { UserName = "admin", Password = "admin" });
            c.Server = "http://192.168.10.242";
            c.BasePath = "/owncloud/remote.php/webdav/";
            var files = await c.List();
            foreach (var file in files)
            {
                Console.WriteLine(file.Href);
            }
        }

        static async void Test2()
        {
            IClient c = new Client(new NetworkCredential { UserName = "admin", Password = "admin" });
            c.Server = "http://192.168.10.242";
            c.BasePath = "/owncloud/remote.php/webdav/";
            var folderFiles = await c.List(c.BasePath + "/KV/");
            foreach (var file in folderFiles)
            {
                Console.WriteLine(file.Href);
            }
        }

        static async void Test3()
        {
            IClient c = new Client(new NetworkCredential { UserName = "admin", Password = "admin" });
            c.Server = "http://192.168.10.242";
            c.BasePath = "/owncloud/remote.php/webdav/";
            var files = await c.List(c.BasePath);
            var folder = files.FirstOrDefault(f => f.Href.EndsWith("KV/"));
            if (folder == null)
                Console.WriteLine("null");
            else
                Console.WriteLine(folder.Href);
        }

        static async void Test4()
        {
            IClient c = new Client(new NetworkCredential { UserName = "admin", Password = "admin" });
            c.Server = "http://192.168.10.242";
            c.BasePath = "/owncloud/remote.php/webdav/";
            var folderCreated = await c.CreateDir(c.BasePath + "/KV/", "nice");
            Console.WriteLine("ok");
        }

        static async void Test5()
        {
            IClient c = new Client(new NetworkCredential { UserName = "admin", Password = "admin" });
            c.Server = "http://192.168.10.242";
            c.BasePath = "/owncloud/remote.php/webdav/";
            var fileUploaded = await c.Upload(c.BasePath + "/KV/", File.OpenRead("C:/workspace/KFC.txt"), "KFC.txt");
            Console.WriteLine("result : " + fileUploaded);
        }

        static async void Test6()
        {
            IClient c = new Client(new NetworkCredential { UserName = "admin", Password = "admin" });
            c.Server = "http://192.168.10.242";
            c.BasePath = "/owncloud/remote.php/webdav/";
            var result = await c.GetFolder(c.BasePath+"/KV2/");
            
            if (result == null)
            {
                Console.WriteLine("null");
            }
            else
            {
                Console.WriteLine(result.Href);
            }
        }

        static async void Test7()
        {
  //          IClient c = new Client(new NetworkCredential { UserName = "admin", Password = "admin" });
  //          c.Server = "http://192.168.10.242";
  //          c.BasePath = "/owncloud/remote.php/webdav/";
  //          var stream = await client.Download(folderFile.Href);
        }
        delegate void action(int i);

        static private void fei(int i) { Console.WriteLine("hello : " + i); }

        static void Main(string[] args)
        {
            TestLink();
            //TestUpload();
            //TestExist();
            //TestMkdir();
            //TestMkdirRec();


            Console.WriteLine("done");
            Console.ReadLine();
        }

        static void TestLink()
        {
            WebDavManager manager = new WebDavManager("admin", "admin", "http://192.168.10.242", "/owncloud/remote.php/webdav/KV/");
            manager.SucceededHandler = (sender, e) => { Console.WriteLine("Link Success"); };
            manager.FailedHandler = (sender, e) => { Console.WriteLine("Link Failed"); };
            manager.CheckLink();
        }

        static void TestUpload()
        {
            WebDavManager manager = new WebDavManager("admin", "admin", "http://192.168.10.242", "/owncloud/remote.php/webdav/KV/");
            manager.SucceededHandler = (sender, e) => { Console.WriteLine("Upload Success"); };
            manager.FailedHandler = (sender, e) => { Console.WriteLine("Upload Failed"); };
            manager.Upload("/", "fxx2", "C:/workspace/KFC.txt");
        }

        static void TestExist()
        {
            WebDavManager manager = new WebDavManager("admin", "admin", "192.168.10.242", "/owncloud/remote.php/webdav/");
            manager.SucceededHandler = (sender, e) => { Console.WriteLine("Exist Success"); };
            manager.FailedHandler = (sender, e) => { Console.WriteLine("Exist Handler"); };
            manager.Exist("/KV/lsj/System/", true);
        }

        static void TestMkdir()
        {
            WebDavManager manager = new WebDavManager("admin", "admin", "http://192.168.10.242", "/owncloud/remote.php/webdav/");
            manager.SucceededHandler = (sender, e) => { Console.WriteLine("Mkdir Success"); };
            manager.FailedHandler = (sender, e) => { Console.WriteLine("Mkdir Failed"); };
            manager.Mkdir("/KV/aa/");
        }

        static void TestMkdirRec()
        {
            WebDavManager manager = new WebDavManager("admin", "admin", "192.168.10.242", "/owncloud/remote.php/webdav/");
            manager.SucceededHandler = (sender, e) => { Console.WriteLine("RecMkdir Success"); };
            manager.FailedHandler = (sender, e) => { Console.WriteLine("RecMkdir Failed"); };
            manager.MkdirRec("/KV/n/nn/nnn/nnnn");
        }
    }
}
