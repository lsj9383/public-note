using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebDAVClient;
using System;
using System.Threading;
using System.Collections.Generic;
using WebDAVClient.Model;

namespace Manager
{
    class WebDavEventArgs : EventArgs
    {
        public bool isSuccess { get; private set; }
        public string Information { get; private set; }

        public WebDavEventArgs(bool isSuccess)
        {
            this.isSuccess = isSuccess;
        }

        public WebDavEventArgs(bool isSuccess, string Information)
        {
            this.isSuccess = isSuccess;
            this.Information = Information;
        }

    }

    delegate void AgentSucceededHandler(object sender, WebDavEventArgs e);
    delegate void AgentFailedHandler(object sender, WebDavEventArgs e);

    class WebDavManager
    {
        private string m_sHsot;
        private string m_sRoot;
        private IClient m_cClient;
        private bool m_bConnect;

        public EventHandler<WebDavEventArgs> SucceededHandler = (sender, e) => { };
        public EventHandler<WebDavEventArgs> FailedHandler = (sender, e) => { };


        public WebDavManager(string strUser, string strPassword, string strHost, string strRoot)
        {
            m_sHsot = strHost;
            m_sRoot = strRoot;

            m_cClient = new Client(new NetworkCredential { UserName = strUser, Password = strPassword });
            m_cClient.Server = strHost;     //  https://xxxx.xxxx.xxxx.xxxx or xxxx.xxxx.xxxx.xxxx
            m_cClient.BasePath = strRoot;

            EmptyHandler();
        }

        public void EmptyHandler()
        {
            SucceededHandler = (sender, e) => { };
            FailedHandler = (sender, e) => { };
        }

        public void CheckLink()
        {
            Task task = m_cClient.List();
            task.ContinueWith(t =>
            {
                m_bConnect = !t.IsFaulted;
                CallEventHanlder(new WebDavEventArgs(!t.IsFaulted));
            });
        }

        public void Upload(string targetPath, string fileName, string LocalFile)
        {
            string absoluteTargetFile = m_sRoot + targetPath;
            Task<bool> task = m_cClient.Upload(absoluteTargetFile, File.OpenRead(LocalFile), fileName);
            task.ContinueWith(t =>
            {
                CallEventHanlder(new WebDavEventArgs(!t.IsFaulted));
            });
        }

        public void Upload(string targetFile, string LocalFile)
        {
            string sFileName = null;
            string sFolder = null;
            Folder(targetFile, out sFolder, out sFileName);
            string absoluteTargetFolder = m_sRoot + sFolder;

            Task<bool> task = m_cClient.Upload(absoluteTargetFolder, File.OpenRead(LocalFile), sFileName);
            task.ContinueWith(t =>
            {
                CallEventHanlder(new WebDavEventArgs(!t.IsFaulted));
            });
        }

        public void Exist(string target, bool isFolder)
        {
            string prevPath = null;
            string currentPath = null;
            Folder(target, out prevPath, out currentPath);

            string absolutePrevPath = m_sRoot + prevPath;
            currentPath = isFolder ? currentPath + "/" : currentPath;
            Task<IEnumerable<Item>> task = m_cClient.List(absolutePrevPath);
            task.ContinueWith(t =>
            {
                bool IsFaulted = true;
                if (!t.IsFaulted)
                {
                    var res = t.Result.FirstOrDefault(f => f.Href.EndsWith(currentPath));
                    IsFaulted = res == null ? true : false;
                }

                CallEventHanlder(new WebDavEventArgs(!IsFaulted, target));
            });
        }

        public void Mkdir(string targetFolder)
        {
            string prevFolder = null;
            string currentFolder = null;
            Folder(targetFolder, out prevFolder, out currentFolder);

            string absolutePrevFolder = m_sRoot + prevFolder;

            Task<bool> task = m_cClient.CreateDir(absolutePrevFolder, currentFolder);
            task.ContinueWith(t =>
            {
                bool IsFaulted = true;
                if (!t.IsFaulted)
                {
                    IsFaulted = t.Result == false ? true : false;
                }

                CallEventHanlder(new WebDavEventArgs(!IsFaulted));
            });
        }

        public void Mkdir(string targetFolder, EventHandler<WebDavEventArgs> eHandler)
        {
            string prevFolder = null;
            string currentFolder = null;
            Folder(targetFolder, out prevFolder, out currentFolder);

            string absolutePrevFolder = m_sRoot + prevFolder;

            Task<bool> task = m_cClient.CreateDir(absolutePrevFolder, currentFolder);
            task.ContinueWith(t =>
            {
                bool IsFaulted = true;
                if (!t.IsFaulted)
                {
                    IsFaulted = t.Result == false ? true : false;
                }

                eHandler(this, new WebDavEventArgs(!IsFaulted));
            });
        }

        public void MkdirRec(string targetFolder)
        {
            Task<bool> task = MkdirRecTask(targetFolder);
            task.ContinueWith(t =>
            {
                CallEventHanlder(new WebDavEventArgs(t.Result));
            });
        }

        private Task<bool> MkdirRecTask(string targetFolder)
        {
            return Task.Run<bool>(() =>
            {
                return MkdirRecProcess(targetFolder, 0);
            });
        }

        private bool MkdirRecProcess(string targetFolder, int deep)
        {   //返回当前目录创建是否成功
            bool bFinish = false;
            bool bSuccess = false;
            Mkdir(targetFolder, (sender, e) =>
            {
                bSuccess = e.isSuccess;
                bFinish = true;
            });
            while (!bFinish) ;

            if (!bSuccess)
            {   //未成功，创建父级目录
                bool reFinish = false;
                bool reSuccess = false;
                string prevFolder = null;
                string currentFolder = null;

                Folder(targetFolder, out prevFolder, out currentFolder);
                if (prevFolder.Equals("/"))
                {   // 要创建根目录"/",因此肯定不是因为根目录不在而使文件夹创建失败
                    return false;
                }

                if (!MkdirRecProcess(prevFolder, deep + 1))
                {   //递归创建失败
                    return false;
                }
                else
                {   //父级的递归创建成功，重新创建当前文件夹
                    Mkdir(targetFolder, (obj, eve) => { reSuccess = eve.isSuccess; reFinish = true; });
                    while (!reFinish) ;
                    return reSuccess;   //返回当前文件夹创建情况
                }
            }

            return true;
        }

        private void Folder(string origin, out string prevPath, out string currentPath)
        {   //当前文件或文件夹所在的文件夹
            prevPath = "/";
            currentPath = "";

            List<string> subString = new List<string>(origin.Split('/'));
            subString.RemoveAll(st => { return st.Equals(""); });

            for (int i = 0; i < subString.Count - 1; i++)
            {
                prevPath += subString[i] + "/";
            }
            if (subString.Count == 0)
            {
                currentPath = "";
            }
            else
            {
                currentPath = subString[subString.Count - 1];
            }
        }

        private void CallEventHanlder(WebDavEventArgs eve)
        {
            if (eve.isSuccess)
            {   //成功
                SucceededHandler(this, eve);
            }
            else
            {   //失败
                FailedHandler(this, eve);
            }
        }
    }
}
