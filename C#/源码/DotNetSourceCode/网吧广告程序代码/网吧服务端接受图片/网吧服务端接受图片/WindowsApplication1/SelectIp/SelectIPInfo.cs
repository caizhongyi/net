using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsApplication1.SelectIp
{
    public class SelectIPInfo
    {
        private static string _host;

        public string Host
        {
            get { return SelectIPInfo._host; }
            set { SelectIPInfo._host = value; }
        }
    }
}
