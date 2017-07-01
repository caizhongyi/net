using System;
using System.Collections.Generic;
using System.Text;
using DAL.Dao.City;
using System.Data;

namespace DAL.impl.City
{
    //城市
    class CityInfoDao : ICityInfo
    {
        Util util = new Util();

        #region SelCityByProvinceID 根据所选的省份的ID将该省的下属城市全部查询出来
        /// <summary>
        /// 根据所选的省份的ID将该省的下属城市全部查询出来
        /// </summary>
        /// <param name="ProvinceID">所选择的省份的ID</param>
        /// <returns></returns>
        public DataSet SelCityByProvinceID(int ProvinceID) 
        {
            string cmdText = "select * from T_CityInfo where PId='" + ProvinceID + "'";
            return util.GetDataSet(cmdText);
        }
        #endregion

        public DataSet SelCityByProvinceIDView(int ProvinceID)
        {
            string cmdText = "select CityID,PID,T_CityInfo.Cname,Count(*) as PeloleNum from T_CityInfo left join  T_CustomerInfo on T_CityInfo.CName=T_CustomerInfo.CityName where T_CityInfo.PId='" + ProvinceID + "' group by  CityID,PID,T_CityInfo.Cname";
            return util.GetDataSet(cmdText);
        }

        #region SelCityByCityID 根据城市ID将该城市查询出来
        /// <summary>
        /// 根据城市ID将该城市查询出来
        /// </summary>
        /// <param name="CityId">城市ID</param>
        /// <returns></returns>
        public DataSet SelCityByCityID(int CityId)
        {
            string cmdText = "select T_ProvinceInfo.PName+T_CityInfo.CName from T_ProvinceInfo,T_CityInfo where T_CityInfo.CityId='" + CityId + "' and T_CityInfo.PId=T_ProvinceInfo.PId";
            return util.GetDataSet(cmdText);
        }
        #endregion
    }
}
