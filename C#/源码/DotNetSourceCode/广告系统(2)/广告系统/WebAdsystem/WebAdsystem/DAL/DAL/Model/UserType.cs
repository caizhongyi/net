using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Model
{
    //用户身份表
    public class UserType
    {
        private int _user_type_id;

        public int User_type_id
        {
            get { return _user_type_id; }
            set { _user_type_id = value; }
        }
        private string _user_type_name;

        public string User_type_name
        {
            get { return _user_type_name; }
            set { _user_type_name = value; }
        }
        private string _user_type_remark;

        public string User_type_remark
        {
            get { return _user_type_remark; }
            set { _user_type_remark = value; }
        }
    }
}
