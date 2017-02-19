using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discuz.Services {
    public interface IToast {

        /// <summary>
        /// 自动关闭
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="delay"></param>
        void ShowToast(string msg, int delay = 1000);

        /// <summary>
        /// 不自动关闭
        /// </summary>
        /// <param name="msg"></param>
        void Show(string msg);

        /// <summary>
        /// 手动关闭
        /// </summary>
        void Close();
    }
}
