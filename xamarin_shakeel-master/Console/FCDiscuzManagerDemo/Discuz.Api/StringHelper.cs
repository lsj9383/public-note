using Discuz.Api.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using Discuz.Api.Attributes;

namespace Discuz.Api {
    public static class StringHelper {

        /// <summary>
        /// 從URL中取 Key / Value
        /// </summary>
        /// <param name="s"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static Dictionary<string, string> ParseString(this string s, bool ignoreCase) {
            //必須這樣,請不要修改
            if (string.IsNullOrEmpty(s)) {
                return new Dictionary<string, string>();
            }

            if (s.IndexOf('?') != -1) {
                s = s.Remove(0, s.IndexOf('?'));
            }

            Dictionary<string, string> kvs = new Dictionary<string, string>();
            Regex reg = new Regex(@"[\?&]?(?<key>[^=]+)=(?<value>[^\&]*)");
            MatchCollection ms = reg.Matches(s);
            string key;
            foreach (Match ma in ms) {
                key = ignoreCase ? ma.Groups["key"].Value.ToLower() : ma.Groups["key"].Value;
                if (kvs.ContainsKey(key)) {
                    kvs[key] += "," + ma.Groups["value"].Value;
                } else {
                    kvs[key] = ma.Groups["value"].Value;
                }
            }

            return kvs;
        }

        /// <summary>
        /// 設置 URL中的 key
        /// </summary>
        /// <param name="url"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public static string SetUrlKeyValue(this string url, string key, string value, Encoding encode = null) {
            if (url == null)
                url = "";
            if (String.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");
            if (value == null)
                value = "";
            if (null == encode)
                encode = Encoding.UTF8;
            //if (!string.IsNullOrEmpty(url.ParseString(key, true).Trim())) {
            if (url.ParseString(true).ContainsKey(key.ToLower())) {
                Regex reg = new Regex(@"([\?\&])(" + key + @"=)([^\&]*)(\&?)", RegexOptions.IgnoreCase);
                //　如果 value 前几个字符是数字，有BUG
                //return reg.Replace(url, "$1$2" + HttpUtility.UrlEncode(value, encode) + "$4");

                return reg.Replace(url, (ma) => {
                    if (ma.Success) {
                        return string.Format("{0}{1}{2}{3}", ma.Groups[1].Value, ma.Groups[2].Value, value, ma.Groups[4].Value);
                    }
                    return "";
                });

            } else {
                return string.Format("{0}{1}{2}={3}",
                    url,
                    (url.IndexOf('?') > -1 ? "&" : "?"),
                    key,
                    value);
                //return url + (url.IndexOf('?') > -1 ? "&" : "?") + key + "=" + value;
            }
        }


        /// <summary>
        /// 修正URL
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string FixUrl(this string url) {
            return url.FixUrl("");
        }

        /// <summary>
        /// 修正URL
        /// </summary>
        /// <param name="url"></param>
        /// <param name="defaultPrefix"></param>
        /// <returns></returns>
        public static string FixUrl(this string url, string defaultPrefix) {
            // 必須這樣,請不要修改
            if (url == null)
                url = "";

            if (defaultPrefix == null)
                defaultPrefix = "";
            string tmp = url.Trim();
            if (!Regex.Match(tmp, "^(http|https):").Success) {
                tmp = string.Format("{0}/{1}", defaultPrefix, tmp);
            }
            tmp = Regex.Replace(tmp, @"(?<!(http|https):)[\\/]+", "/").Trim();
            return tmp;
        }

        public static ErrorTypes ParseErrorType(this string str) {
            var fs = typeof(ErrorTypes).GetRuntimeFields();
            foreach (var f in fs) {
                var attr = f.GetCustomAttribute<ErrorTagAttribute>();
                if (attr != null && str.IndexOf(attr.Tag, StringComparison.OrdinalIgnoreCase) == 0) {
                    return (ErrorTypes)Enum.Parse(typeof(ErrorTypes), f.Name);
                }
            }

            return ErrorTypes.Unknow;
        }

    }
}
