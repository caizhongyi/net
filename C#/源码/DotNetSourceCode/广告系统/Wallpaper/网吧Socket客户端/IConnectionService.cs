using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SocketClass
{
    public interface IConnectionService
    {
        /// <summary>
        /// ����ͼƬ
        /// </summary>
        /// <returns></returns>
        Thread GetConnection();
        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <returns></returns>
        Thread GetConnectionMessage();

    }
}
