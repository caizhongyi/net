﻿using System;
using System.Net.Sockets;
using System.Text;
using System.Net;
using System.Threading;

namespace czy.MyClass.Communications
{
    public class UDPClientHelper
    {
        public void Send()
        {
            try
            {

                UdpClient udpClient = new UdpClient(11000);



                //向服务器发送数据

                udpClient.Connect(Dns.GetHostName().ToString(), 12000);

                // Sends a message to the host to which you have connected.

                string sendStr = "我来自客户端:" + DateTime.Now.ToString();

                Byte[] sendBytes = Encoding.UTF8.GetBytes(sendStr);

                //Byte[] sendBytes = Encoding.ASCII.GetBytes(sendStr); 此处若用ASCII，不能正确处理中文

                udpClient.Send(sendBytes, sendBytes.Length);

                Console.WriteLine("This is the message client send: " + sendStr);





                //等待服务器的答复，收到后显示答复，并结束对话

                IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

                // 此处通过引用传值，获得客户端的IP地址及端口号

                Byte[] receiveBytes = udpClient.Receive(ref RemoteIpEndPoint);

                //此处获得服务器端的数据

                string returnData = Encoding.UTF8.GetString(receiveBytes);

                //Encoding.ASCII.GetString(receiveBytes); 此处若用ASCII，不能正确处理中文

                Console.WriteLine("This is the message come from server: " + returnData.ToString());

                udpClient.Close();

            }

            catch (Exception e)
            {

                Console.WriteLine(e.ToString());

            }

        

        }
    }
    public class UDPServerHelper
    {
        public void Listen()
        {
            try
            {

                UdpClient udpClient = new UdpClient(12000);

                string returnData = "client_end";

                do
                {

                    Console.WriteLine("服务器端接收数据：.............................");

                    IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

                    // 此处通过引用传值，获得客户端的IP地址及端口号

                    Byte[] receiveBytes = udpClient.Receive(ref RemoteIpEndPoint);

                    //此处获得客户端的数据

                    returnData = Encoding.UTF8.GetString(receiveBytes);

                    //Encoding.ASCII.GetString(receiveBytes); 此处若用ASCII，不能正确处理中文

                    Console.WriteLine("This is the message server received: " + returnData.ToString());



                    Thread.Sleep(3000);



                    Console.WriteLine("向客户端发送数据：.............................");

                    udpClient.Connect(Dns.GetHostName().ToString(), 11000);

                    // Sends a message to the host to which you have connected.

                    string sendStr = "我来自服务器端:" + DateTime.Now.ToString();

                    Byte[] sendBytes = Encoding.UTF8.GetBytes(sendStr);

                    //Byte[] sendBytes = Encoding.ASCII.GetBytes(sendStr); 此处若用ASCII，不能正确处理中文

                    udpClient.Send(sendBytes, sendBytes.Length);

                    Console.WriteLine("This is the message server send: " + sendStr);



                } while (returnData != "client_end");



            }

            catch (Exception e)
            {

                Console.WriteLine(e.ToString());

            }

        }

    }

}

