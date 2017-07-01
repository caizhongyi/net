using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Model
{
    //广告信息表
    public class AdvInfo : DAL.Model.IAdvInfo
    {
        private int _adv_id;

        public int Adv_id
        {
            get { return _adv_id; }
            set { _adv_id = value; }
        }
        private string _adv_name;

        public string Adv_name
        {
            get { return _adv_name; }
            set { _adv_name = value; }
        }
        private string _adv_content;

        public string Adv_content
        {
            get { return _adv_content; }
            set { _adv_content = value; }
        }
        private string _adv_url;

        public string Adv_url
        {
            get { return _adv_url; }
            set { _adv_url = value; }
        }
        private int _adv_operation;

        public int Adv_operation
        {
            get { return _adv_operation; }
            set { _adv_operation = value; }
        }
        private int _adv_clicknumber;

        public int Adv_clicknumber
        {
            get { return _adv_clicknumber; }
            set { _adv_clicknumber = value; }
        }
        private DateTime _adv_time;

        public DateTime Adv_time
        {
            get { return _adv_time; }
            set { _adv_time = value; }
        }
        private int _adv_discount;

        public int Adv_discount
        {
            get { return _adv_discount; }
            set { _adv_discount = value; }
        }
        private string _adv_pay_state;

        public string Adv_pay_state
        {
            get { return _adv_pay_state; }
            set { _adv_pay_state = value; }
        }
        private int _adv_master;

        public int Adv_master
        {
            get { return _adv_master; }
            set { _adv_master = value; }
        }


        private int _user_id;

        public int User_id
        {
            get { return _user_id; }
            set { _user_id = value; }
        }

      
    }

}
