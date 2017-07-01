using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Model
{
    //广告们价格表
    public class AdvUnitprice
    {
        private int _unitprice;

        public int Unitprice
        {
            get { return _unitprice; }
            set { _unitprice = value; }
        }
        private string _up_remark;

        public string Up_remark
        {
            get { return _up_remark; }
            set { _up_remark = value; }
        }
        private DateTime _moditf_time;

        public DateTime Moditf_time
        {
            get { return _moditf_time; }
            set { _moditf_time = value; }
        }
    }
}
