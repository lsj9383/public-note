using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace httpclient
{
    class Program
    {
        static async void GetData()
        {
            HttpClient httpclient = new HttpClient();

            HttpResponseMessage response = await httpclient.GetAsync("http://www.baidu.com");
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("yes");
                Console.WriteLine( response.Content.ReadAsStringAsync().Result );
            }
            else
            {
                Console.WriteLine("no");
            }
        }

        

        static void Main(string[] args)
        {
            GetData();
            Console.ReadLine();
        }
    }
}
