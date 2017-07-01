using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DAL.Dao.City
{
    public interface ICountryInfo
    {
        #region SelCountryByCityID ��������ĳ��е�ID���������ء���ȫ����ѯ����
        /// <summary>
        /// ��������ĳ��е�ID���������ء���ȫ����ѯ����
        /// </summary>
        /// <param name="CityID">ѡ��õĳ��е�ID</param>
        /// <returns></returns>
        DataSet SelCountryByCityID(int CityID);
        #endregion
        DataSet SelCountryByCityIDView(int CityID);

        #region SelCountryByCountryID �����ء��е�ID���ء��в�ѯ����
        /// <summary>
        /// �����ء��е�ID���ء��в�ѯ����
        /// </summary>
        /// <param name="CountryID">�ء��е�ID</param>
        /// <returns></returns>
        DataSet SelCountryByCountryID(int CountryID);
        #endregion

        DataSet SelCountryPeopleByCountryId(int CId);
    }
}
