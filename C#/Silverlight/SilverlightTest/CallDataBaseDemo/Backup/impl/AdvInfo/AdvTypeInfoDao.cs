using System;
using System.Collections.Generic;
using System.Text;
using DAL.Dao.AdvInfo;
using System.Data;

namespace DAL.impl.AdvInfo
{
    class AdvTypeInfoDao : IAdvTypeInfo
    {
        Util util = new Util();

        #region SelAdvBigTypeInfo �����Ĵ������Ϣ��ѯ����
        /// <summary>
        /// �����Ĵ������Ϣ��ѯ����
        /// </summary>
        /// <returns></returns>
        public DataSet SelAdvBigTypeInfo()
        {
            string cmdText = "select * from T_BigTypeInfo";
            return util.GetDataSet(cmdText);
        }
        #endregion

        #region SelAdvSmallTypeInfoByBigId ������С������Ϣ��ѯ����
        /// <summary>
        /// ������С������Ϣ��ѯ����
        /// </summary>
        /// <param name="TBid">�������ID</param>
        /// <returns></returns>
        public DataSet SelAdvSmallTypeInfoByBigId(int TBid)
        {
            string cmdText = "select * from T_SmalllTypeInfo where TBid='" + TBid + "'";
            return util.GetDataSet(cmdText);
        }
        #endregion
    }
}
