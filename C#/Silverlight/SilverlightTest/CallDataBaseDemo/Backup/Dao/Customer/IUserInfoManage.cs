using System;
using System.Collections.Generic;
using System.Text;
using Model;
using System.Data;

namespace DAL.Dao.Customer
{
    public interface IUserInfoManage
    {
        #region UserPwdUpadata 用户修改密码
        /// <summary>
        /// 用户修改密码
        /// </summary>
        /// <param name="UserID">当前用户ID</param>
        /// <param name="NewPwd">当前用户的新密码</param>
        /// <param name="OldPwd">当前用户的旧密码</param>
        /// <returns></returns>
        int UserPwdUpadata(string UserID, string NewPwd, string OldPwd);
        int UserPwdUpadata(string UserID, string NewPwd);
        #endregion

        #region UserInfoUpdata 用户基本信息修改
        /// <summary>
        /// 用户基本信息修改
        /// </summary>
        /// <param name="UserID">当前用户ID</param>
        /// <param name="CustomerInfo">当前用户的基本信息</param>
        /// <returns></returns>
        int UserInfoUpdata(string UserID, T_CustomerInfo CustomerInfo);
        #endregion

        #region SelUserInfoByUserID 将当前用户的基本信息查询出来
        /// <summary>
        /// 将当前用户的基本信息查询出来
        /// </summary>
        /// <param name="UserID">当前用户ID</param>
        /// <returns></returns>
        DataSet SelUserInfoByUserID(string UserID);
        #endregion

        DataSet SelUserAddress(string UserId);

        string GetUserEmailAddress1(string userid);
        string GetUserEmailAddress2(string userid);
        string GetUserEmailAddress3(string userid);
        string SelUserInfoCEMailByUserID(string UserID);
    }
}
