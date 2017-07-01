using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using WbService;
using System.Threading;
using GetIpInfo;
using UnCompress;
using System.Windows.Forms;

namespace WindowsApplication1.SendInfo
{
    class SendInfo
    {
        public static bool IsRun = false;
        IPInfo ipi = new IPInfo();
        private Socket sk;
        public void SendIpAdress()
        {
            //服务器端口号和IP地址 
            try
            {
            int port = 2000;
            string host = ipi.Ipadress;
            //获得局域网内在线IP
            //string host = "192.168.1.12";
            //获得机子IP地址
            string ipadress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString();
            //第一步：用指定的端口号和服务器的ip建立一个EndPoint对像；

            IPAddress ip = IPAddress.Parse(host);
            IPEndPoint ipep = new IPEndPoint(ip, port);

            //第二步：建立一个Socket对像；

           sk = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

    
                //第三步：用socket对像的Connect()方法以上面建立的EndPoint对像做为参数，向服务器发出连接请求；
                sk.Connect(ipep);


                //第四步：如果连接成功，就用socket对像的Send()方法向服务器发送信息；

                //发送一个IP给服务器
                string message = "YZWANTDATA:"+ipadress;
                byte[] bt = Encoding.ASCII.GetBytes(message);
                sk.Send(bt, bt.Length, 0);  
                //第六步：通信结束后一定记得关闭socket；
                sk.Close();
            }
            catch
            {
                //向服务器读取图片
            }
            finally
            {
                //启动服务器侦听
                IService isce = new WbService.Service();
                Thread td = isce.ServiceStart();
            }
        }
    }
}
