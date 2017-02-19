using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using Manager;

namespace BtnSimulation
{
    //初始化进行的操作：连接测试、文件夹检测、文件夹创建。
    enum StatusCode
    {
        READY,      //准备就绪状态。刚刚初始化完，以及其他webdav操作后的状态。
        SUCCESS,    //操作成功状态，这个是操作完成后，到READY前的过渡状态，事件处理完成后，SUCCESS转为READY.
        RUNNING,    //运行状态。开始Start后进入的状态，该状态用于实时检查文件队列。
        LINKERR,    //连接失败状态。进行初始化，在连接测试时失败。

        RETRY,      //重试状态。某些状态失败后，可以开启重试。
        UNKNOWN,    //未知错误状态。可尝试重新初始化以修复。
        EMPTY,      //空状态。刚刚创建对象，还没有初始化时候的状态
        BUSY        //繁忙状态。
    }

    class UploadFileTask
    {
        public string TargetFile { get; private set; }
        public string LocalFile { get; private set; }
        public int Life;
        public UploadFileTask(string tf, string lf)
        {
            TargetFile = tf;
            LocalFile = lf;
            Life = 2;           //默认两次重试机会
        }

        public UploadFileTask(string tf, string lf, int life)
        {
            TargetFile = tf;
            LocalFile = lf;
            Life = life;
        }
    }

    class WebDavProcess
    {
        private WebDavManager m_wManager;
        private Queue<UploadFileTask> FilesQueue = new Queue<UploadFileTask>();
        private string m_sUser;
        public StatusCode iStatus { get; private set; }
        private readonly string[] m_sFolders = {"/System/", "/Person/", "/Collect/" };

        public WebDavProcess(string user, string mUser, string mPassword, string server, string root)
        {
            m_sUser = user;
            for (int i = 0; i < m_sFolders.Length; i++)
            {
                m_sFolders[i] = "/" + user + m_sFolders[i];
            }
            iStatus = StatusCode.EMPTY;
            m_wManager = new WebDavManager(mUser, mPassword, server, root);
        }

        public void Start()
        {
            new Thread(ProcessLoopThread).Start();
        }

        public void TaskEqneue(UploadFileTask Files)
        {
            //对该方法上锁.不可有多个线程共同进行入队
            lock (FilesQueue)
            {
                FilesQueue.Enqueue(Files);
            }
        }

        public int TaskEqneueCount()
        {
            //对该方法上锁.不可有多个线程共同进行入队
            lock (FilesQueue)
            {
                return FilesQueue.Count;
            }
        }

        public void UserWebDavInitial(EventHandler eHandler)
        {
            iStatus = StatusCode.BUSY;
            Task<int> initTask = new Task<int>(UserWebDavInitialProcess);
            initTask.ContinueWith(t =>
            {
                if (t.Result == 1)
                {
                    iStatus = StatusCode.LINKERR;
                }
                else if (t.Result == 2)
                {
                    iStatus = StatusCode.UNKNOWN;
                }
                else
                {
                    iStatus = StatusCode.SUCCESS;
                }
                eHandler(this, null);
                iStatus = iStatus == StatusCode.SUCCESS ? StatusCode.READY : iStatus;
            });
            iStatus = StatusCode.BUSY;
            initTask.Start();
        }

        private int UserWebDavInitialProcess()
        {
            iStatus = StatusCode.BUSY;
            bool bFinish = false;
            int errStep = 0;
            List<string> noExistFolders = new List<string>();

            //检查连接
            m_wManager.SucceededHandler = (sender, e) => { bFinish = true; };
            m_wManager.FailedHandler    = (sender, e) => { bFinish = true; errStep = 1; };
            m_wManager.CheckLink();
            while (!bFinish) ;
            if (errStep != 0)
            {
                return errStep;
            }

            //检查文件夹
            for (int i = 0; i < m_sFolders.Length; i++)
            {
                bFinish = false;
                m_wManager.SucceededHandler = (sender, e) => { bFinish = true; };
                m_wManager.FailedHandler = (sender, e) =>
                {
                    noExistFolders.Add(e.Information);
                    bFinish = true;
                };
                m_wManager.Exist(m_sFolders[i], true);
                while (!bFinish) ;
            }

            //创建文件夹
            for (int i = 0; i < noExistFolders.Count; i++)
            {
                bFinish = false;
                m_wManager.SucceededHandler = (sender, e) => { bFinish = true; };
                m_wManager.FailedHandler    = (sender, e) => { bFinish = true; errStep = 2; };
                m_wManager.MkdirRec(noExistFolders[i]);
                while (!bFinish) ;
                if (errStep != 0)
                {
                    return errStep;
                }
            }
            
            return errStep;
        }
        
        private void ProcessLoopThread()
        {
            while(true)
            {
                switch (iStatus)
                {
                    //Normal Status
                    case StatusCode.BUSY:  break;                                       //no action
                    case StatusCode.READY: iStatus = StatusCode.RUNNING; break;
                    case StatusCode.EMPTY: UserWebDavInitialProcess(); break;           //Initial Block
                    case StatusCode.RUNNING: UploadFile(); break;                       //若有有文件上传，将会开启线程，并且iStatus会变为BUSY，直到上传任务结束

                    //Error Status
                    case StatusCode.RETRY: iStatus = StatusCode.RETRY;  break;
                    case StatusCode.LINKERR: break;
                    case StatusCode.UNKNOWN: break;
                    default:    break;
                }
            }
        }

        private void UploadFile()
        {
            if (FilesQueue.Count != 0)
            {
                iStatus = StatusCode.BUSY;

                UploadFileTask UploadTask = FilesQueue.Dequeue();
                m_wManager.SucceededHandler = (sender, e) =>
                {
                    iStatus = StatusCode.SUCCESS;
                    //无客户事件处理
                    iStatus = iStatus == StatusCode.SUCCESS ? StatusCode.READY : iStatus;
                };
                m_wManager.FailedHandler = (sender, e) =>
                {
                    int Life = UploadTask.Life;
                    if (Life > 0)
                    {   //还有生命
                        TaskEqneue(new UploadFileTask(UploadTask.TargetFile, UploadTask.LocalFile, Life-1));
                        iStatus = StatusCode.RETRY;
                    }
                    else
                    {   //error
                        iStatus = StatusCode.UNKNOWN;
                    }
                };
                m_wManager.Upload(UploadTask.TargetFile, UploadTask.LocalFile);
            }
        }
    }
}
