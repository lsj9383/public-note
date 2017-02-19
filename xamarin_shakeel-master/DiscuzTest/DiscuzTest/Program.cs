using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Discuz.Api;
using Discuz.Api.Entities;
using Discuz.Api.Methods;

namespace DiscuzTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Test();

            Console.ReadKey();
        }

        static async void Test()
        {
            ApiClient.SetBaseUrl("http://192.168.10.242/discuz");

            var method = new ForumIndex();
            var catalogs = await ApiClient.Execute(method);
            foreach (ForumCatalog c in catalogs)
            {
                Console.WriteLine(c.Name);

                foreach (Forum f in c.SubFourms)
                {
                    Console.WriteLine("--" + f.Name);

                    var method2 = new ForumDisplay()
                    {
                        ForumID = f.ID
                    };
                    var threads = await ApiClient.Execute(method2);
                    foreach (ThreadSummary s in threads)
                    {
                        Console.WriteLine("    ==========");
                        Console.WriteLine("    " + s.Subject);

                        var method3 = new ViewThread()
                        {
                            ThreadID = s.ID
                        };
                        var posts = await ApiClient.Execute(method3);
                        foreach (ThreadPost p in posts)
                        {
                            if (!p.IsFirst)
                            {
                                continue;
                            }

                            Console.WriteLine("      " + p.Content.Replace("\r\n", ""));

                            if (p.Attachments != null)
                            {
                                foreach (KeyValuePair<string, Attachement> a in p.Attachments)
                                {
                                    Console.WriteLine("      ** " + a.Value.Url);
                                }
                            }

                            Console.WriteLine();
                        }
                    }
                }

                Console.WriteLine();
            }
        }
    }
}
