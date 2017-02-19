using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTask
{
    class Program
    {
        static void ThreadMain()
        {
            Thread.Sleep(5000);
            Console.WriteLine("Running is a main thread.");
        }

        static void ThreadBack()
        {
            Console.WriteLine("background.");
            Thread.Sleep(6000);
            Console.WriteLine("Running is a background thread.");
        }

        static void TestThread()
        {
            var threadmain = new Thread(ThreadMain);   //默认为前台进程
            var threadback = new Thread(ThreadBack) { Name = "background thread", IsBackground = true};
            threadmain.Start();
            threadback.Start();
            Console.WriteLine("main thread");
        }

        static void DoFirst()
        {
            Console.WriteLine("do something in Task1");
            Thread.Sleep(2000);
        }

        static void DoSecond(Task t)
        {
            Console.WriteLine("task1 status : {0}", t.Status);
            Console.WriteLine("do something in Task2");
            Thread.Sleep(2000);
        }

        static void TestTask()
        {
            Task t1 = new Task(DoFirst);
            Task t2 = t1.ContinueWith(DoSecond);        //后续任务都是后台模式
            t1.Start();
        }

        static void Main(string[] args)
        {
            //            TestThread();
            TestTask();
            Console.ReadLine();
        }
    }
}
