using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace DAL.Dao.Customer
{
    public interface IUserIdeaInfo
    {
        #region InsertIdearByUserID ���û�������������ӵ����ݿ���
        /// <summary>
        /// ���û�������������ӵ����ݿ���
        /// </summary>
        /// <param name="customerIdeaInfo">�û��������ϸ��Ϣ</param>
        /// <returns></returns>
        int InsertIdearByUserID(T_CustomerIdeaInfo customerIdeaInfo);
        #endregion
    }
}
