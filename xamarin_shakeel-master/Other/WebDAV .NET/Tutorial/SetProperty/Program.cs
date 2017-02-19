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

            //Set property
            Property myProperty = new Property("myproperty", "independentsoft:", "1.0");
            resource.SetProperty("http://myserver/dav/file1.dat", myProperty);

            //Set array of properties
            Property[] propertyArray = new Property[5];
            propertyArray[0] = new Property("property1", "mynamespace:", "value1");
            propertyArray[1] = new Property("property2", "mynamespace:", "value2");
            propertyArray[2] = new Property("property3", "mynamespace:", "value3");
            propertyArray[3] = new Property("property4", "mynamespace:", "value4");
            propertyArray[4] = new Property("property5", "mynamespace:", "value5");

            resource.SetProperty("http://myserver/dav/file1.dat", propertyArray);
        }
    }
}
