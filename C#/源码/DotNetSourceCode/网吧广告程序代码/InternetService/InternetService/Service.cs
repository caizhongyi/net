using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace InternetService
{
    public class Service : IService
    {
        public void GetConnection()
        {
            int port = 8001;
            string host = "192.168.1.10";
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(host),port);
            Socket sk = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
            sk.Bind(ipep);
            while (true)
            {
                sk.Listen(10);
            }
        }
    }
}
