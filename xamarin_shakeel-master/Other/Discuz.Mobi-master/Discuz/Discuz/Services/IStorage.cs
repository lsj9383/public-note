using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discuz.Services {
    public interface IStorage {
        /// <summary>
        /// 获取大小
        /// </summary>
        /// <param name="path"></param>
        /// <returns>返回字节大小</returns>
        Task<long> GetSize(string path = "");

        /// <summary>
        /// 清理
        /// </summary>
        /// <param name="path"></param>
        Task Clear(string path = "");
    }
}
