using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DAL.Dao.AdvInfo
{
    public interface IAdvTypeInfo
    {
        #region SelAdvBigTypeInfo 将广告的大分类信息查询出来
        /// <summary>
        /// 将广告的大分类信息查询出来
        /// </summary>
        /// <returns></returns>
        DataSet SelAdvBigTypeInfo();
        #endregion

        #region SelAdvSmallTypeInfoByBigId 将广告的小分类信息查询出来
        /// <summary>
        /// 将广告的小分类信息查询出来
        /// </summary>
        /// <param name="TBid">大分类广告ID</param>
        /// <returns></returns>
        DataSet SelAdvSmallTypeInfoByBigId(int TBid);
        #endregion
    }
}
