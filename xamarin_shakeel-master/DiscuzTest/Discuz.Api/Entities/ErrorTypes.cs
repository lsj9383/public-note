using Discuz.Api.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discuz.Api.Entities {
    public enum ErrorTypes {
        Unknow,

        /// <summary>
        /// 没有主题的访问权限
        /// </summary>
        [ErrorTag("thread_nopermission")]
        NoneThreadPermission,

        /// <summary>
        /// 没有版块的访问权限
        /// </summary>
        [ErrorTag("viewperm_none_nopermission")]
        NoneForumPermission,

        /// <summary>
        /// 主题不存在
        /// </summary>
        [ErrorTag("thread_nonexistence")]
        NotExists,

        /// <summary>
        /// 404
        /// </summary>
        [ErrorTag("404")]
        HttpNotFound,

        /// <summary>
        /// 500
        /// </summary>
        [ErrorTag("500")]
        ServerException,

        [ErrorTag("-2146233079")]
        DNSError
    }
}
