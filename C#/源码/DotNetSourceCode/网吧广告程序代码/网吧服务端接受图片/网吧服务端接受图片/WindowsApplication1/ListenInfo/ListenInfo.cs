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
            //* ��һ������ָ���Ķ˿ںźͷ�������ip����һ��EndPoint����

            IPEndPoint ipep = new IPEndPoint(ServerIp, port);

            //* �ڶ���������һ��Socket����

            Socket sk = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //* ����������socket�����Bind()������EndPoint��
            byte[] bt = new byte[1024];
            sk.Bind(ipep);
            sk.Listen(1024);
            //* ���Ĳ�����socket�����Listen()������ʼ������ 
            while (true)
            {
                //* ���岽�����ܵ��ͻ��˵����ӣ���socket�����Accept()���������µ�socket�������ں�����Ŀͻ��˽���ͨ��;
                Socket kt = sk.Accept();
                ls.KeepSocket(kt,bt);
                Thread newThread = new Thread(new ThreadStart(ls.GetListenSessionInfo));
                //�����µ��߳�
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
