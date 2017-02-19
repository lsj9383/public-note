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

            Property[] properties = resource.GetProperties("http://myserver/dav/file1.dat");

            for (int i = 0; i < properties.Length; i++)
            {
                Console.WriteLine(properties[i].Name);
                Console.WriteLine(properties[i].Namespace);
                Console.WriteLine(properties[i].Value);
                Console.WriteLine("-------------------------------------------------------------");
            }

            Console.Read();
        }
    }
}

