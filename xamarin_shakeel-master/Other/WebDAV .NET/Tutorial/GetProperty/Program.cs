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

            Property sizeProperty = resource.GetProperty("http://myserver/dav/file1.dat", DavProperty.GetContentLength);
            Console.WriteLine("File size = " + sizeProperty.Value);

            PropertyName myPropertyName = new PropertyName("myproperty", "independentsoft:");
            Property myProperty = resource.GetProperty("http://myserver/dav/file1.dat", myPropertyName);
            Console.WriteLine("My property = " + myProperty.Value);

            //Press ENTER to exit
            Console.Read();
        }
    }
}

