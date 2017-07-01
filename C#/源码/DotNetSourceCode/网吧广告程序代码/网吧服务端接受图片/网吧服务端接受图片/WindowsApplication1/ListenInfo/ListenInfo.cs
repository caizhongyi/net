using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using WbClient;
using System.Threading;

namespace WindowsApplication1.ListenInfo
{
    class ListenInfo
    {
        ListenSession ls = new ListenSession();
        public static IPAddress GetServerIP()
        {
            IPHostEntry ieh = Dns.GetHostByName(Dns.GetHostName());
            return ieh.AddressList[0];
        }
        public void BeginListen()
        {
            IPAddress ServerIp = GetServerIP();
            int port = 2000;
            //* 第一步：用指定的端口号和服务器的ip建立一个EndPoint对像；

            IPEndPoint ipep = new IPEndPoint(ServerIp, port);

            //* 第二步：建立一个Socket对像；

            Socket sk = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //* 第三步：用socket对像的Bind()方法绑定EndPoint；
            byte[] bt = new byte[1024];
            sk.Bind(ipep);
            sk.Listen(1024);
            //* 第四步：用socket对像的Listen()方法开始监听； 
            while (true)
            {
                //* 第五步：接受到客户端的连接，用socket对像的Accept()方法创建新的socket对像用于和请求的客户端进行通信;
                Socket kt = sk.Accept();
                ls.KeepSocket(kt,bt);
                Thread newThread = new Thread(new ThreadStart(ls.GetListenSessionInfo));
                //启动新的线程
                newThread.Start();
            }
        }
        public void ListenStart()
        {
            Thread td = new Thread(new ThreadStart(BeginListen));
            td.Start();
        }
    }
}
