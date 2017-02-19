using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace DiscuzAPI
{
    class Program
    {

        static void TestJsonReader()
        {
            string json = @"{
              'CPU': 'Intel',
              'PSU': '500W',
              'Drives': [
                'DVD read/writer'
                /*(broken)*/,
                '500 gigabyte hard drive',
                '200 gigabype hard drive'
              ]
            }";
            JsonTextReader reader = new JsonTextReader(new StringReader(json));
            while (reader.Read())
            {
                if (reader.Value != null)
                {
                    Console.WriteLine("Token: {0}, Value: {1}", reader.TokenType, reader.Value);
                }
                else
                {
                    Console.WriteLine("Token: {0}", reader.TokenType);
                }
            }
        }

        static void TestJObject()
        {
            string json = @"{
    'Version': '4',
    'Charset': 'UTF-8',
    'Variables': {
        'cookiepre': 'pJnv_2132_',
        'auth': '6c5cWC5reQx3LD4QPVqH2OBg5DAnb6dGql9NLLBxpvEFMjCeFv/y/6S0IrLexkMh2zdtaX/49A2ZaCpoNCPA',
        'saltkey': 'oi332JJz',
        'member_uid': '2',
        'member_username': 'shakeel',
        'member_avatar': 'http://192.168.10.242/discuz/uc_server/avatar.php?uid=2&size=small',
        'groupid': '10',
        'formhash': '0c2538df',
        'ismoderator': '0',
        'readaccess': '10',
        'notice': {
            'newpush': '0',
            'newpm': '0',
            'newprompt': '1',
            'newmypost': '0'
        },
        'thread': {
            'tid': '6',
            'fid': '37',
            'posttableid': '0',
            'typeid': '0',
            'sortid': '0',
            'readperm': '0',
            'price': '0',
            'author': 'admin',
            'authorid': '1',
            'subject': 'some photos',
            'dateline': '1476942682',
            'lastpost': '2016-10-20 13:53',
            'lastposter': 'shakeel',
            'views': '3',
            'replies': '2',
            'displayorder': '0',
            'highlight': '0',
            'digest': '0',
            'rate': '0',
            'special': '0',
            'attachment': '2',
            'moderated': '0',
            'closed': '0',
            'stickreply': '0',
            'recommends': '0',
            'recommend_add': '0',
            'recommend_sub': '0',
            'heats': '2',
            'status': '32',
            'isgroup': '0',
            'favtimes': '0',
            'sharetimes': '0',
            'stamp': '-1',
            'icon': '-1',
            'pushedaid': '0',
            'cover': '0',
            'replycredit': '0',
            'relatebytag': '1476942684\t',
            'maxposition': '3',
            'bgcolor': '',
            'comments': '0',
            'hidden': '0',
            'threadtable': 'forum_thread',
            'threadtableid': '0',
            'posttable': 'forum_post',
            'allreplies': '2',
            'is_archived': '',
            'archiveid': '0',
            'subjectenc': 'some%20photos',
            'short_subject': 'some photos',
            'recommendlevel': '0',
            'heatlevel': '0',
            'relay': '0',
            'ordertype': '0',
            'recommend': '0'
        },
        'fid': '37',
        'postlist': [
            {
                'pid': '8',
                'tid': '6',
                'first': '1',
                'author': 'admin',
                'authorid': '1',
                'dateline': '3&nbsp;分钟前',
                'message': 'this is my photos.....<br />\n<br />\n<br />\n<div class=\'img\'><img src=\'http://192.168.10.242/discuz/data/attachment/forum/201610/20/135043ekvbdkp55kdfujam.jpg\' attach=\'8\' /></div><div class=\'img\'><img src=\'http://192.168.10.242/discuz/data/attachment/forum/201610/20/135042wz1ttzz3stpe9tth.jpg\' attach=\'8\' /></div><br />\r\n',
                'anonymous': '0',
                'attachment': '0',
                'status': '0',
                'replycredit': '0',
                'position': '1',
                'username': 'admin',
                'adminid': '1',
                'groupid': '1',
                'memberstatus': '0',
                'number': '1',
                'dbdateline': '1476942682',
                'attachments': [],
                'imagelist': [],
                'groupiconid': 'admin'
            },
            {
                'pid': '9',
                'tid': '6',
                'first': '0',
                'author': 'admin',
                'authorid': '1',
                'dateline': '3&nbsp;分钟前',
                'message': '我想听听你们的建议',
                'anonymous': '0',
                'attachment': '0',
                'status': '0',
                'replycredit': '0',
                'position': '2',
                'username': 'admin',
                'adminid': '1',
                'groupid': '1',
                'memberstatus': '0',
                'number': '2',
                'dbdateline': '1476942702',
                'groupiconid': 'admin'
            },
            {
                'pid': '10',
                'tid': '6',
                'first': '0',
                'author': 'shakeel',
                'authorid': '2',
                'dateline': '1&nbsp;分钟前',
                'message': '非常好，没意见',
                'anonymous': '0',
                'attachment': '0',
                'status': '0',
                'replycredit': '0',
                'position': '3',
                'username': 'shakeel',
                'adminid': '0',
                'groupid': '10',
                'memberstatus': '0',
                'number': '3',
                'dbdateline': '1476942784',
                'groupiconid': '1'
            }
        ],
        'allowpostcomment': null,
        'comments': [],
        'commentcount': [],
        'ppp': '5',
        'setting_rewriterule': null,
        'setting_rewritestatus': '',
        'forum_threadpay': '',
        'cache_custominfo_postno': [
            '<sup>#</sup>',
            '楼主',
            '沙发',
            '板凳',
            '地板'
        ],
        'forum': {
            'password': '0'
        }
    }
}";
            
            JObject jsonObj = JObject.Parse(json);

            int uid = (int)jsonObj["Variables"]["postlist"][0]["authorid"];
            string Author = jsonObj["Variables"]["postlist"][0]["author"].ToString();
            string Title = jsonObj["Variables"]["thread"]["subject"].ToString();
            string Message = jsonObj["Variables"]["postlist"][0]["message"].ToString();

            string myText = json;
            const string pattern = @".jpg\b";
            MatchCollection myMatches = Regex.Matches(myText, pattern, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);

            foreach (Match nextMatch in myMatches)
            {
                Console.WriteLine(nextMatch.Index);
            }

        }

        static async void TestDiscuz()
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = null;
            
            response = await httpClient.GetAsync("http://192.168.10.242/discuz/api/mobile/index.php?version=4&module=forumdisplay&fid=37&page=1");
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            }
        }

        static void TestDiscuzPhoto()
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = null;

            response = httpClient.GetAsync("http://192.168.10.242/discuz/uc_server/avatar.php?uid=3&size=small").Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStreamAsync().Result;
                string filePath = "C:\\workspace\\hello.gif";
                byte[] buffer = new byte[result.Length];

                if (!File.Exists(filePath))
                {
                    File.Create(filePath).Close();
                }

                FileStream file = new FileStream(filePath, FileMode.Open);       //创建后会默认会返回文件流。
                result.Read(buffer, 0, (int)result.Length);
                file.Write(buffer, 0, buffer.Length);

                file.Close();

                Console.WriteLine("done");
            }
        }

        static void TestDiscuzPhoto2()
        {
            string filePath = "C:\\workspace\\hello3.gif";
            
            DiscuzManager discuz = new DiscuzManager("192.168.10.24", "C:/workspace/");

            
            var stream = discuz.SyncGetPhotoResponse("http://192.168.10.242/discuz/uc_server/avatar.php?uid=3&size=small");
            
            byte[] buffer = new byte[stream.Length];
            FileStream file = new FileStream(filePath, FileMode.Create);       //创建后会默认会返回文件流。
            stream.CopyTo(file);
            file.Close();
            Console.WriteLine("OK");
        }



        static async void TestDiscuzJson()
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = null;

            httpClient.DefaultRequestHeaders.Add("Accept", "application/json;odata=verbose");
            response = await httpClient.GetAsync("http://192.168.10.242/discuz/api/mobile/index.php?version=4&module=forumdisplay&fid=37&page=1");
            if (response.IsSuccessStatusCode)
            {
                string jsonString = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(jsonString);
            }
        }

        static void TestDiscuzManagerPosts()
        {
            DiscuzManager discuz = new DiscuzManager("192.168.10.242", "C:/workspace/");
            discuz.GetMessages(1, (sender, e) =>
            {
                Console.WriteLine("finish : " + e.isSuccess.ToString());
                if (e.isSuccess)
                {
                    List<MessageProfile> Messages = e.ResponseParseObject as List<MessageProfile>;

                    for (int i = 0; i < Messages.Count; i++)
                    {
                        Console.WriteLine("================================");
                        Console.WriteLine("Id : " + Messages[i].Identify);
                        Console.WriteLine("Title : " + Messages[i].MessageTitle);
                        Console.WriteLine("Content : " + Messages[i].MessageContent);
                        for (int j = 0; j < Messages[i].MessagePhotos.Count; j++)
                        {
                            Console.WriteLine(j+" : "+Messages[i].MessagePhotos[j]["src"]);
                        }
                        Console.WriteLine();
                        Console.WriteLine("\t\t\tAuthor : " + Messages[i].User.Name);
                        Console.WriteLine("\t\t\tHeadIcon : " + Messages[i].User.HeadIcon);
                    }
                }
            });

            for (int i = 0; true; i++)
            {
                Console.WriteLine(i);
                Thread.Sleep(1000);
            }
        }

        static void TestDiscuzManagerMessageDetail()
        {
            DiscuzManager discuz = new DiscuzManager("192.168.10.242", "C:/workspace/");
            discuz.GetContent(6, (sender, e) =>
            {
                Console.WriteLine("finish : " + e.isSuccess.ToString());
                if (e.isSuccess)
                {
                    var Message = e.ResponseParseObject as MessageDetail;

                    Console.WriteLine("Tid : " + Message.MainMessage.Identify);
                    Console.WriteLine("Title : " + Message.MainMessage.MessageTitle);
                    Console.WriteLine("Author : " + Message.MainMessage.User.Name);
                    Console.WriteLine("HeadIcon : " + Message.MainMessage.User.HeadIcon);
                    Console.WriteLine("Content : " + Message.MainMessage.MessageContent);
                    for (int i = 0; i < Message.Replies.Count; i++)
                    {
                        Console.WriteLine("==========================" + (i+1) + "==========================");
                        Console.WriteLine("  from : " + Message.Replies[i].User.Name);
                        Console.WriteLine("  Reply : " + Message.Replies[i].MessageContent);
                    }
                }
            });
        }

        static void Regular1()
        {
            //            const string myText = "This comprehensive compendium provides a broad and thorough investigation of all aspects of programming with ASP.NET. Entirely revised and updated for the fourth release of .NET, this book will give you the information you need to master ASP.NET and build a dynamic successful, enterprise Web application";
            const string myText = "nans nnb nc";
            const string pattern = @"[.]n";
            MatchCollection myMatches = Regex.Matches(myText, pattern, RegexOptions.IgnoreCase|RegexOptions.ExplicitCapture);

            foreach (Match nextMatch in myMatches)
            {
                Console.WriteLine(nextMatch.Index);
            }
        }

        static void WriteMatches(string text, MatchCollection matches)
        {
            Console.WriteLine("Original text was : \n\n" + text + "\n");
            Console.WriteLine("No. of matches:" + matches.Count);

            foreach (Match nextMatch in matches)
            {
                int index = nextMatch.Index;
                string result = nextMatch.ToString();
                int charsBefore = (index < 5) ? index : 5;
                int fromEnd = text.Length - index - result.Length;
                int charsAfter = (fromEnd < 5) ? fromEnd : 5;
                int charsToDisplay = charsBefore + charsAfter + result.Length;
                Console.WriteLine("Index : {0}, \tstring: {1}, \t{2}",
                    index, result, text.Substring(index-charsBefore, charsToDisplay));

            }
        }

        static void Main(string[] args)
        {
            //TestDiscuzJson();
            //TestJObject();
            TestDiscuzManagerPosts();
            //TestDiscuzManagerMessageDetail();
            //TestDiscuzPhoto2();
            //Regular1();

            Console.WriteLine("Main Done!");
            Console.ReadLine();
        }
    }
}
