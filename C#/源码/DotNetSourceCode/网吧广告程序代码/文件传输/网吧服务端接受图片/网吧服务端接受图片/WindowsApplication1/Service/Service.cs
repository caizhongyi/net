using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Windows.Forms;

namespace WbService
{
    public class Service:IService
    {
        //定义作为服务器端接受信息套接字
        public Socket socketReceive = null;

        //定义接受信息的IP地址和端口号
        public IPEndPoint ipReceive = null;

        //定义发送信息的IP地址和端口号
        public IPEndPoint ipSent = null;

        //定义接受信息的套接字
        public Socket chat = null;

        ServiceInfo si = new ServiceInfo();

        private void ReceiveNews()
        {
            try
            {
                //初始化接受套接字：寻址方案，以字符流方式和Tcp通信
                socketReceive = new Socket(AddressFamily.InterNetwork,
                 SocketType.Stream,
                 ProtocolType.Tcp);

                //获取本机IP地址并设置接受信息的端口
                ipReceive = new IPEndPoint(
                  Dns.GetHostEntry(Dns.GetHostName()).AddressList[0],si.Port);

                //将本机IP地址和接受端口绑定到接受套接字
                socketReceive.Bind(ipReceive);

                //监听端口，并设置监听缓存大小为1024byte
                socketReceive.Listen(1024);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

            //定义接受信息时缓冲区
            byte[] buff = new byte[1024];

            //连续接受客户端发送过来的信息

            while (true)
            {
                //定义一个chat套接字用来接受信息
                Socket chat = socketReceive.Accept();


                //定义一个处理信息的对象
                ClassSession cs = new ClassSession(chat);

                //定义一个新的线程用来接受其他主机发送的信息
                Thread newThread = new Thread(new ThreadStart(cs.StartChat));

                //启动新的线程
                newThread.Start();
            }

        }
        public Thread ServiceStart()
        {
            Thread td = new Thread(new ThreadStart(ReceiveNews));
            td.Start();
            return td;  
        }
    }
}
