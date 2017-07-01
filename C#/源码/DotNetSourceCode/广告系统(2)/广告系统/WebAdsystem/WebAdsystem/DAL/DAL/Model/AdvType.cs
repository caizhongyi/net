using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Model
{
    //广告们类型表
    public class AdvType
    {
        private int _adv_type_id;

        public int Adv_type_id
        {
            get { return _adv_type_id; }
            set { _adv_type_id = value; }
        }
        private string _adv_type_name;

        public string Adv_type_name
        {
            get { return _adv_type_name; }
            set { _adv_type_name = value; }
        }
        private string _adv_type_remark;

        public string Adv_type_remark
        {
            get { return _adv_type_remark; }
            set { _adv_type_remark = value; }
        }
    }
}
