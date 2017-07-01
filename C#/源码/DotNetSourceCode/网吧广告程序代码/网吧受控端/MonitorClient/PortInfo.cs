using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorClient
{
    public class PortInfo
    {
        public int GetPort()
        {
            int port =Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["port"]);
            return port;
        }
    }
}
