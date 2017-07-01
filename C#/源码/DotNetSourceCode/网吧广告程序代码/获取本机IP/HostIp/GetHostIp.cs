using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace HostIp
{
    public class GetHostIp
    {
        public string GetMyHostIp()
        {
            IPHostEntry iphe = new IPHostEntry();
            iphe = Dns.GetHostByName(Dns.GetHostName());
            string ip = iphe.AddressList[0].ToString();
            return ip;

        }
    }
}
