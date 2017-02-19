using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Discuz {
    public static class HtmlClear {

        public static string Clear(this string text) {
            //var parser = new HtmlParser();
            //var a = parser.Parse(str);
            //return a.TextContent;

            text = text.Trim();
            if (string.IsNullOrEmpty(text))
                return string.Empty;
            text = text.Replace("&nbsp;", " ");
            text = Regex.Replace(text, "[/s]{2,}", " ");    //two or more spaces
            text = Regex.Replace(text, "(<[b|B][r|R]/*>)+|(<[p|P](.|/n)*?>)", " ");    //<br>
            text = Regex.Replace(text, "(/s*&[n|N][b|B][s|S][p|P];/s*)+", " ");    //&nbsp;
            text = Regex.Replace(text, "<(.|/n)*?>", string.Empty);    //any other tags
            text = Regex.Replace(text, "/<//?[^>]*>/g", string.Empty);    //any other tags
            text = Regex.Replace(text, "/[    | ]* /g", string.Empty);    //any other tags
            text = text.Replace("'", "''");
            text = Regex.Replace(text, "/ [/s| |    ]* /g", string.Empty);
            return text;
        }

    }
}
