using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;
using Newtonsoft.Json.Linq;

using System.Runtime.Serialization.Json;
using System.IO;

namespace DiscuzAPI
{

    class DiscuzEventArgs : EventArgs
    {
        public bool isSuccess = false;
        public object ResponseParseObject { get; private set; }

        public DiscuzEventArgs(object obj)
        {
            if (obj != null) { isSuccess = true; }
            ResponseParseObject = obj;
        }
    }

    class DiscuzManager
    {
        string m_sServer = "192.168.10.242";
        public string LocalRoot { get; private set; }

        public DiscuzManager(string Server, string localRoot)
        {
            m_sServer = Server;
            LocalRoot = localRoot;
        }

        public void GetContent(int tid, EventHandler<DiscuzEventArgs> eHandler)
        {
            AsyncGetJsonResponse(DetailUrl(tid), (sender, e) =>
            {   //异步方式，获得帖子内容的信息Json，以开启多线程

                MessageDetail messageDetial = null;
                try
                {
                    var jsonObj = e.ResponseParseObject as JObject;
                    var replies = jsonObj["Variables"]["postlist"].Count() - 1;             //若上步错误，这里是会抛出异常的，立即终止
                    var uid     = (int)jsonObj["Variables"]["postlist"][0]["authorid"];
                    var Author  = jsonObj["Variables"]["postlist"][0]["author"].ToString();
                    var Title   = jsonObj["Variables"]["thread"]["subject"].ToString();
                    var Message = jsonObj["Variables"]["postlist"][0]["message"].ToString();

                    var locaHeadIcon = String.Format("{0}/{1}.gif", LocalTmpUsersIcon(), Author);
                    SaveAndCloseStream(locaHeadIcon, SyncGetPhotoResponse(String.Format("http://{0}/discuz/uc_server/avatar.php?uid={1}", m_sServer, uid)));    //同步获取照片若错误，会抛出异常。
                    MessageProfile profile = new MessageProfile(new UserInfo(locaHeadIcon, Author), tid, Title, Message);
                    messageDetial = new MessageDetail(profile);

                    //加载回复
                    for (int i = 0; i < replies; i++)
                    {
                        var replyUid     = (int)jsonObj["Variables"]["postlist"][i + 1]["authorid"];
                        var replyAuthor  = jsonObj["Variables"]["postlist"][i + 1]["author"].ToString();
                        var replyTitle   = "Reply";
                        var replyMessage = jsonObj["Variables"]["postlist"][i + 1]["message"].ToString();

                        var localReplyHeadIcon = String.Format("{0}/{1}.gif", LocalTmpUsersIcon(), replyAuthor);
                        SaveAndCloseStream(localReplyHeadIcon, SyncGetPhotoResponse(String.Format("http://{0}/discuz/uc_server/avatar.php?uid={1}", m_sServer, replyUid)));
                        MessageProfile replyProfile = new MessageProfile(new UserInfo(localReplyHeadIcon, replyAuthor), 0, replyTitle, replyMessage);
                        messageDetial.AddReply(replyProfile);
                    }
                }
                catch (Exception error) { Console.WriteLine(error); messageDetial = null; }
                finally
                {
                    eHandler(this, new DiscuzEventArgs(messageDetial));
                }
            });
        }


        public void GetMessages(int page, EventHandler<DiscuzEventArgs> eHandler)
        {
            AsyncGetJsonResponse(PostsUrl(37, page), (sender, e) =>
            {   //异步获取帖子列表的响应，开启了多线程，将会在里面读取所有的帖子
                List<MessageProfile> Messages = null;
                try
                {
                    var jsonObj = e.ResponseParseObject as JObject;
                    int threadcount = jsonObj["Variables"]["forum_threadlist"].Count();
                    Messages = new List<MessageProfile>();

                    //循环获得每个帖子的作者(名字与头像)、标题、内容
                    for (int i = 0; i < threadcount; i++)
                    {

                        int tid = (int)jsonObj["Variables"]["forum_threadlist"][i]["tid"];            //帖子的id
                        int uid = (int)jsonObj["Variables"]["forum_threadlist"][i]["authorid"];       //用户的id
                        var rss = SyncGetJsonResponse(DetailUrl(tid));                                //帖子内容的Json对象

                        //保存
                        string Author = rss["Variables"]["thread"]["author"].ToString();
                        string Title = rss["Variables"]["thread"]["subject"].ToString();
                        string Message = rss["Variables"]["postlist"][0]["message"].ToString();

                        //读取帖子中的所有照片，并保存
                        var PostPhotos = LoadPhotosFromMessage(Message);
                        foreach (var Photo in PostPhotos)
                        {
                            string fileName = Photo["src"].Substring(Photo["src"].LastIndexOf('/') + 1);
                            var localDir = String.Format("{0}/{1}/", LocalTmpPosts(), Title);
                            var localPath = localDir + fileName;
                            if (!Directory.Exists(localDir))
                            {   //这个是递归式创建
                                Directory.CreateDirectory(localDir);
                            }
                            SaveAndCloseStream(localPath, SyncGetPhotoResponse(Photo["src"]));
                            Photo["src"] = localPath;
                        }

                        //读取头像，并保存
                        var localHeadIcon = String.Format("{0}/{1}.gif", LocalTmpUsersIcon(), Author);
                        SaveAndCloseStream(localHeadIcon, SyncGetPhotoResponse(String.Format("http://{0}/discuz/uc_server/avatar.php?uid={1}", m_sServer, uid)));
                        Messages.Add(new MessageProfile(new UserInfo(localHeadIcon, Author), tid, Title, Message, PostPhotos));
                    }
                }
                catch (Exception error) { Console.WriteLine(error); Messages = null; }
                finally
                {
                    eHandler(this, new DiscuzEventArgs(Messages));
                }
            });
        }

        public async void AsyncGetJsonResponse(string UrlString, EventHandler<DiscuzEventArgs> eHandler)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = null;


            try
            {
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json;odata=verbose");
                response = await httpClient.GetAsync(UrlString);
            }
            catch (Exception e) { Console.WriteLine(e); }
            finally
            {
                if (response == null)
                {
                    eHandler(this, new DiscuzEventArgs(null));
                }
                else
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string json = response.Content.ReadAsStringAsync().Result;
                        JObject rss = JObject.Parse(json);
                        Console.WriteLine(rss);

                        eHandler(this, new DiscuzEventArgs(rss));
                    }
                    else
                    {
                        eHandler(this, new DiscuzEventArgs(null));
                    }
                }
            }
        }

        public JObject SyncGetJsonResponse(string UrlString)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = null;
            JObject json = null;

            try
            {
                response = httpClient.GetAsync(UrlString).Result;
            }
            catch (Exception e) { throw e; }
            finally
            {
                if (response.IsSuccessStatusCode)
                {
                    json = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                }
            }
            return json;
        }
        public Stream SyncGetPhotoResponse(string UrlString)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = null;
            Stream photoStream = null;

            try
            {
                response = httpClient.GetAsync(UrlString).Result;
            }
            catch (Exception e) { throw e; }
            finally
            {
                if (response.IsSuccessStatusCode)
                {
                    photoStream = response.Content.ReadAsStreamAsync().Result;
                }
            }
            return photoStream;
        }


        private void SaveAndCloseStream(string localPath, Stream stream)
        {
            SaveStream(localPath, stream);
            stream.Close();
        }

        private void SaveStream(string localPath, Stream stream)
        {
            byte[] buffer = new byte[stream.Length];
            FileStream file = new FileStream(localPath, FileMode.Create);
            stream.CopyTo(file);
            file.Close();
        }

        private List<Dictionary<string, string>> LoadPhotosFromMessage(string message)
        {
            List<Dictionary<string, string>> photos = new List<Dictionary<string, string>>();
            var imgHead = 0;
            var imgTail = 0;


            for (int startIndex = 0, photoNum=0; (imgHead = message.IndexOf("<img", startIndex))>0; startIndex = imgTail, photoNum++)
            {
                imgTail = message.IndexOf(">", imgHead);

                var imgInfoParts = message.Substring(imgHead, imgTail - imgHead + 1).Split(' ');
                photos.Add(new Dictionary<string, string>());

                for (int i = 1; i < imgInfoParts.Count()-1; i++)
                {
                    string[] kv = imgInfoParts[i].Split('=');
                    photos[photoNum].Add(kv[0], kv[1].Replace("\"", ""));
                }
            }
            return photos;
        }


        private string PostsUrl(int fid, int page)
        {
            return String.Format("http://{0}/discuz/api/mobile/index.php?version=4&module=forumdisplay&fid={1}&page={2}",
                                m_sServer, fid, page);
        }

        private string DetailUrl(int tid)
        {
            return String.Format("http://{0}/discuz/api/mobile/index.php?version=4&module=viewthread&tid={1}",
                                m_sServer, tid);
        }

        private string LocalTmpUsersIcon()
        {
            return String.Format("{0}/tmp/users_icon/", LocalRoot);
        }

        private string LocalTmpPosts()
        {
            return String.Format("{0}/tmp/posts", LocalRoot);
        }
    }

    class MessageDetail
    {
        public MessageProfile MainMessage { get; private set; }
        public List<MessageProfile> Replies { get; private set; }

        public MessageDetail(MessageProfile profile)
        {
            this.MainMessage = profile;
            this.Replies = new List<MessageProfile>();
        }

        public void AddReply(MessageProfile reply)
        {
            Replies.Add(reply);
        }
    }

    class MessageProfile
    {
        public UserInfo User { get; private set; }
        public string MessageTitle { get; private set; }
        public string MessageContent { get; private set; }
        public List<Dictionary<string, string>> MessagePhotos { get; private set; }
        public int Identify { get; private set; }

        private MessageProfile()
        {
            MessagePhotos = new List<Dictionary<string, string>>();
        }

        public MessageProfile(UserInfo User, int tid, string MessageTitle, string MessageContent) : this()
        {
            this.User = User;
            this.MessageTitle = MessageTitle;
            this.MessageContent = MessageContent;
            this.Identify = tid;
        }

        public MessageProfile(UserInfo User, int tid, string MessageTitle, string MessageContent, List<Dictionary<string, string>> Photos) : this(User, tid, MessageTitle, MessageContent)
        {
            MessagePhotos = Photos;
        }

    }

    class UserInfo
    {
        public int Identify { get; private set; }
        public string HeadIcon { get; private set; }
        public string Name { get; private set; }

        public UserInfo(string Name)
        {
            this.HeadIcon = "red-user.png";
            this.Name = Name;
        }

        public UserInfo(string HeadIcon, string Name)
        {
            this.HeadIcon = HeadIcon;
            this.Name = Name;
        }
    }
}
