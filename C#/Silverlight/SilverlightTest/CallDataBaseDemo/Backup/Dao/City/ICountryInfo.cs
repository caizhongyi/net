using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DAL.Dao.City
{
    public interface ICountryInfo
    {
        #region SelCountryByCityID 根据所择的城市的ID将其下属县、市全部查询出来
        /// <summary>
        /// 根据所择的城市的ID将其下属县、市全部查询出来
        /// </summary>
        /// <param name="CityID">选择好的城市的ID</param>
        /// <returns></returns>
        DataSet SelCountryByCityID(int CityID);
        #endregion
        DataSet SelCountryByCityIDView(int CityID);

        #region SelCountryByCountryID 根据县、市的ID将县、市查询出来
        /// <summary>
        /// 根据县、市的ID将县、市查询出来
        /// </summary>
        /// <param name="CountryID">县、市的ID</param>
        /// <returns></returns>
        DataSet SelCountryByCountryID(int CountryID);
        #endregion

        DataSet SelCountryPeopleByCountryId(int CId);
    }
}
