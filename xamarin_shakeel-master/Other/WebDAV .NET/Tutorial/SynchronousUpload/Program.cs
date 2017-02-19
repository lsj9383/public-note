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
            resource.Upload("http://myserver/dav/file1.txt", "c:\\file1.txt");
        }
    }
}

