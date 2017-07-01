using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
namespace FlyMessage
{
    class ClassSocket
    {
        private Socket socketSent;
        private IPEndPoint ipSent;
        public ClassSocket(Socket socketSent, IPEndPoint ipSent)
        {
            this.socketSent = socketSent;
            this.ipSent = ipSent;
        }
        public void SocketConnect()
        {
            socketSent.Connect(ipSent);
        }
    }
}
