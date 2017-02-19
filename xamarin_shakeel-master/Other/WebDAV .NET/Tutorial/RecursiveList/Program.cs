using System;
using System.Net;
using Independentsoft.Webdav;

namespace Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            NetworkCredential credential = new NetworkCredential("username", "password");
            WebdavSession session = new WebdavSession(credential);
            Resource resource = new Resource(session);

            string[] list = resource.List("http://myserver/dav/", true);

            for (int i = 0; i < list.Length; i++)
            {
                Console.WriteLine(list[i]);
            }

            //Press ENTER to exit.
            Console.Read();
        }
    }
}
