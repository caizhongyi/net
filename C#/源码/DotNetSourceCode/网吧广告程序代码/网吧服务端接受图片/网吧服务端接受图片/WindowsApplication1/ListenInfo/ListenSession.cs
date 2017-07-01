using System;
using System.Collections.Generic;
using System.Text;
using WbClient;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace WindowsApplication1.ListenInfo
{
    class ListenSession
    {
        private Socket socketSent;
        private byte[] bt;
        public void KeepSocket(Socket socketSent,byte[] bt)
        {
            this.socketSent = socketSent;
            this.bt = bt;
        }
        ClientInfo ci = new ClientInfo();
        public void GetListenSessionInfo()
        {
            string result = "";
            int bytes;
            bytes = socketSent.Receive(bt, bt.Length, 0);
            result += Encoding.ASCII.GetString(bt, 0, bytes);
            ci.Host = result;
            //Æô¶¯·¢ËÍÍ¼Æ¬
            IConnectionService iconn = new Client();
            Thread td = iconn.GetConnection(); 
        }
    }
}
