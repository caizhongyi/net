using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Model
{
    //省份一览表
    public class ProvinceList
    {
        private int _province_id;

        public int Province_id
        {
            get { return _province_id; }
            set { _province_id = value; }
        }
        private string _province_name;

        public string Province_name
        {
            get { return _province_name; }
            set { _province_name = value; }
        }
        private string _remark;

        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
    }
}
