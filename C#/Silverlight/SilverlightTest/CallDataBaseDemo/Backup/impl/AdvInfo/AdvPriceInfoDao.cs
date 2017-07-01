using System;
using System.Collections.Generic;
using System.Text;
using DAL.Dao.AdvInfo;

namespace DAL.impl.AdvInfo
{
    class AdvPriceInfoDao:IAdvPriceInfo
    {
        Util util = new Util();

        #region GetAdvPriceByAdvId 根据广告Id将广告的单价查询出来
        /// <summary>
        /// 根据广告Id将广告的单价查询出来
        /// </summary>
        /// <param name="AdvId">广告ID</param>
        /// <returns></returns>
        public double GetAdvPriceByAdvId(int AdvId) 
        {
            string cmdText = "select Adv_Unit_Price.Unit_Price from Adv_Unit_Price,Adv_Info where Adv_Info.Adv_Type_Id=Adv_Unit_Price.Adv_Type_Id and Adv_Info.Adv_Id='" + AdvId + "'";
            return util.GetdoubleExecuteScalar(cmdText);
        }
        #endregion
    }
}
