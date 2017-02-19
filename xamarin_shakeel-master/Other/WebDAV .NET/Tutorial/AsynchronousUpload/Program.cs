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

            resource.UploadProgress += new UploadProgressEventHandler(UploadProgress);

            try
            {
                resource.UploadFile("http://myserver/dav/file1.txt", "c:\\temp\\file1.txt");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Read();
            }

            //Wait till upload is finished and press ENTER to exit.
            Console.Read();
        }

        private static void UploadProgress(object sender, ProgressEventArgs e)
        {
            if (e.Exception != null)
            {
                Console.WriteLine(e.Exception.Message);
                Console.Read();
            }
            else if (e.IsComplete)
            {
                Console.WriteLine("Upload completed");
            }
            else
            {
                Console.WriteLine(e.Progress);
            }
        }
    }
}



