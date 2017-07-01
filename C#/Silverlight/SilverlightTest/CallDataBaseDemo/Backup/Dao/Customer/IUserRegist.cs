using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace DAL.Dao.Customer
{
    public interface IUserRegist
    {
        #region IsUserRegist 新用户注册
        /// <summary>
        /// 注册新用户
        /// </summary>
        /// <param name="CustomerInfo">用户填写的详细信息</param>
        /// <returns></returns>
        bool IsUserRegist(T_CustomerInfo CustomerInfo);
        #endregion 

        #region IsUserRegist 新用户注册
        /// <summary>
        /// 注册新用户
        /// </summary>
        /// <param name="CustomerInfo">用户填写的详细信息</param>
        /// <returns></returns>
        bool IsUserRegist1(T_CustomerInfo CustomerInfo);
        #endregion 

        #region IsUserRegist 判断用户ID是否存在||false->不存在改用户||true->存在该用户
        /// <summary>
        /// 判断用户ID是否存在
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <returns></returns>
        bool IsUserRegist(string UserID);
        #endregion
    }
}
