using System;
using System.Collections.Generic;
using System.Threading;

namespace FCDiscuzManagerDemo
{
    class Program
    {
        static MessageDetail messageDetail = new MessageDetail();
        static void Main(string[] args)
        {
            Test1();
            Test2();

            for (int i = 0; true; i++)
            {
        //        Console.WriteLine(i);
                Thread.Sleep(500);
            }
        }

        static void Test2()
        {
            var manager = new FCDiscuzManager("C:/workspace/", "http://192.168.10.242/discuz", 37);
            manager.GetReplies(6, (sender, e) => 
            {
                if (!e.isSuccess)
                {
                    Console.WriteLine("failed");
                    return;
                }

                messageDetail.SetReplies(e.ResponseParseObject as List<MessageProfile>);
                while (messageDetail.MainMessage == null) { Thread.Sleep(10); } //确保设置完毕

                Console.WriteLine("Tid : " + messageDetail.MainMessage.Identify);
                Console.WriteLine("Title : " + messageDetail.MainMessage.MessageTitle);
                Console.WriteLine("Author : " + messageDetail.MainMessage.User.Name);
                Console.WriteLine("HeadIcon : " + messageDetail.MainMessage.User.HeadIcon);
                Console.WriteLine("Content : " + messageDetail.MainMessage.MessageContent);
                for (int i = 0; i < messageDetail.Replies.Count; i++)
                {
                    Console.WriteLine("==========================" + (i + 1) + "==========================");
                    Console.WriteLine("  from : " + messageDetail.Replies[i].User.Name);
                    Console.WriteLine("  Reply : " + messageDetail.Replies[i].MessageContent);
                }

            });
        }

        static void Test1()
        {
            var manager = new FCDiscuzManager("C:/workspace/", "http://192.168.10.242/discuz", 37);
            manager.GetMessages(1, (sender, e)=> 
            {
                if (!e.isSuccess)
                {
                    Console.WriteLine("failed");
                    return;
                }
                var Messages = e.ResponseParseObject as List<MessageProfile>;
                messageDetail.SetMessageMainBody(Messages[0]);

                foreach (var message in Messages)
                {
                    Console.WriteLine("==================================================");
                    Console.WriteLine("tid: " + message.Identify);
                    Console.WriteLine("Title: " + message.MessageTitle);
                    Console.WriteLine("Content: " + message.MessageContent);
                    Console.WriteLine("\n");
                    foreach (var photo in message.MessagePhotos)
                    {
                        Console.WriteLine("photo: " + photo["src"]);
                    }
                    Console.WriteLine("\t\tAuthor: " + message.User.Name);
                    Console.WriteLine("\t\tHead: " + message.User.HeadIcon);
                }
            });
            
        }
    }
}
