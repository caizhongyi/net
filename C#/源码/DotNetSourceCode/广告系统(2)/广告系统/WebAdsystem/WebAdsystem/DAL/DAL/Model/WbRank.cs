using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Model
{
    //Íø°ÉµÈ¼¶±í
    public class WbRank
    {
        private int _rk_id;

        public int Rk_id
        {
            get { return _rk_id; }
            set { _rk_id = value; }
        }
        private string _rk_name;

        public string Rk_name
        {
            get { return _rk_name; }
            set { _rk_name = value; }
        }
        private string _rk_remark;

        public string Rk_remark
        {
            get { return _rk_remark; }
            set { _rk_remark = value; }
        }
    }
}
