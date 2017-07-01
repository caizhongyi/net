using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Model
{
    //广告发布表
    public class AdvIssue
    {
        private DateTime _adv_startday;

        public DateTime Adv_startday
        {
            get { return _adv_startday; }
            set { _adv_startday = value; }
        }
        private DateTime _adv_endday;

        public DateTime Adv_endday
        {
            get { return _adv_endday; }
            set { _adv_endday = value; }
        }
    }
}
