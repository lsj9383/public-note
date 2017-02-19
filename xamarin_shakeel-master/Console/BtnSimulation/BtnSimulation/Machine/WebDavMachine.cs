using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;
using Machine.StateImpl;
using Manager;
using System.Collections;

namespace Machine
{
    class WebDavMachineEventArgs : EventArgs
    {
        public string sRemark { get; private set; }
        public JobCode jobCode { get; private set; }

        public WebDavMachineEventArgs(JobCode jobCode, string sRemakr)
        {
            this.sRemark = sRemark;
            this.jobCode = jobCode;
        }

        public WebDavMachineEventArgs(JobCode jobCode) : this(jobCode, "") { }
    }

    class WebDavMachine
    {
        // State
        public readonly State EMPTY = null;
        public readonly State BUSY = null;
        public readonly State ERROR = null;
        public readonly State LOOP = null;

        public State MachineState { get; private set; }

        // Machine Property
        public WebDavManager m_wWebDav { get; private set; }
        public JobQueue jobQueue { get; private set; }
        public readonly string[] m_sFolders = { "/System/", "/Person/", "/Collect/" };
        public string User { get; private set; }
        private WebDavMachineEventArgs eventArgs;
        public Thread RunThread { get; private set; }
        public EventHandler<WebDavMachineEventArgs> ErrorHandler;
        private Job CurrentJob;
        private int StartTimes = 0;

        // Webdav configuration
        static private string m_sWebDavAdminUser = "admin";
        static private string m_sWebDavAdminPassword = "admin";
        static private string m_sServer = "192.168.10.242";
        static private string m_sRoot = "/owncloud/remote.php/webdav/KV/";

        public WebDavMachine(string user, JobQueue jobQueue)
        {
            //webdav configuration
            User = user;
            for (int i = 0; i < m_sFolders.Length; i++)
            {
                m_sFolders[i] = "/" + user + m_sFolders[i];
            }
            m_wWebDav = new WebDavManager(m_sWebDavAdminUser, m_sWebDavAdminPassword, m_sServer, m_sRoot);
            this.jobQueue = jobQueue;

            //state configuration

            EMPTY = new Empty(this);
            BUSY = new Busy(this);
            ERROR = new Error(this);
            LOOP = new Loop(this);

            MachineState = EMPTY;

            //RunThread
            RunThread = new Thread(() =>
            {
                while (true)
                {
                    MachineState.Next();
                    Thread.Sleep(50);
                }
            });
        }

        public bool Start()
        {   //运行
            if (StartTimes == 0)
            {
                StartTimes = 1;
                RunThread.Start();
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public void Stop()
        {
            if (RunThread.IsAlive)
            {
                ;
            }
        }

        //状态转移
        public void SetState(State newState)
        {
            MachineState = newState;
        }

        public void SetCurrentJob(Job job)
        {
            CurrentJob = job;
        }

        public Job GetCurrentJob()
        {
            return CurrentJob;
        }

        public void SetEventArgs(WebDavMachineEventArgs eventArgs)
        {
            this.eventArgs = eventArgs;
        }

        public WebDavMachineEventArgs GetEventArgs()
        {
            return eventArgs;
        }
    }
}
