using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Model
{
    //地区一览表
    public class AreaList
    {
        private int _area_id;

        public int Area_id
        {
            get { return _area_id; }
            set { _area_id = value; }
        }
        private string _area_name;

        public string Area_name
        {
            get { return _area_name; }
            set { _area_name = value; }
        }
        private string _area_remark;

        public string Area_remark
        {
            get { return _area_remark; }
            set { _area_remark = value; }
        }
    }
}
