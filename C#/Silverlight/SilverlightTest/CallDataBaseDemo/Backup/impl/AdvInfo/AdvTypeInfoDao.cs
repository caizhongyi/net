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

        #region SelAdvBigTypeInfo 将广告的大分类信息查询出来
        /// <summary>
        /// 将广告的大分类信息查询出来
        /// </summary>
        /// <returns></returns>
        public DataSet SelAdvBigTypeInfo()
        {
            string cmdText = "select * from T_BigTypeInfo";
            return util.GetDataSet(cmdText);
        }
        #endregion

        #region SelAdvSmallTypeInfoByBigId 将广告的小分类信息查询出来
        /// <summary>
        /// 将广告的小分类信息查询出来
        /// </summary>
        /// <param name="TBid">大分类广告ID</param>
        /// <returns></returns>
        public DataSet SelAdvSmallTypeInfoByBigId(int TBid)
        {
            string cmdText = "select * from T_SmalllTypeInfo where TBid='" + TBid + "'";
            return util.GetDataSet(cmdText);
        }
        #endregion
    }
}
