using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace 获取局域网IP
{
    class ClassBroadCast
    {
        public void BroadCast()
        {
            UdpClient udpClient = new UdpClient();
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("192.168.1.255"),7999);

            string computerInfo = ":USER" + ":" + Dns.GetHostName() + ":" + Dns.GetHostEntry(Dns.GetHostName()).AddressList[0];

            byte[] buff = Encoding.Default.GetBytes(computerInfo);
            while (true)
            {
                udpClient.Send(buff, buff.Length, ep);
                Thread.Sleep(2000);
            }
        }
    }
}
