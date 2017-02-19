using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using Machine;

namespace BtnSimulation
{
    class Program
    {
        static string[] LocalFiles = {  "C:\\workspace\\Upload\\lsj\\System\\start-sys.txt",
                                        "C:\\workspace\\Upload\\lsj\\Person\\information.txt",
                                        "C:\\workspace\\Upload\\lsj\\Collect\\banana.txt",
                                        "C:\\workspace\\Upload\\lsj\\Collect\\KFC.txt",
                                        "C:\\workspace\\Upload\\lsj\\Collect\\MC.txt"};
        static string[] TargetFiles= {  "/System/start-sys.txt",
                                        "/Person/information.txt",
                                        "/Collect/banana.txt",
                                        "/Collect/KFC.txt",
                                        "/Collect/MC.txt"};

        static WebDavMachine machine;
        static JobQueue jobQueue = new JobQueue();
        static WebDavUpload EasyUpload;

        static void MainThread()
        {
            //主线程循环
            Console.WriteLine("Main Thread Loop Start");

            for (int loopCnt=0, cnt=0; true; loopCnt++)
            {
                Thread.Sleep(100);
                if (cnt < LocalFiles.Length && loopCnt%20==0)
                {
                    Console.WriteLine(String.Format("Add {0} File", cnt+1));
                    jobQueue.EnqueueJobs(new UploadJob("/lr/"+TargetFiles[cnt], LocalFiles[cnt]));
                    cnt++;
                }
                Console.WriteLine(jobQueue.JobsCount());
            }
        }

        static void TestMachine()
        {
            machine = new WebDavMachine("lr", jobQueue);
            

            machine.ErrorHandler = (sender, e) =>
            {
                Console.WriteLine("Happen Error : " + e.jobCode.ToString());
            };
            machine.Start();

            //主线程循环
            MainThread();
        }
        
        static void TestEasyUpload()
        {
            object root = new object();
            Boolean EnableTrans = true;

            EasyUpload = new WebDavUpload("lr", (sender, e) => 
            {
                lock (root)
                {
                    Console.WriteLine("Happen Error : " + e.jobCode.ToString());
                    EnableTrans = false;
                }
            });


            for (int loopCnt = 0, cnt = 0; true; loopCnt++)
            {
                Thread.Sleep(100);
                lock (root)
                {
                    if (EnableTrans && cnt < LocalFiles.Length && loopCnt % 20 == 0)
                    {
                        Console.WriteLine(String.Format("Add {0} File", cnt + 1));
                        EasyUpload.Upload(TargetFiles[cnt], LocalFiles[cnt]);
                        cnt++;
                    }
                    Console.WriteLine(EasyUpload.QueueSize());
                }
            }
        }

        static void TestError()
        {
            machine = new WebDavMachine("lr", jobQueue);
            machine.ErrorHandler = (sender, e) =>
            {
                Console.WriteLine("Happen Error : " + e.jobCode.ToString());
            };

            jobQueue.EnqueueJobs(new UploadJob("/lr/" + TargetFiles[0], LocalFiles[0]));
            machine.Start();
        }

        static void Main(string[] args)
        {
            TestMachine();
            //TestEasyUpload();

            //TestError();
        }
    }
}
