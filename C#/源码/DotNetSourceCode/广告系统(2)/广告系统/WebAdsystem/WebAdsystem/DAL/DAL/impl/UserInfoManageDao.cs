using System;
using System.Collections.Generic;
using System.Text;
using DAL.Dao;
using DAL.Model;
using System.Data;

namespace DAL.impl
{
    class UserInfoManageDao : IUserInfoManage
    {
        Util util = new Util();
        #region 实现登录

        public bool UserLogin(UserInfo userinfo)
        {
            string sqlText = "select User_Pwd from User_Info where User_Id='" + userinfo.User_id + "'";
            string PwdStr = util.GetStrExecuteScalar(sqlText);
            if (userinfo.User_pwd == PwdStr)
            {
                return true;
            }
            else
                return false;
        }

        #endregion

        #region 用户信息操作管理


        public bool AddUserInfo(UserInfo ui,int typeid)
        {
            string sqltext = "insert into User_Info values('"+ui.User_id+"','"+ui.User_nickname+"','"+ui.User_name+"','"+ui.User_pwd+"',"+typeid+",'"+ui.User_sex+"','"+ui.User_birthday+"','"+ui.User_time+"','"+ui.User_remark+"','"+ui.User_postalcode+"','"+ui.User_address+"','"+ui.User_tel1+"','"+ui.User_tel2+"','"+ui.User_fax+"','"+ui.User_email1+"','"+ui.User_email2+"','"+ui.User_qq1+"','"+ui.User_qq2+"','"+ui.Wb_connect+"')";
            int i = util.GetExecuteNonQuery(sqltext);
            return (i > 0) ? true : false;
        }

        public bool DelUserInfo(string id)
        {
            string sqltext = "delete from User_Info where User_Id='"+id+"'";
            int i = util.GetExecuteNonQuery(sqltext);
            return (i > 0) ? true : false;
        }

        public bool ModUserInfo(UserInfo ui,int typeid)
        {
            string sqltext = "update User_Info set User_NickName='"+ui.User_nickname+"',[User_Name]='"+ui.User_name+"',User_Pwd='"+ui.User_pwd+"',User_Type_Id="+typeid+",User_Sex='"+ui.User_sex+"',User_Birthday='"+ui.User_birthday+"',User_Time='"+ui.User_time+"',User_Remark='"+ui.User_remark+"',User_Postalcode='"+ui.User_postalcode+"',User_Address='"+ui.User_address+"',User_Tel1='"+ui.User_tel1+"',User_Tel2='"+ui.User_tel2+"',User_Fax='"+ui.User_fax+"',User_Email1='"+ui.User_email1+"',User_Email2='"+ui.User_email2+"',User_QQ1='"+ui.User_qq1+"',User_QQ2='"+ui.User_qq2+"',Wb_Connect='"+ui.Wb_connect+"' where [User_Id]='"+ui.User_id+"'";
            int i = util.GetExecuteNonQuery(sqltext);
            return (i > 0) ? true : false;
        }

        public DataSet SelUserInfo()
        {
            string sqltext = "select * from User_Info";
            DataSet ds = util.GetDataSet(sqltext);
            return ds;
        }


        public DataSet SelAdvUserInfo()
        {
            string sqltext = "select distinct User_name from user_info,adv_info where user_info.user_id=adv_info.user_id";
            DataSet ds = util.GetDataSet(sqltext);
            return ds;
        }

        #endregion
    }
}
