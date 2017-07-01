using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DAL.Dao.City
{
    public interface IAreaInfo
    {
        #region SelAreaByCountryID 根据所选的县、市的ID将其下属小区全部查询出来
        /// <summary>
        /// 根据所选的县、市的ID将其下属小区全部查询出来
        /// </summary>
        /// <param name="CountryID">选择好的县、市的ID</param>
        /// <returns></returns>
        DataSet SelAreaByCountryID(int CountryID);
        #endregion

        #region SelAreaManCountByCountryID 根据所选的县、市的ID将气下属小区和总用户全部查询出来
        /// <summary>
        /// 根据所选的县、市的ID将气下属小区和总用户全部查询出来
        /// </summary>
        /// <param name="CountryID">选择好的县、市的ID</param>
        /// <returns></returns>
        DataSet SelAreaManCountByCountryID(int CountryID);
        #endregion

        #region SelAreaInfoByAreaId 根据小区Id将小区查询出来
        /// <summary>
        /// 根据小区Id将小区查询出来
        /// </summary>
        /// <param name="AreaId">小区Id</param>
        /// <returns></returns>
        DataSet SelAreaInfoByAreaId(int AreaId);
        #endregion
    }
}
