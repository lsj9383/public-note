using System;
using System.Net;
using Independentsoft.Webdav;

namespace Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            NetworkCredential credential = new NetworkCredential("admin", "admin");
            WebdavSession session = new WebdavSession(credential);
            Resource resource = new Resource(session);

            string[] list = resource.List("http://192.168.10.242/owncloud/remote.php/webdav/");

            for (int i = 0; i < list.Length; i++)
            {
                Console.WriteLine(list[i]);
            }

            //Press ENTER to exit.
            Console.Read();
        }
    }
}
