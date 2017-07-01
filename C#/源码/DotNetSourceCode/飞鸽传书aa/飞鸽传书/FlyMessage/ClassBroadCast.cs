using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
namespace FlyMessage
{
    class ClassBroadCast
    {
        
        public  void BroadCast()
        {
               UdpClient udpClient = new UdpClient();
               IPEndPoint ep = new IPEndPoint(IPAddress.Parse("192.168.1.255"), 7999);
                FrmSetUp setUp = new FrmSetUp();
           
                string computerInfo = ":USER" + ":" + setUp.txtUserName.Text + ":" + Dns.GetHostName() + ":" +
                Dns.GetHostEntry(Dns.GetHostName()).AddressList[0] + ":" + setUp.txtWorkGroup.Text;

                byte[] buff = Encoding.Default.GetBytes(computerInfo);
                while (true)
                {
                    udpClient.Send(buff, buff.Length, ep);
                    Thread.Sleep(2000);
                }
              
            
        }
    }
}
