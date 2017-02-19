using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Machine
{
    enum JobCode
    {
        UPLOAD,
        INITIAL
    }

    abstract class Job
    {
        public JobCode jobCode { get; protected set; }
        public Task<bool> JobTask { get; protected set; }
        public abstract void Execute(WebDavMachine Machine);
    }

    class JobQueue
    {
        Queue<Job> Jobs = new Queue<Job>();

        public void EnqueueJobs(Job job)
        {
            lock (this)
            {
                Jobs.Enqueue(job);
            }
        }

        public Job DequeueJobs()
        {
            lock (this)
            {
                return Jobs.Dequeue();
            }
        }

        public int JobsCount()
        {
            lock (this)
            {
                return Jobs.Count;
            }
        }
    }

    class UploadJob : Job
    {
        string TargetFile;
        string LocalFile;
        int Life;

        public UploadJob(string tf, string lf)
        {
            jobCode = JobCode.UPLOAD;
            TargetFile = tf;
            LocalFile = lf;
            Life = 3;           //3次机会
        }

        public UploadJob(string tf, string lf, int life)
        {
            TargetFile = tf;
            LocalFile = lf;
            Life = life;           //3次提交
        }

        public override void Execute(WebDavMachine Machine)
        {
            Machine.SetState(Machine.BUSY);             //在开启线程前，必须确保状态已经是BUSY

            Machine.m_wWebDav.SucceededHandler = (sender, e) =>
            {
            };

            Machine.m_wWebDav.FailedHandler = (sender, e) =>
            {
                if (Life > 0)
                {
                    Machine.jobQueue.EnqueueJobs(new UploadJob(TargetFile, LocalFile, Life - 1));              //还有生命，就将Job入队，以重新操作
                }
            };

            JobTask = Machine.m_wWebDav.Upload(TargetFile, LocalFile);
        }
    }

    class InitJob : Job
    {
        private WebDavMachine m_wMachine;

        public InitJob()
        {
            jobCode = JobCode.INITIAL;
        }

        public override void Execute(WebDavMachine Machine)
        {
            m_wMachine = Machine;
            Machine.SetState(Machine.BUSY);             //在开启线程前，必须确保状态已经是BUSY
            Task<bool> task = new Task<bool>(InitialProcess);
            task.Start();

            JobTask = task;
        }

        private bool InitialProcess()
        {
            int bFinish = 0;
            List<string> noExistFolders = new List<string>();

            //检查连接
            m_wMachine.m_wWebDav.SucceededHandler = (sender, e) => { bFinish = 1; };
            m_wMachine.m_wWebDav.FailedHandler = (sender, e) => { bFinish = -1; };
            m_wMachine.m_wWebDav.CheckLink() ;
            while (bFinish == 0) ;
            if (bFinish == -1)
            {
                return false;
            }

            //检查文件夹
            for (int i = 0; i < m_wMachine.m_sFolders.Length; i++)
            {
                bFinish = 0;
                m_wMachine.m_wWebDav.SucceededHandler = (sender, e) => { bFinish = 1; };
                m_wMachine.m_wWebDav.FailedHandler = (sender, e) =>
                {
                    noExistFolders.Add(e.Information);
                    bFinish = -1;
                };
                m_wMachine.m_wWebDav.Exist(m_wMachine.m_sFolders[i], true);
                while (bFinish == 0) ;
            }

            //创建文件夹
            for (int i = 0; i < noExistFolders.Count; i++)
            {
                bFinish = 0;
                m_wMachine.m_wWebDav.SucceededHandler = (sender, e) => { bFinish = 1; };
                m_wMachine.m_wWebDav.FailedHandler = (sender, e) => { bFinish = -1; };
                m_wMachine.m_wWebDav.MkdirRec(noExistFolders[i]);
                while (bFinish == 0) ;
                if (bFinish == -1)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
