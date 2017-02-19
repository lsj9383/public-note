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

            //Rename file
            resource.Move("http://myserver/dav/file1.dat", "http://myserver/dav/file2.dat");

            //Move folder and all subfolders
            resource.Move("http://myserver/dav/MyFolder", "http://myserver/dav/backup/MyFolder");
        }
    }
}
