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

            string xml = "<i:Software xmlns:i=\"independentsoft:\"><i:Name>WebDAV .NET</i:Name>" +
            "<i:Version>1.0</i:Version><i:Date>01.01.2005</i:Date></i:Software>";

            Property myProperty = new Property(xml);
            resource.SetProperty("http://myserver/dav/file1.dat", myProperty);
        }
    }
}

