using System;
using System.Collections.Generic;
using System.Text;
using Model;
using System.Data;

namespace DAL.Dao.Customer
{
    public interface IUserInfoManage
    {
        #region UserPwdUpadata �û��޸�����
        /// <summary>
        /// �û��޸�����
        /// </summary>
        /// <param name="UserID">��ǰ�û�ID</param>
        /// <param name="NewPwd">��ǰ�û���������</param>
        /// <param name="OldPwd">��ǰ�û��ľ�����</param>
        /// <returns></returns>
        int UserPwdUpadata(string UserID, string NewPwd, string OldPwd);
        int UserPwdUpadata(string UserID, string NewPwd);
        #endregion

        #region UserInfoUpdata �û�������Ϣ�޸�
        /// <summary>
        /// �û�������Ϣ�޸�
        /// </summary>
        /// <param name="UserID">��ǰ�û�ID</param>
        /// <param name="CustomerInfo">��ǰ�û��Ļ�����Ϣ</param>
        /// <returns></returns>
        int UserInfoUpdata(string UserID, T_CustomerInfo CustomerInfo);
        #endregion

        #region SelUserInfoByUserID ����ǰ�û��Ļ�����Ϣ��ѯ����
        /// <summary>
        /// ����ǰ�û��Ļ�����Ϣ��ѯ����
        /// </summary>
        /// <param name="UserID">��ǰ�û�ID</param>
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
