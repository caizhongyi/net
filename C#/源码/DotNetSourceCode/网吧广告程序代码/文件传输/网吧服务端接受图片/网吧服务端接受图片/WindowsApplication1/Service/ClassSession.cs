using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Windows.Forms;
using System.IO;
using WbClient;
using System.Threading;

namespace WbService
{
    class ClassSession
    {
        private Socket chat;
        private string pack;
        private static int i = 0;
        public ClassSession(Socket chat)
        {
            this.chat = chat;           
        }
        public void StartChat()
        {
            //获取远程主机的IP地址和端口号
            IPEndPoint ep = (IPEndPoint)chat.RemoteEndPoint;
            //设置
            byte[] buff = new byte[1024];
            int len;
            while ((len = chat.Receive(buff)) != 0)
            {
                string msg = Encoding.Default.GetString(buff, 0, len);
                pack = msg.Substring(0, 1);
                string cmd = msg.Substring(1, 3);
                if (cmd == "DAT")
                {
                            NewMethod(ep, buff, ref len, ref msg);
                }
            }


            chat.Close();
        }
        private void NewMethod(IPEndPoint ep, byte[] buff, ref int len, ref string msg)
        {
            string filename = @"D:\ScreenPricture\" + "save" + i.ToString();
            i++;
            string extName = msg.Substring(msg.LastIndexOf('.'));
            FileStream writer = new FileStream(filename + extName, FileMode.OpenOrCreate, FileAccess.Write);
            while ((len = chat.Receive(buff)) != 0)
            {
                msg = Encoding.Default.GetString(buff, 0, len);
                if (msg == "END")
                    break;
                writer.Write(buff, 0, len);
            }
            writer.Write(buff, 0, len);
            writer.Close();
            //回收
            IConnectionService iconn = new Client();
            Thread td = iconn.GetConnection(); 
        }
    }
}
