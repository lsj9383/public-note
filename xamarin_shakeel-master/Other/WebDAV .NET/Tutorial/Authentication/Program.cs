using System;
using System.Net;
using Independentsoft.Webdav;

namespace Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            //Basic or Digest authentication
            NetworkCredential credential = new NetworkCredential("username", "password");

            //Windows Integrated Authentication
            //ICredentials credential = CredentialCache.DefaultCredentials;

            WebdavSession session = new WebdavSession(credential);
            Resource resource = new Resource(session);

            string[] list = resource.List("http://myserver/dav/");

            for (int i = 0; i < list.Length; i++)
            {
                Console.WriteLine(list[i]);
            }

            //Press ENTER to exit.
            Console.Read();
        }
    }
}
