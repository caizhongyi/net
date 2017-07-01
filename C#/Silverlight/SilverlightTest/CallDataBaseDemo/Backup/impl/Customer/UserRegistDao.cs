using System;
using System.Collections.Generic;
using System.Text;
using Model;
using DAL.Dao.Customer;

namespace DAL.impl.Customer
{
    class UserRegistDao : IUserRegist
    {
        Util util = new Util();

        #region 新用户注册
        /// <summary>
        /// 注册新用户
        /// </summary>
        /// <param name="CustomerInfo">用户填写的详细信息</param>
        /// <returns></returns>
        public bool IsUserRegist(T_CustomerInfo CustomerInfo) 
        {
            string cmdText = "insert into T_CustomerInfo(CId,CPwd,CAreaId,CAddress,[CE-mail],ProvinceName,CityName,CountryName,AreaName,CReTime,CRemark) values('" + CustomerInfo.CId + "','" + CustomerInfo.CPwd + "','" + CustomerInfo.CAreaId + "','" + CustomerInfo.CAddress + "','" + CustomerInfo.CEmail + "','" + CustomerInfo.ProvinceName + "','" + CustomerInfo.CityName + "','" + CustomerInfo.CountryName + "','" + CustomerInfo.AreaName + "','" + CustomerInfo.CReTime + "','" + CustomerInfo.CRemark + "')";
            int i = util.GetExecuteNonQuery(cmdText);
            switch (i)
            {
                case 0:
                    return false;
                default:
                    return true;
            }
        }
        #endregion

        #region 新用户注册
        /// <summary>
        /// 注册新用户 IsCorporateMembers (0为不是会员,1为会员)
        /// </summary>
        /// <param name="CustomerInfo">用户填写的详细信息</param>
        /// <returns></returns>
        public bool IsUserRegist1(T_CustomerInfo CustomerInfo)
        {
            string cmdText = "insert into T_CustomerInfo(CId,CPwd,CAreaId,CAddress,[CE-mail],ProvinceName,CityName,CountryName,AreaName,RecommendPeople,CReTime,no_codeID,IsCorporateMembers) values('" + CustomerInfo.CId + "','" + CustomerInfo.CPwd + "','" + CustomerInfo.CAreaId + "','" + CustomerInfo.CAddress + "','" + CustomerInfo.CEmail + "','" + CustomerInfo.ProvinceName + "','" + CustomerInfo.CityName + "','" + CustomerInfo.CountryName + "','" + CustomerInfo.AreaName + "','" + CustomerInfo.RecommendPeople + "','" + CustomerInfo.CReTime + "','" + CustomerInfo.No_codeID + "','" + CustomerInfo.IsCorporateMembers + "')";
            int i = util.GetExecuteNonQuery(cmdText);
            switch (i)
            {
                case 0: 
                    return false;
                default:
                    return true;
            }
        }
        #endregion

        #region 判断用户ID是否存在||false->不存在改用户||true->存在该用户
        /// <summary>
        /// 判断用户ID是否存在
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <returns></returns>
        public bool IsUserRegist(string UserID) 
        {
            string cmdText = "select CId from T_CustomerInfo where CId='" + UserID + "'";
            string i = util.GetStrExecuteScalar(cmdText);
            switch (i)
            {
                case "":
                    return false;
                default:
                    return true;
            }
        }
        #endregion
    }
}
