using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace YZWBSM
{
    class ClsInfoListen
    {

        public void StartListenNewDay()
        {
            UdpClient server = new UdpClient(5918);
            IPEndPoint ep = new IPEndPoint(IPAddress.Any, 0);
            while (ClsConstant.bListenNewD)
            {
                byte[] buff = server.Receive(ref ep);
                string user = Encoding.Default.GetString(buff);
                string cmd = user.Substring(0, 11);
                string user1 = user.Substring(11);
                if (cmd == ":YZNewData:")
                {
                    try
                    {
                        string[] s = user1.Split(':');
                        //string lvsiComputerName= s[0];
                        string lvsiIP = s[1];
                        string lvsiInfo = s[2];
                        ClsConstant.Addmydr(lvsiIP, lvsiInfo);
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
