using System;
using System.Collections.Generic;
using System.Text;

namespace WbClient
{
    public class ClientInfo
    {
        //�˿ں�
        private  int _prot;
        //����Ip
        private string _host;

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
