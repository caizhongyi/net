using System;
using System.Collections.Generic;
using System.Text;
using DAL.Dao;
using DAL.Model;
using System.Data;

namespace DAL.impl
{
    class UserTypeManageDao : IUserTypeManage
    {
        Util util = new Util();
        #region 用户身份管理

        public bool AddUserType(UserType ut)
        {
            string sqltext = "insert into User_Type values('"+ut.User_type_name+"','"+ut.User_type_remark+"')";
            int i = util.GetExecuteNonQuery(sqltext);
            return (i > 0) ? true : false;
        }

        public bool DelUserType(int id)
        {
            string sqltext = "delete from User_Type where User_Type_Id="+id+"";
            int i = util.GetExecuteNonQuery(sqltext);
            return (i > 0) ? true : false;
        }

        public bool ModUserType(UserType ut)
        {
            string sqltext = "update User_Type set User_Type_Name='"+ut.User_type_name+"',User_Type_Remark='"+ut.User_type_remark+"' where User_Type_Id="+ut.User_type_id+"";
            int i = util.GetExecuteNonQuery(sqltext);
            return (i > 0) ? true : false;
        }

        public DataSet SelUserType()
        {
            string sqltext = "select * from User_Type";
            DataSet ds = util.GetDataSet(sqltext);
            return ds;
        }

        #endregion
    }
}
