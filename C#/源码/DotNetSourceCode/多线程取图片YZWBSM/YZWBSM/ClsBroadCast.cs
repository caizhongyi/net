using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace YZWBSM
{
    class ClsBroadCast
    {
        public void StartSendNewDay()
        {
            /*
            string mip = "192.168.0.3";
            string mday = "20081216";
            //只能用UDP协议发送广播，所以ProtocolType设置为UDP
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            //让其自动提供子网中的IP地址
            IPEndPoint iep = new IPEndPoint(IPAddress.Broadcast, 5918);
            //设置broadcast值为1，允许套接字发送广播信息
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
            //将发送内容转换为字节数组
            byte[] bytes = System.Text.Encoding.Unicode.GetBytes(":YZNewData:"+"ford:" + mip +":"+ mday);
            //向子网发送信息
            while (ClsConstant.bListenNewD)
            {
                socket.SendTo(bytes, iep);
                Thread.Sleep(2000);
             }
            */
             UdpClient udpClient = new UdpClient();
             IPEndPoint ep = new IPEndPoint(IPAddress.Broadcast , 5918);
            // IPEndPoint ep = new IPEndPoint(IPAddress.Parse("192.168.1.255"), 5918);
             //FrmSetUp setUp = new FrmSetUp();

             string computerInfo = ":YZNewData:" + Dns.GetHostName() + ":" +
             Dns.GetHostEntry(Dns.GetHostName()).AddressList[0] + ":" + ClsConstant.snewday;

             byte[] buff = Encoding.Default.GetBytes(computerInfo);
             while (ClsConstant.bListenNewD)
             {
                 udpClient.Send(buff, buff.Length, ep);
                 Thread.Sleep(2000);
             }
        }
    }
}
