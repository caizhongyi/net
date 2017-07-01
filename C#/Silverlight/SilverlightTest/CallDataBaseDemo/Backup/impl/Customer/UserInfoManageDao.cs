using System;
using System.Collections.Generic;
using System.Text;
using DAL.Dao.Customer;
using Model;
using System.Data;

namespace DAL.impl.Customer
{
    class UserInfoManageDao:IUserInfoManage
    {
        Util util = new Util();

        #region UserPwdUpadata 用户修改密码
        /// <summary>
        /// 用户修改密码
        /// </summary>
        /// <param name="UserID">当前用户ID</param>
        /// <param name="NewPwd">当前用户的新密码</param>
        /// <param name="OldPwd">当前用户的旧密码</param>
        /// <returns></returns>
        public int UserPwdUpadata(string UserID,string NewPwd,string OldPwd) 
        {
            //用户密码修改
            string cmdText = "update T_CustomerInfo set CPwd='" + NewPwd + "' where CId='" + UserID + "' and CPwd='" + OldPwd + "'";
            return util.GetExecuteNonQuery(cmdText);
        }
        #endregion

        #region UserPwdUpadata 用户修改密码
        /// <summary>
        /// 用户修改密码
        /// </summary>
        /// <param name="UserID">当前用户ID</param>
        /// <param name="NewPwd">当前用户的新密码</param>
        /// <param name="OldPwd">当前用户的旧密码</param>
        /// <returns></returns>
        public int UserPwdUpadata(string UserID, string NewPwd)
        {
            //用户密码修改
            string cmdText = "update T_CustomerInfo set CPwd='" + NewPwd + "' where CId='" + UserID + "'";
            return util.GetExecuteNonQuery(cmdText);
        }
        #endregion

        #region UserInfoUpdata 用户基本信息修改
        /// <summary>
        /// 用户基本信息修改
        /// </summary>
        /// <param name="UserID">当前用户ID</param>
        /// <param name="CustomerInfo">当前用户的基本信息</param>
        /// <returns></returns>
        public int UserInfoUpdata(string UserID,T_CustomerInfo CustomerInfo) 
        {
            //用户基本信息修改
            string cmdText = "update T_CustomerInfo set CName='" + CustomerInfo.CName + "',CCompany='" + CustomerInfo.CCompany + "',CTel='" + CustomerInfo.CTel + "',CMobile='" + CustomerInfo.CMobile + "',CAddress='" + CustomerInfo.CAddress + "',CQQ='" + CustomerInfo.CQQ + "',[CE-mail]='" + CustomerInfo.CEmail + "',CMailAddress1='" + CustomerInfo.CMailAddress1 + "',ProvinceName='" + CustomerInfo.ProvinceName + "',CityName='" + CustomerInfo.CityName + "',CountryName='" + CustomerInfo.CountryName + "',AreaName='" + CustomerInfo.AreaName + "' where CId='" + UserID + "'";//需要添加数据
            return util.GetExecuteNonQuery(cmdText);
        }
        #endregion

        #region SelUserInfoByUserID 将当前用户的基本信息查询出来
        /// <summary>
        /// 将当前用户的基本信息查询出来
        /// </summary>
        /// <param name="UserID">当前用户ID</param>
        /// <returns></returns>
        public DataSet SelUserInfoByUserID(string UserID) 
        {
            string cmdText = "select * from T_CustomerInfo where CId='" + UserID + "'";
            return util.GetDataSet(cmdText);
        }
        #endregion

        #region 查询用户email
        /// <summary>
        /// 查询用户email
        /// </summary>
        /// <param name="UserID">当前用户ID</param>
        /// <returns></returns>
        public string  SelUserInfoCEMailByUserID(string UserID)
        {
            string cmdText = "select [CE-Mail] from T_CustomerInfo where CId='" + UserID + "'";
            return util.GetDataSet(cmdText).Tables[0].Rows [0].ItemArray [0].ToString ();
        }
        #endregion

        #region 获得全部用户地址


        public DataSet SelUserAddress(string UserId)
        {
            string sqltext = "select CmailAddress1,CmailAddress2,CmailAddress3 from T_CustomerInfo where T_CustomerInfo.CId='" + UserId + "'";
            return util.GetDataSet(sqltext);
        }

        #endregion

        #region 获得用户邮差地址


        public string GetUserEmailAddress1(string userid)
        {
            string sqltext = "select CAddress from T_CustomerInfo where T_CustomerInfo.CId='" + userid + "'";
            return util.GetStrExecuteScalar(sqltext);
        }

        public string GetUserEmailAddress2(string userid)
        {
            string sqltext = "select CmailAddress2 from T_CustomerInfo where T_CustomerInfo.CId='" + userid + "'";
            return util.GetStrExecuteScalar(sqltext);
        }

        public string GetUserEmailAddress3(string userid)
        {
            string sqltext = "select CmailAddress3 from T_CustomerInfo where T_CustomerInfo.CId='" + userid + "'";
            return util.GetStrExecuteScalar(sqltext);
        }

        #endregion
    }
}
