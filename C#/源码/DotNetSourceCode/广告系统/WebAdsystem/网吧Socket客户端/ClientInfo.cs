using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Net;


namespace SocketClass
{
    public class ClientInfo
    {
        //端口号
        private int _Mprot=Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["prot"]);
        
        public string _message;

        private string _MserverIP = System.Configuration.ConfigurationManager.AppSettings["ipaddress"].ToString();
        //找到ScreenPricture文件名
        private string _foldpath;

        private int _prot= Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["prot"]);

        private string _serverIP=System.Configuration.ConfigurationManager.AppSettings["ipaddress"].ToString();
        //主机Ip

        private string _host;

        private int _hostProt;

        public string ServerIP
        {
            get { return _serverIP; }
            set { _serverIP = value; }
        }


        
        

           public int Prot
        {
          get { return _prot; }
          set { _prot = value; }
        }

        public string Foldpath
        {
            get { return _foldpath; }
            set { _foldpath = value; }
        }


        public int HostProt
        {
            get { return _hostProt; }
            set { _hostProt= value; }
        }



        public string MserverIP
        {
            get { return _MserverIP; }
            set { _MserverIP = value; }
        }

        public string Message
        {
          get { return _message; }
          set { _message = value; }
        }


        public int MProt
        {
            get { return _Mprot; }
            set { _Mprot = value; }
        }
       

        public  string Host
        {
            get { return _host; }
            set { _host = value; }
        }

    }
}
