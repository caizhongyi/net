using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Dao.AdvInfo
{
    public interface IAdvPriceInfo
    {
        #region GetAdvPriceByAdvId 根据广告Id将广告的单价查询出来
        /// <summary>
        /// 根据广告Id将广告的单价查询出来
        /// </summary>
        /// <param name="AdvId">广告ID</param>
        /// <returns></returns>
        double GetAdvPriceByAdvId(int AdvId);
        #endregion
    }
}
