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

        #region UserPwdUpadata �û��޸�����
        /// <summary>
        /// �û��޸�����
        /// </summary>
        /// <param name="UserID">��ǰ�û�ID</param>
        /// <param name="NewPwd">��ǰ�û���������</param>
        /// <param name="OldPwd">��ǰ�û��ľ�����</param>
        /// <returns></returns>
        public int UserPwdUpadata(string UserID,string NewPwd,string OldPwd) 
        {
            //�û������޸�
            string cmdText = "update T_CustomerInfo set CPwd='" + NewPwd + "' where CId='" + UserID + "' and CPwd='" + OldPwd + "'";
            return util.GetExecuteNonQuery(cmdText);
        }
        #endregion

        #region UserPwdUpadata �û��޸�����
        /// <summary>
        /// �û��޸�����
        /// </summary>
        /// <param name="UserID">��ǰ�û�ID</param>
        /// <param name="NewPwd">��ǰ�û���������</param>
        /// <param name="OldPwd">��ǰ�û��ľ�����</param>
        /// <returns></returns>
        public int UserPwdUpadata(string UserID, string NewPwd)
        {
            //�û������޸�
            string cmdText = "update T_CustomerInfo set CPwd='" + NewPwd + "' where CId='" + UserID + "'";
            return util.GetExecuteNonQuery(cmdText);
        }
        #endregion

        #region UserInfoUpdata �û�������Ϣ�޸�
        /// <summary>
        /// �û�������Ϣ�޸�
        /// </summary>
        /// <param name="UserID">��ǰ�û�ID</param>
        /// <param name="CustomerInfo">��ǰ�û��Ļ�����Ϣ</param>
        /// <returns></returns>
        public int UserInfoUpdata(string UserID,T_CustomerInfo CustomerInfo) 
        {
            //�û�������Ϣ�޸�
            string cmdText = "update T_CustomerInfo set CName='" + CustomerInfo.CName + "',CCompany='" + CustomerInfo.CCompany + "',CTel='" + CustomerInfo.CTel + "',CMobile='" + CustomerInfo.CMobile + "',CAddress='" + CustomerInfo.CAddress + "',CQQ='" + CustomerInfo.CQQ + "',[CE-mail]='" + CustomerInfo.CEmail + "',CMailAddress1='" + CustomerInfo.CMailAddress1 + "',ProvinceName='" + CustomerInfo.ProvinceName + "',CityName='" + CustomerInfo.CityName + "',CountryName='" + CustomerInfo.CountryName + "',AreaName='" + CustomerInfo.AreaName + "' where CId='" + UserID + "'";//��Ҫ�������
            return util.GetExecuteNonQuery(cmdText);
        }
        #endregion

        #region SelUserInfoByUserID ����ǰ�û��Ļ�����Ϣ��ѯ����
        /// <summary>
        /// ����ǰ�û��Ļ�����Ϣ��ѯ����
        /// </summary>
        /// <param name="UserID">��ǰ�û�ID</param>
        /// <returns></returns>
        public DataSet SelUserInfoByUserID(string UserID) 
        {
            string cmdText = "select * from T_CustomerInfo where CId='" + UserID + "'";
            return util.GetDataSet(cmdText);
        }
        #endregion

        #region ��ѯ�û�email
        /// <summary>
        /// ��ѯ�û�email
        /// </summary>
        /// <param name="UserID">��ǰ�û�ID</param>
        /// <returns></returns>
        public string  SelUserInfoCEMailByUserID(string UserID)
        {
            string cmdText = "select [CE-Mail] from T_CustomerInfo where CId='" + UserID + "'";
            return util.GetDataSet(cmdText).Tables[0].Rows [0].ItemArray [0].ToString ();
        }
        #endregion

        #region ���ȫ���û���ַ


        public DataSet SelUserAddress(string UserId)
        {
            string sqltext = "select CmailAddress1,CmailAddress2,CmailAddress3 from T_CustomerInfo where T_CustomerInfo.CId='" + UserId + "'";
            return util.GetDataSet(sqltext);
        }

        #endregion

        #region ����û��ʲ��ַ


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
