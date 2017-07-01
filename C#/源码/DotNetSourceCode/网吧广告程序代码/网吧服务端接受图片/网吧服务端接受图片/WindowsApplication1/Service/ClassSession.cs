using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Windows.Forms;
using System.IO;
using ��ȡ������IP;
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
            //��ȡԶ��������IP��ַ�Ͷ˿ں�
            IPEndPoint ep = (IPEndPoint)chat.RemoteEndPoint;
            //����
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

            //������ر�����IP
            ClassBroadCast broadCast = new ClassBroadCast();
            Thread tBroadCast = new Thread(new ThreadStart(broadCast.BroadCast));
            tBroadCast.IsBackground = true;
            tBroadCast.Start();
        }
        private void NewMethod(IPEndPoint ep, byte[] buff, ref int len, ref string msg)
        {
            string filename = msg.Substring(4);
            i++;
            FileStream writer = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write);
            while ((len = chat.Receive(buff)) != 0)
            {
                msg = Encoding.Default.GetString(buff, 0, len);
                if (msg == "END")
                    break;
                writer.Write(buff, 0, len);
            }
            writer.Write(buff, 0, len);
            writer.Close();
        }
    }
}
