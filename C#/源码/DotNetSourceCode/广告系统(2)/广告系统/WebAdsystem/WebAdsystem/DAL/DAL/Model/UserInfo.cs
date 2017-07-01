using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Model
{
    //用户信息表
    public class UserInfo
    {
        private string _user_id;

        public string User_id
        {
            get { return _user_id; }
            set { _user_id = value; }
        }
        private string _user_nickname;

        public string User_nickname
        {
            get { return _user_nickname; }
            set { _user_nickname = value; }
        }
        private string _user_name;

        public string User_name
        {
            get { return _user_name; }
            set { _user_name = value; }
        }
        private string _user_pwd;

        public string User_pwd
        {
            get { return _user_pwd; }
            set { _user_pwd = value; }
        }
        private string _user_sex;

        public string User_sex
        {
            get { return _user_sex; }
            set { _user_sex = value; }
        }
        private string _user_birthday;

        public string User_birthday
        {
            get { return _user_birthday; }
            set { _user_birthday = value; }
        }
        private DateTime _user_time;

        public DateTime User_time
        {
            get { return _user_time; }
            set { _user_time = value; }
        }
        private string _user_remark;

        public string User_remark
        {
            get { return _user_remark; }
            set { _user_remark = value; }
        }
        private string _user_postalcode;

        public string User_postalcode
        {
            get { return _user_postalcode; }
            set { _user_postalcode = value; }
        }
        private string _user_address;

        public string User_address
        {
            get { return _user_address; }
            set { _user_address = value; }
        }
        private string _user_tel1;

        public string User_tel1
        {
            get { return _user_tel1; }
            set { _user_tel1 = value; }
        }
        private string _user_tel2;

        public string User_tel2
        {
            get { return _user_tel2; }
            set { _user_tel2 = value; }
        }
        private string _user_fax;

        public string User_fax
        {
            get { return _user_fax; }
            set { _user_fax = value; }
        }
        private string _user_email1;

        public string User_email1
        {
            get { return _user_email1; }
            set { _user_email1 = value; }
        }
        private string _user_email2;

        public string User_email2
        {
            get { return _user_email2; }
            set { _user_email2 = value; }
        }
        private string _user_qq1;

        public string User_qq1
        {
            get { return _user_qq1; }
            set { _user_qq1 = value; }
        }
        private string _user_qq2;

        public string User_qq2
        {
            get { return _user_qq2; }
            set { _user_qq2 = value; }
        }
        private string _wb_connect;

        public string Wb_connect
        {
            get { return _wb_connect; }
            set { _wb_connect = value; }
        }

        //private string _user_TypeID;

        //public string User_TypeID
        //{
        //    get { return _user_TypeID; }
        //    set { _user_TypeID = value; }
        //}


    }
}
