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

            PropertyName[] propertyName = new PropertyName[5];

            propertyName[0] = DavProperty.DisplayName;
            propertyName[1] = DavProperty.CreationDate;
            propertyName[2] = DavProperty.GetLastModified;
            propertyName[3] = DavProperty.GetContentLength;
            propertyName[4] = DavProperty.IsCollection;

            ResourceInfo[] info = resource.List("http://myserver/dav", propertyName);

            for (int i = 0; i < info.Length; i++)
            {
                Property displayName = info[i].Properties[DavProperty.DisplayName];
                Property creationDate = info[i].Properties[DavProperty.CreationDate];
                Property getLastModified = info[i].Properties[DavProperty.GetLastModified];
                Property getContentLength = info[i].Properties[DavProperty.GetContentLength];
                Property isCollection = info[i].Properties[DavProperty.IsCollection];

                Console.WriteLine(info[i].Address);

                if (displayName != null)
                {
                    Console.WriteLine(displayName.Value);
                }

                if (creationDate != null)
                {
                    Console.WriteLine(creationDate.Value);
                }

                if (getLastModified != null)
                {
                    Console.WriteLine(getLastModified.Value);
                }

                if (getContentLength != null)
                {
                    Console.WriteLine(getContentLength.Value);
                }

                if (isCollection != null)
                {
                    Console.WriteLine(isCollection.Value);
                }

                Console.WriteLine("-------------------------------------------------------------------");
            }

            //Press ENTER to exit.
            Console.Read();
        }
    }
}
