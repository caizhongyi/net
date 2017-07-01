using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace InternetClient
{
    public class Client:IClient
    {

        public Socket GetConnetion()
        {
            int port = 8001;
            string host = "192.168.1.10";
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(host),port);
            Socket sk = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
            sk.Connect(ipep);
            return sk;
        }
    }
}
