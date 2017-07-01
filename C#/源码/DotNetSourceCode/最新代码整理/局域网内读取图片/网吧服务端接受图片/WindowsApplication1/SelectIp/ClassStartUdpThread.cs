using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Collections;
using DataKeepInfo;
using System.Data;
using WindowsApplication1;

namespace ClassStartUdpThreadnamespace
{

    class ClassStartUdpThread
    {
        IPInfo ipi = new IPInfo();
        private static bool istrue=true;
        public void StartUdpThread()
        {
            UdpClient server = new UdpClient(7999);
            IPEndPoint ep = new IPEndPoint(IPAddress.Any, 0);
            while (istrue)
            {
                byte[] buff = server.Receive(ref ep);
                 string user = Encoding.Default.GetString(buff);
                string cmd = user.Substring(0, 6);
                string user1 = user.Substring(6);
                if (cmd == ":USER:")
                {
                    try
                    {
                        string[] s = user1.Split(':');
                        string name = s[0];
                        string ip = s[1];
                        string datetime = s[2];
                        if (ip== Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString())
                        {
                            continue;
                        }
                        else
                        {
                            ipi.Ipadress = ip;
                            Form1 f = new Form1();
                            f.Show();
                            istrue = false;
                        }
                    }
                    catch
                    {
                        return;
                    }
                }
            }
        }
    }
}
