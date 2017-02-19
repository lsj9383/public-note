using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace AsyncAwait
{
    class Program
    {
        static Task<string> GreetingTask()
        {
            return Task.Run<string>(() => { return "hello"; });
        }

        private async static void CallerWithAsync()
        {
            Thread.Sleep(1000);                     //主线程阻塞
            //主线程跑到await立即返回。
            string s = await GreetingTask();        //await该行是开多线程跑的代码，主线程在该处立即返回.
            Console.WriteLine(s);
            while (true) ;
        }

        static void Main(string[] args)
        {
            CallerWithAsync();
            Console.WriteLine("main thread done!");
        }
    }
}
