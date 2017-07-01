using System;
using System.Collections.Generic;
using System.Text;

namespace WbClient
{
    public class ClientInfo
    {
        //¶Ë¿ÚºÅ
        private  static int _prot;
        //Ö÷»úIp
        private static string _host;

        public int Prot
        {
            get { return _prot; }
            set { _prot =value; }
        }
       

        public string Host
        {
            get { return _host; }
            set { _host = value; }
        }

    }
}
