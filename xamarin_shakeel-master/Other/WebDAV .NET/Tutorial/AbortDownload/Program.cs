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

            resource.DownloadProgress += new DownloadProgressEventHandler(DownloadProgress);

            try
            {
                FileDownload fileDownload = resource.DownloadFile("http://myserver/dav/file1.txt", "c:\\temp\\file1.txt");

                //Press ENTER to abort download
                Console.Read();
                fileDownload.Abort();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.Read();
            }

            //Press ENTER to exit.
            Console.Read();
        }

        private static void DownloadProgress(object sender, ProgressEventArgs e)
        {
            if (e.Exception != null)
            {
                Console.WriteLine(e.Exception.Message);
                Console.Read();
            }
            else if (e.IsComplete)
            {
                Console.WriteLine("Download completed");
            }
            else
            {
                Console.WriteLine(e.Progress);
            }
        }
    }
}
