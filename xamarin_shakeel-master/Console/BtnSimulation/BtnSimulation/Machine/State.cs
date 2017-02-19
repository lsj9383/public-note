using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;

namespace Machine.StateImpl
{
    abstract class State
    {
        protected WebDavMachine m_wMachine;

        public State(WebDavMachine wMachine)
        {
            m_wMachine = wMachine;
        }
        
        abstract public void Next();
    }

    class Loop : State
    {
        public Loop(WebDavMachine wMachine) : base(wMachine) { }

        public override void Next()
        {
            if (m_wMachine.jobQueue.JobsCount() > 0)
            {
                m_wMachine.SetState(m_wMachine.BUSY);               //设为忙状态
                Job job = m_wMachine.jobQueue.DequeueJobs();        //提取新job
                m_wMachine.SetCurrentJob(job);                      //将新job设置为当前job

                job.Execute(m_wMachine);                            //处理job
            }
        }
    }

    class Busy : State
    {
        public Busy(WebDavMachine wMachine) : base(wMachine) { }

        public override void Next()
        {
            if (m_wMachine.GetCurrentJob().JobTask.IsCompleted)
            {
                bool result = true;

                if (m_wMachine.GetCurrentJob().JobTask.IsFaulted)
                {
                    result = false;
                }
                else
                {
                    result = m_wMachine.GetCurrentJob().JobTask.Result;
                }
                
                JobCode jobCode = m_wMachine.GetCurrentJob().jobCode;

                m_wMachine.SetCurrentJob(null);                        //Job执行完成，则将当前Job其清空。

                if (result)
                {   //成功
                    m_wMachine.SetState(m_wMachine.LOOP);
                }
                else
                {   //失败
                    m_wMachine.SetEventArgs(new WebDavMachineEventArgs(jobCode));
                    m_wMachine.SetState(m_wMachine.ERROR);
                }

                if (jobCode == JobCode.INITIAL)
                {
                    Console.WriteLine("Initial : " + result);
                }
                else if(jobCode == JobCode.UPLOAD)
                {
                    Console.WriteLine("Have " + m_wMachine.jobQueue.JobsCount());
                }
            }
            else
            {
                ;       //Not Complete, Keep BUSY, No Action.
            }
        }
    }

    class Empty : State
    {
        public Empty(WebDavMachine wMachine) : base(wMachine) { }

        public override void Next()
        {
            m_wMachine.jobQueue.EnqueueJobs(new InitJob());
            m_wMachine.SetState(m_wMachine.LOOP);
        }
    }

    class Error : State
    {
        public Error(WebDavMachine wMachine) : base(wMachine) { }

        public override void Next()
        {
            m_wMachine.ErrorHandler(m_wMachine, m_wMachine.GetEventArgs());

            JobCode jobCode = m_wMachine.GetEventArgs().jobCode;
            if (jobCode == JobCode.INITIAL)
            {   //初始化失败....kill self
                Thread.CurrentThread.Abort();
            }
            else
            {
                m_wMachine.SetState(m_wMachine.LOOP);
            }
        }
    }
}
