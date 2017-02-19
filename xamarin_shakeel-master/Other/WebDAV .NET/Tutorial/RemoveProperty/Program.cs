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

            PropertyName myPropertyName = new PropertyName("myproperty", "independentsoft:");
            resource.RemoveProperty("http://myserver/dav/file1.dat", myPropertyName);
        }
    }
}

