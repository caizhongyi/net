using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
namespace WbService
{
    public class ServiceInfo
    {
        private static int _port;

        public int Port
        {
            get { return _port; }
            set { _port =value; }
        }
    }
}
