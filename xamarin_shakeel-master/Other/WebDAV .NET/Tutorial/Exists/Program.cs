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

            bool exist = resource.Exists("http://myserver/dav/MyFolder");

            if (exist)
            {
                Console.WriteLine("Folder exists");
            }
            else
            {
                Console.WriteLine("Folder does not exist");
            }

            Console.Read();
        }
    }
}

