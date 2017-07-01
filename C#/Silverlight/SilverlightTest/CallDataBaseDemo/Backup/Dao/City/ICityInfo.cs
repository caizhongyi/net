using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DAL.Dao.City
{
    public interface ICityInfo
    {
        #region SelCityByProvinceID 根据所选的省份的ID将该省的下属城市全部查询出来
        /// <summary>
        /// 根据所选的省份的ID将该省的下属城市全部查询出来
        /// </summary>
        /// <param name="ProvinceID">所选择的省份的ID</param>
        /// <returns></returns>
        DataSet SelCityByProvinceID(int ProvinceID);
        #endregion

        DataSet SelCityByProvinceIDView(int ProvinceID);

        #region SelCityByCityID 根据城市ID将该城市查询出来
        /// <summary>
        /// 根据城市ID将该城市查询出来
        /// </summary>
        /// <param name="CityId">城市ID</param>
        /// <returns></returns>
        DataSet SelCityByCityID(int CityId);
        #endregion
    }
}
