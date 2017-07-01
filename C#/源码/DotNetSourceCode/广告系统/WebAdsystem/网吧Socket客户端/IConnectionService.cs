using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SocketClass
{
    public interface IConnectionService
    {
        /// <summary>
        /// 发送图片
        /// </summary>
        /// <returns></returns>
        Thread GetConnection();
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <returns></returns>
        Thread GetConnectionMessage();

    }
}
