using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RowGrow
{
    class Message
    {
        public string Title { get; private set; }
        public string Content { get; private set; }
        public List<string> ImagesName { get; private set; }
        public nfloat MessageHeight = 0.0f;


        public Message(string title, List<string> images)
        {
            this.Title = title;
            this.ImagesName = images;
        }

        public Message SetContent(string content)
        {
            this.Content = content;
            return this;
        }
    }
}