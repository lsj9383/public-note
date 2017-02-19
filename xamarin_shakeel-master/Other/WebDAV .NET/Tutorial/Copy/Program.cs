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

            //Copy file
            resource.Copy("http://myserver/dav/file1.dat", "http://myserver/dav/backup/file1.dat");

            //Copy folder and all subfolders
            resource.Copy("http://myserver/dav/MyFolder", "http://myserver/dav/backup/MyFolder", true, true);
        }
    }
}

