using Discuz.Api.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;


namespace Discuz.Api {
    internal static class ParameterHelper {

        public static Dictionary<string, string> GetParams(this MethodBase method) {
            var dic = new Dictionary<string, string>();
            var props = method.GetType().GetRuntimeProperties().Where(p => p.GetCustomAttributes(typeof(ParamAttribute), true).Count() > 0);
            foreach (var p in props) {
                var pa = (ParamAttribute)p.GetCustomAttributes(typeof(ParamAttribute), true).First();
                var pms = pa.GetParams(method, p);
                if (pms != null) {
                    foreach (var pm in pms) {
                        //dic.Set(pm.Key, pm.Value);
                        if (dic.ContainsKey(pm.Key))
                            dic[pm.Key] = pm.Value;
                        else
                            dic.Add(pm.Key, pm.Value);
                    }
                }
            }

            return dic;
        }


        /// <summary>
        /// 将参数转换为 StringContent
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static StringContent GetStringContent(this MethodBase method) {
            var dic = method.GetParams();
            var str = string.Join("&", dic.Select(s => string.Format("{0}={1}", s.Key, s.Value)));
            return new StringContent(str);
        }


        /// <summary>
        /// 将参数编进URL
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string BuildUrl(this MethodBase method, string url) {
            var dic = method.GetParams();
            foreach (var kv in dic)
                url = url.SetUrlKeyValue(kv.Key, kv.Value);

            return url;
        }

        /// <summary>
        /// 将参数编进URI
        /// </summary>
        /// <param name="method"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static Uri BuildUrl(this MethodBase method, Uri uri) {
            return new Uri(method.BuildUrl(uri.AbsoluteUri));
        }
    }
}
