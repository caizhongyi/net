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

        #region ���û�ע��
        /// <summary>
        /// ע�����û�
        /// </summary>
        /// <param name="CustomerInfo">�û���д����ϸ��Ϣ</param>
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

        #region ���û�ע��
        /// <summary>
        /// ע�����û� IsCorporateMembers (0Ϊ���ǻ�Ա,1Ϊ��Ա)
        /// </summary>
        /// <param name="CustomerInfo">�û���д����ϸ��Ϣ</param>
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

        #region �ж��û�ID�Ƿ����||false->�����ڸ��û�||true->���ڸ��û�
        /// <summary>
        /// �ж��û�ID�Ƿ����
        /// </summary>
        /// <param name="UserID">�û�ID</param>
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
