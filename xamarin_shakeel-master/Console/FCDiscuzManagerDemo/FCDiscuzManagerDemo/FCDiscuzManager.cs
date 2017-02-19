using Discuz.Api;
using Discuz.Api.Entities;
using Discuz.Api.Methods;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FCDiscuzManagerDemo
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

    class FCDiscuzManager
    {
        int formID = 37;
        string baseUrl;
        string localRoot;

        public FCDiscuzManager(string localRoot, string baseUrl, int formID)
        {
            ApiClient.SetBaseUrl(baseUrl);
            this.formID = formID;
            this.baseUrl = baseUrl;
            this.localRoot = localRoot;

            if (!Directory.Exists(LocalTmpPosts())) { Directory.CreateDirectory(LocalTmpPosts()); }
            if (!Directory.Exists(LocalTmpUsersIcon())) { Directory.CreateDirectory(LocalTmpUsersIcon()); }
        }

        public async void GetReplies(int tid, EventHandler<DiscuzEventArgs> eHandler)
        {
            List<MessageProfile> replies = new List<MessageProfile>();
            try
            {
                var methodViewThread = new ViewThread() { ThreadID = tid };
                var posts = await ApiClient.Execute(methodViewThread);
                //浏览帖子内容
                foreach (ThreadPost p in posts)
                {
                    if (!p.IsFirst)
                    {   //回复
                        var replyPostID = p.ID;
                        var replyTitle = "reply";
                        var replyContent = p.Content;
                        var replyAuthor = p.Author;
                        var replyAuthorID = p.AuthorID;
                        var replyAuthorImgURL = p.AuthorImg;

                        var replyImgLocalPath = LocalTmpUsersIcon() + "/" + replyAuthor + ".gif";
                        SaveAndCloseStream(replyImgLocalPath, await GetPhotoResponse(GetUserImgUrl(replyAuthorID)));
                        replies.Add(new MessageProfile(new UserInfo(replyImgLocalPath, replyAuthor), replyPostID, replyTitle, replyContent));
                    }
                }

            }
            catch (Exception e) { Console.WriteLine(e); replies = null; }
            finally
            {
                eHandler(this, new DiscuzEventArgs(replies));
            }
        }

        public async void GetMessages(int page, EventHandler<DiscuzEventArgs> eHandler)
        {
            List<MessageProfile> Messages = new List<MessageProfile>();
            try
            {
                var methodForumDisplay = new ForumDisplay() { ForumID = this.formID };
                var threads = await ApiClient.Execute(methodForumDisplay);              //获得所有板块中的所有帖子
                foreach (ThreadSummary s in threads)
                {
                    var methodViewThread = new ViewThread() { ThreadID = s.ID };        //帖子id
                    var posts = await ApiClient.Execute(methodViewThread);              //获得当前帖子的内容

                    var PostID = s.ID;
                    var Title = s.Subject;
                    var Content = "";
                    var Author = s.Author;
                    var AuthorID = s.AuthorID;
                    var AuthorImgURL = s.AuthorImg;
                    List<Dictionary<string, string>> messagePhotos = new List<Dictionary<string, string>>();

                    //浏览帖子内容
                    foreach (ThreadPost p in posts)
                    {
                        if (p.IsFirst)
                        {
                            Content = p.Content;
                            if (p.Attachments != null)
                            {
                                foreach (KeyValuePair<string, Attachement> a in p.Attachments)
                                {
                                    var Attachments = new Dictionary<string, string>() { { "src", a.Value.Url } };
                                    messagePhotos.Add(Attachments);
                                }
                            }
                        }
                        else
                        {
                            break;  //不是首帖，直接退出
                        }
                    }

                    //图像保存
                    var AuthorImgLocalPath = LocalTmpUsersIcon() + "/" + Author + ".gif";
                    SaveAndCloseStream(AuthorImgLocalPath, await GetPhotoResponse(GetUserImgUrl(AuthorID)));
                    for (int i = 0; i < messagePhotos.Count; i++)
                    {
                        var photoURL = baseUrl + "/" + messagePhotos[i]["src"];
                        var fileName = photoURL.Substring(photoURL.LastIndexOf('/') + 1);
                        var localDir = String.Format("{0}/{1}/", LocalTmpPosts(), Title);
                        var photoLocalPath = localDir + fileName;
                        if (!Directory.Exists(localDir)) { Directory.CreateDirectory(localDir); }    //文件夹不存在，则创建文件夹
                        SaveAndCloseStream(photoLocalPath, GetPhotoResponse(photoURL).Result);
                        messagePhotos[i]["src"] = photoLocalPath;
                    }

                    Messages.Add(new MessageProfile(new UserInfo(AuthorImgLocalPath, s.Author), PostID, Title, Content, messagePhotos));
                }
            }
            catch (Exception e) { Console.WriteLine(e); Messages = null; }
            finally
            {
                eHandler(this, new DiscuzEventArgs(Messages));
            }
        }

        public async void GetContent(int tid, EventHandler<DiscuzEventArgs> eHandler)
        {
            MessageDetail messageDetial = new MessageDetail();
            try
            {
                var methodViewThread = new ViewThread() { ThreadID = tid };
                var posts = await ApiClient.Execute(methodViewThread);
                var PostID = posts.First().ID;
                var Title = posts.First().ThreadID.ToString();
                var Content = posts.First().Content;
                var Author = posts.First().Author;
                var AuthorID = posts.First().AuthorID;
                var AuthorImgURL = posts.First().AuthorImg;
                List<Dictionary<string, string>> messagePhotos = new List<Dictionary<string, string>>();

                var AuthorImgLocalPath = LocalTmpUsersIcon() + "/" + Author + ".gif";
                SaveAndCloseStream(AuthorImgLocalPath, await GetPhotoResponse(GetUserImgUrl(AuthorID)));


                //浏览帖子内容
                foreach (ThreadPost p in posts)
                {
                    //获取所有回复
                    if (!p.IsFirst)
                    {   
                        var replyPostID = posts.First().ID;
                        var replyTitle = "reply";
                        var replyContent = posts.First().Content;
                        var replyAuthor = posts.First().Author;
                        var replyAuthorID = posts.First().AuthorID;
                        var replyAuthorImgURL = posts.First().AuthorImg;

                        var replyImgLocalPath = LocalTmpUsersIcon() + "/" + replyAuthor + ".gif";
                        SaveAndCloseStream(replyImgLocalPath, await GetPhotoResponse(GetUserImgUrl(replyAuthorID)));
                        messageDetial.AddReply(new MessageProfile(new UserInfo(replyImgLocalPath, replyAuthor), replyPostID, replyTitle, replyContent));
                    }
                    //获取附件
                    if (p.Attachments != null)
                    {
                        foreach (KeyValuePair<string, Attachement> a in p.Attachments)
                        {
                            var Attachments = new Dictionary<string, string>() { { "src", a.Value.Url } };
                            messagePhotos.Add(Attachments);
                        }
                    }
                }

                //保存附件图像
                for (int i = 0; i < messagePhotos.Count; i++)
                {
                    var photoURL = baseUrl + "/" + messagePhotos[i]["src"];
                    var fileName = photoURL.Substring(photoURL.LastIndexOf('/') + 1);
                    var localDir = String.Format("{0}/{1}/", LocalTmpPosts(), Title);
                    var photoLocalPath = localDir + fileName;
                    if (!Directory.Exists(localDir)) { Directory.CreateDirectory(localDir); }    //文件夹不存在，则创建文件夹
                    SaveAndCloseStream(photoLocalPath, GetPhotoResponse(photoURL).Result);
                    messagePhotos[i]["src"] = photoLocalPath;
                }
                messageDetial.SetMessageMainBody(new MessageProfile(new UserInfo(AuthorImgLocalPath, Author), PostID, Title, Content, messagePhotos));

            }
            catch (Exception e) { Console.WriteLine(e); messageDetial = null; }
            finally
            {
                eHandler(this, new DiscuzEventArgs(messageDetial));
            }
        }

        public Task<Stream> GetPhotoResponse(string UrlString)
        {

            return Task.Run<Stream>(() =>
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
            });
        }

        private void SaveStream(string localPath, Stream stream)
        {
            byte[] buffer = new byte[stream.Length];
            FileStream file = new FileStream(localPath, FileMode.Create);
            stream.CopyTo(file);
            file.Close();
        }
        private void SaveAndCloseStream(string localPath, Stream stream)
        {
            SaveStream(localPath, stream);
            stream.Close();
        }

        private string GetUserImgUrl(int uid)
        {
            return String.Format("{0}/uc_server/avatar.php?uid={1}", baseUrl, uid);
        }

        private string LocalTmpUsersIcon()
        {
            return String.Format("{0}/tmp/users_icon/", localRoot);
        }

        private string LocalTmpPosts()
        {
            return String.Format("{0}/tmp/posts", localRoot);
        }
    }

    class MessageDetail
    {
        public MessageProfile MainMessage { get; private set; }
        public List<MessageProfile> Replies { get; private set; }

        public MessageDetail()
        {
            this.MainMessage = null;
            this.Replies = new List<MessageProfile>();
        }

        public void SetMessageMainBody(MessageProfile MainProfile)
        {
            MainMessage = MainProfile;
        }

        public void SetReplies(List<MessageProfile> Replies)
        {
            this.Replies = Replies;
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
