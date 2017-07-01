using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace DAL.Dao.Customer
{
    public interface IUserRegist
    {
        #region IsUserRegist ���û�ע��
        /// <summary>
        /// ע�����û�
        /// </summary>
        /// <param name="CustomerInfo">�û���д����ϸ��Ϣ</param>
        /// <returns></returns>
        bool IsUserRegist(T_CustomerInfo CustomerInfo);
        #endregion 

        #region IsUserRegist ���û�ע��
        /// <summary>
        /// ע�����û�
        /// </summary>
        /// <param name="CustomerInfo">�û���д����ϸ��Ϣ</param>
        /// <returns></returns>
        bool IsUserRegist1(T_CustomerInfo CustomerInfo);
        #endregion 

        #region IsUserRegist �ж��û�ID�Ƿ����||false->�����ڸ��û�||true->���ڸ��û�
        /// <summary>
        /// �ж��û�ID�Ƿ����
        /// </summary>
        /// <param name="UserID">�û�ID</param>
        /// <returns></returns>
        bool IsUserRegist(string UserID);
        #endregion
    }
}
