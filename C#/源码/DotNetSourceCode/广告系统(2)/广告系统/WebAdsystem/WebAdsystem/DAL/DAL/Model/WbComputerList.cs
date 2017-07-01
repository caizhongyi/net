using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Model
{
    //Íø°ÉµçÄÔ±í
    public class WbComputerList
    {
        private int _wb_c_id;

        public int Wb_c_id
        {
            get { return _wb_c_id; }
            set { _wb_c_id = value; }
        }
        private string _wb_c_ip;

        public string Wb_c_ip
        {
            get { return _wb_c_ip; }
            set { _wb_c_ip = value; }
        }
        private string _wb_c_time;

        public string Wb_c_time
        {
            get { return _wb_c_time; }
            set { _wb_c_time = value; }
        }
    }
}
