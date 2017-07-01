using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Data;

using System.Threading;


namespace SocketClass
{
    class Socketclass : WbClient.ISocketclass
    {
       
        Socket socket;
        /// <summary>
        /// 取得终端IP地址
        /// </summary>
        /// <returns></returns>
        public static IPAddress GetServerIP()
        {
            IPHostEntry ieh = Dns.GetHostByName(Dns.GetHostName());
            return ieh.AddressList[0];
        }
        /// <summary>
        /// 侦听
        /// </summary>
       public void BeginListen()
       {

           IPAddress ServerIp = GetServerIP();
           IPEndPoint iep = new IPEndPoint(ServerIp, 8000);
           socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
           socket.Bind(iep);
           //            do

           //            while(byteMessage!=null);
           while (true)
           {
               socket.Listen(1024);
               byte[] byteMessage = new byte[100];
               string msg = "";
               try
               {
                   Socket newSocket = socket.Accept();
                   newSocket.Receive(byteMessage);

                   string sTime = DateTime.Now.ToShortTimeString();
                   // string msg = sTime + ":" + "Message from:";
                   msg += Encoding.UTF32.GetString(byteMessage);
                   //this.listBox1.Items.Add(msg);
                   MessageBox.Show(msg);
               }
               catch (SocketException ex)
               {
                   throw ex;

               }
           }

       }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="ip">要发送的IP地址</param>
        /// <param name="port">端口</param>
        /// <param name="message">消息</param>
        public  void BeginSend(string ip,string port,string message)
        {

            IPAddress serverIp = IPAddress.Parse(ip);

            int serverPort = Convert.ToInt32(port);

            IPEndPoint iep = new IPEndPoint(serverIp, serverPort);

            byte[] byteMessage;

            //            do

            //            {

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            socket.Connect(iep);



            byteMessage = Encoding.ASCII.GetBytes(message);

            socket.Send(byteMessage);

            socket.Shutdown(SocketShutdown.Both);

            socket.Close();

            //            }

            //            while(byteMessage!=null);

        }
    

    }
}
