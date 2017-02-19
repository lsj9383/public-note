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

            //Lock file for 10 minutes
            ActiveLock fileLock = resource.Lock("http://myserver/dav/file1.dat", Depth.Zero, 600);

            //Unlock file
            resource.Unlock("http://myserver/dav/file1.dat", fileLock);
        }
    }
}

