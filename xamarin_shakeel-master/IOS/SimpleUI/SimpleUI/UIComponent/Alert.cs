using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;

namespace SimpleUI.UIComponent
{
    class SimpleAlert
    {
        public static void Show(string Title, string Message, string Button)
        {   //标题 + 信息 + 单按钮 + 无事件
            var alert = new UIAlertView();
            alert.Title = Title;
            alert.Message = Message;
            alert.AddButton(Button);
            alert.Show();
        }

        public static void Show(string Title, string Message, string Button, EventHandler<UIButtonEventArgs> handler)
        {   // 标题 + 信息 + 单按钮 + 事件处理
            var alert = new UIAlertView();
            alert.Title = Title;
            alert.Message = Message;
            alert.AddButton(Button);
            alert.Dismissed += handler;
            alert.Show();
        }
        public static void Show(string Title, string Message, List<string> Buttons, EventHandler<UIButtonEventArgs> handler)
        {   // 标题 + 信息 + 多按钮 + 事件处理
            var alert = new UIAlertView();
            nint index = 0;

            alert.Title = Title;
            alert.Message = Message;
            foreach (string Button in Buttons)
            {
                alert.AddButton(Button);
            }
            alert.Dismissed += handler;
            alert.Show();
        }

        public static nint BlockShow(string Title, string Message, List<string> Buttons, EventHandler<UIButtonEventArgs> handler)
        {   // 标题 + 信息 + 多按钮 + 事件处理 + 按钮索引返回 + 阻塞
            var alert = new UIAlertView();
            int index = -1;

            alert.Title = Title;
            alert.Message = Message;
            foreach (string Button in Buttons)
            {
                alert.AddButton(Button);
            }
            alert.Dismissed += handler;
            alert.Dismissed += (sender, e) =>
            {
                index = (int)e.ButtonIndex;
            };
            alert.Show();
            return (nint)index;
        }
    }
}