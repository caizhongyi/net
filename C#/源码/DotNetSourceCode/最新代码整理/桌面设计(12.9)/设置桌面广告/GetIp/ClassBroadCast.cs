using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using WbSystem.GetIp;
using System.Data;
using System.IO;

namespace GetIpInfo
{
    class ClassBroadCast
    {
        FileDateTimeInfo fdti = new FileDateTimeInfo();
        private static bool IsFirst = true;
        public void BroadCast()
        {
            if (IsFirst)
            {
                IsFirst = false;
            }
            UdpClient udpClient = new UdpClient();
            IPEndPoint ep = new IPEndPoint(IPAddress.Broadcast,7999);
            udpClient.EnableBroadcast = true;
            string computerInfo = ":YZNewData" + ":" + Dns.GetHostName() + ":" + Dns.GetHostEntry(Dns.GetHostName()).AddressList[0] + ":" + fdti.Filedatetime;

            byte[] buff = Encoding.Default.GetBytes(computerInfo);
            while (true)
            {
                udpClient.Send(buff, buff.Length, ep);
                Thread.Sleep(2000);
            }
        }
    }
}
