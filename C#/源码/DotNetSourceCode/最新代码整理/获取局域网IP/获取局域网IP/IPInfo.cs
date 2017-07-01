using System;
using System.Collections.Generic;
using System.Text;

namespace DataKeepInfo
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
