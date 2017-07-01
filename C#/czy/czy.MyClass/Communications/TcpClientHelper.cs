﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace czy.MyClass.Communications
{
    public class TcpClientHelper
    {
        private static int portNum = 11000;
        private static string hostName = Dns.GetHostName().ToString();
        public void Send()
        { 
            try

            {

                Console.WriteLine("主机名字："+ Dns.GetHostName());

                Console.WriteLine("主机IP地址："+ Dns.GetHostAddresses(Dns.GetHostName())[0]);

                TcpClient client = new TcpClient(hostName, portNum);

                NetworkStream ns = client.GetStream();

                byte[] bytes = new byte[1024];

                int bytesRead = ns.Read(bytes, 0, bytes.Length);

                //将字节流解码为字符串

                Console.WriteLine(Encoding.ASCII.GetString(bytes, 0, bytesRead));

                client.Close();

            }

            catch (Exception e)

            {

                Console.WriteLine(e.ToString());

            }

     

        }

}
    
    public class TcpServerHelper
    {
        private const int portNum = 11000;
        public void Listen()
        {
            bool done = false;

            //TcpListener listener = new TcpListener(portNum); //根据VS2005 MSDN 此方法已经过时，不再使用

            // IPEndPoint类将网络标识为IP地址和端口号

            TcpListener listener = new TcpListener(new IPEndPoint(IPAddress.Any, portNum));

            listener.Start();

            while (!done)
            {

                Console.Write("Waiting for connection...");

                TcpClient client = listener.AcceptTcpClient();

                Console.WriteLine("Connection accepted.");

                NetworkStream ns = client.GetStream();

                byte[] byteTime = Encoding.ASCII.GetBytes(DateTime.Now.ToString());

                try
                {

                    ns.Write(byteTime, 0, byteTime.Length);

                    ns.Close();

                    client.Close();

                }

                catch (Exception e)
                {

                    Console.WriteLine(e.ToString());

                }

            }

            listener.Stop();

        }

    }
}

