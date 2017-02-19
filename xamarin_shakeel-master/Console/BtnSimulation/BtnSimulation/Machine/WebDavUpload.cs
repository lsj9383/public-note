using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Machine
{
    class WebDavUpload
    {
        private JobQueue jobQueue;
        private readonly WebDavMachine Machine;
        private string m_sUser;

        public WebDavUpload(string sUser, EventHandler<WebDavMachineEventArgs> ErrorHandler)
        {
            jobQueue = new JobQueue();
            Machine = new WebDavMachine(sUser, jobQueue);
            Machine.ErrorHandler = ErrorHandler;
            m_sUser = sUser;
        }

        public WebDavUpload(WebDavMachine Machine, JobQueue jobQueue)
        {
            this.jobQueue = jobQueue;
            this.Machine = Machine;
            m_sUser = Machine.User;
        }

        public void Upload(string File, string localFile)
        {
            string TargetFile = "/" + m_sUser + "/" + File;

            if (Machine.RunThread.IsAlive == false)
            {
                if (false == Machine.Start())        //开启线程
                {   //线程开启失败
                    return;
                }
                while (Machine.MachineState == Machine.EMPTY) Thread.Sleep(80);    //确保线程启动，并且到LOOP状态
            }

            jobQueue.EnqueueJobs(new UploadJob(TargetFile, localFile));  //添加job
        }

        public int QueueSize()
        {
            return jobQueue.JobsCount();
        }
    }
}
