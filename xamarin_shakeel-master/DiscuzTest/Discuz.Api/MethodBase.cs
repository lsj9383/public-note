using Discuz.Api.Attributes;
using Discuz.Api.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Discuz.Api {
    public abstract class MethodBase {

        /// <summary>
        /// 模块
        /// </summary>
        public abstract string Module {
            get;
        }

        public bool HasError {
            get;
            set;
        }

        public ErrorTypes? ErrorType {
            get;
            set;
        }

        /// <summary>
        /// 执行消息
        /// </summary>
        public string Message {
            get;
            set;
        }

        public async virtual Task<string> GetResult(ApiClient client) {
            try {
                var url = this.BuildUrl(client.GetUrl(this));
                HttpClient hc = new HttpClient();
                return await hc.GetStringAsync(url);
            } catch (HttpRequestException ex) {
                var bex = ex.GetBaseException();
                var o = new {
                    Message = new {
                        messageval = bex.HResult.ToString(),
                        messagestr = bex.Message
                    }
                };
                return JsonConvert.SerializeObject(o);
            } catch (WebException ex1) {
                var bex = ex1.GetBaseException();
                var o = new {
                    Message = new {
                        messageval = bex.HResult.ToString(),
                        messagestr = bex.Message
                    }
                };
                return JsonConvert.SerializeObject(o);
            }
        }

        protected void ParseMessage(string result) {
            var o = new {
                Message = new {
                    messagestr = "",
                    messageval = ""
                }
            };
            o = JsonConvert.DeserializeAnonymousType(result, o);
            if (o.Message != null && !string.IsNullOrWhiteSpace(o.Message.messageval)) {
                this.HasError = true;
                this.Message = o.Message.messagestr;
                this.ErrorType = o.Message.messageval.ParseErrorType();
            }
        }
    }

    public abstract class MethodBase<TResult> : MethodBase {

        internal async Task<TResult> Execute(ApiClient client) {
            var result = await this.GetResult(client);
            this.ParseMessage(result);
            return this.Execute(result);
        }

        protected virtual TResult Execute(string result) {
            return JsonConvert.DeserializeObject<TResult>(result);
        }
    }
}
