using System;
using System.Collections.Generic;
using System.Text;

namespace GetIpInfo
{
    class IPInfo
    {
        private static string ipadress;

        public string Ipadress
        {
            get { return IPInfo.ipadress; }
            set { IPInfo.ipadress = value; }
        }
    }
}
