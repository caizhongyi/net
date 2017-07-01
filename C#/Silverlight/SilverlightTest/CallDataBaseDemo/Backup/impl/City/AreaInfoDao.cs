using System;
using System.Collections.Generic;
using System.Text;
using DAL.Dao.City;
using System.Data;

namespace DAL.impl.City
{
    //С��
    class AreaInfoDao:IAreaInfo
    {
        Util util = new Util();

        #region SelAreaByCountryID ������ѡ���ء��е�ID��������С��ȫ����ѯ����
        /// <summary>
        /// ������ѡ���ء��е�ID��������С��ȫ����ѯ����
        /// </summary>
        /// <param name="CountryID">ѡ��õ��ء��е�ID</param>
        /// <returns></returns>
        public DataSet SelAreaByCountryID(int CountryID) 
        {
            string cmdText = "select * from T_AreaInfo where CId='" + CountryID + "'";
            return util.GetDataSet(cmdText);
        }
        #endregion

        #region SelAreaManCountByCountryID ������ѡ���ء��е�ID��������С�������û�ȫ����ѯ����
        /// <summary>
        /// ������ѡ���ء��е�ID��������С�������û�ȫ����ѯ����
        /// </summary>
        /// <param name="CountryID">ѡ��õ��ء��е�ID</param>
        /// <returns></returns>
        public DataSet SelAreaManCountByCountryID(int CountryID) 
        {
            string cmdText = "Select T_AreaInfo.AId,T_AreaInfo.AName,count(*) as CountMan from  T_CustomerInfo,T_AreaInfo where T_AreaInfo.AId=T_CustomerInfo.CAreaId and T_AreaInfo.CId='" + CountryID + "' group by T_AreaInfo.AId,T_AreaInfo.AName,T_CustomerInfo.CAreaId";
            return util.GetDataSet(cmdText);
        }
        #endregion 

        #region SelAreaInfoByAreaId ����С��Id��С����ѯ����
        /// <summary>
        /// ����С��Id��С����ѯ����
        /// </summary>
        /// <param name="AreaId">С��Id</param>
        /// <returns></returns>
        public DataSet SelAreaInfoByAreaId(int AreaId) 
        {
            string cmdText = "select T_ProvinceInfo.PName+T_CityInfo.CName+T_CountyInfo.CName+T_AreaInfo.AName from T_ProvinceInfo,T_CityInfo,T_CountyInfo,T_AreaInfo where T_AreaInfo.AId='" + AreaId + "' and T_AreaInfo.CId=T_CountyInfo.CId and T_CityInfo.CityId=T_CountyInfo.CityId and T_CityInfo.PId=T_ProvinceInfo.PId'";
            return util.GetDataSet(cmdText);
        }
        #endregion
    }
}
