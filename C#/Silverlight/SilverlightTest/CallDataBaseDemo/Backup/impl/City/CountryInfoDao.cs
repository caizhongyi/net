using System;
using System.Collections.Generic;
using System.Text;
using DAL.Dao.City;
using System.Data;

namespace DAL.impl.City
{
    //县、市
    class CountryInfoDao:ICountryInfo
    {
        Util util = new Util();

        #region SelCountryByCityID 根据所择的城市的ID将其下属县、市全部查询出来
        /// <summary>
        /// 根据所择的城市的ID将其下属县、市全部查询出来
        /// </summary>
        /// <param name="CityID">选择好的城市的ID</param>
        /// <returns></returns>
        public DataSet SelCountryByCityID(int CityID) 
        {
            string cmdText = "select * from T_CountyInfo where CityId='" + CityID + "'";
            return util.GetDataSet(cmdText);
        }
        #endregion
        public DataSet SelCountryByCityIDView(int CityID)
        {
            string cmdText = "select T_CountyInfo.CityId,T_CountyInfo.CName,T_CountyInfo.CId,CountryName,sum(case when CountryName is null then 0 else 1 end) as PeloleNum from T_CountyInfo left join  T_CustomerInfo  on   T_CountyInfo.CName=T_CustomerInfo.countryName where T_CountyInfo.CityId='" + CityID + "'  group by  CityId,T_CountyInfo.CName,T_CountyInfo.CId,CountryName";
            return util.GetDataSet(cmdText);
        }

        #region SelCountryByCountryID 根据县、市的ID将县、市查询出来
        /// <summary>
        /// 根据县、市的ID将县、市查询出来
        /// </summary>
        /// <param name="CountryID">县、市的ID</param>
        /// <returns></returns>
        public DataSet SelCountryByCountryID(int CountryID)
        {
            string cmdText = "select T_ProvinceInfo.PName+T_CityInfo.CName+T_CountyInfo.CName from T_ProvinceInfo,T_CityInfo,T_CountyInfo where T_CountyInfo.CId='" + CountryID + "' and T_CityInfo.CityId=T_CountyInfo.CityId and T_CityInfo.PId=T_ProvinceInfo.PId";
            return util.GetDataSet(cmdText);
        }
        #endregion

        public DataSet SelCountryPeopleByCountryId(int CId)
        {
            string cmdText = "select T_CountyInfo.CityId,T_CountyInfo.CName,T_CountyInfo.CId,CountryName,sum(case when CountryName is null then 0 else 1 end) as PeloleNum from T_CountyInfo left join  T_CustomerInfo  on   T_CountyInfo.CName=T_CustomerInfo.countryName where T_CountyInfo.CId='" + CId + "'  group by  CityId,T_CountyInfo.CName,T_CountyInfo.CId,CountryName";
            return util.GetDataSet(cmdText);
        }
    }
}
