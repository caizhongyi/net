using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DAL.Dao.AdvInfo
{
    public interface IAdvTypeInfo
    {
        #region SelAdvBigTypeInfo �����Ĵ������Ϣ��ѯ����
        /// <summary>
        /// �����Ĵ������Ϣ��ѯ����
        /// </summary>
        /// <returns></returns>
        DataSet SelAdvBigTypeInfo();
        #endregion

        #region SelAdvSmallTypeInfoByBigId ������С������Ϣ��ѯ����
        /// <summary>
        /// ������С������Ϣ��ѯ����
        /// </summary>
        /// <param name="TBid">�������ID</param>
        /// <returns></returns>
        DataSet SelAdvSmallTypeInfoByBigId(int TBid);
        #endregion
    }
}
