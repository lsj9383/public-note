using System;
using System.IO;
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

            Stream output = resource.GetOutputStream("http://myserver/dav/file1.dat");

            FileStream file = new FileStream("c:\\file1.dat", FileMode.Open);

            byte[] buffer = new byte[2048];
            int len = 0;

            while ((len = file.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
                output.Flush();
            }

            output.Close();
            file.Close();

            //Press ENTER to exit.
            Console.Read();
        }
    }
}

