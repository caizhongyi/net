using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DAL.Dao.City
{
    public interface IAreaInfo
    {
        #region SelAreaByCountryID ������ѡ���ء��е�ID��������С��ȫ����ѯ����
        /// <summary>
        /// ������ѡ���ء��е�ID��������С��ȫ����ѯ����
        /// </summary>
        /// <param name="CountryID">ѡ��õ��ء��е�ID</param>
        /// <returns></returns>
        DataSet SelAreaByCountryID(int CountryID);
        #endregion

        #region SelAreaManCountByCountryID ������ѡ���ء��е�ID��������С�������û�ȫ����ѯ����
        /// <summary>
        /// ������ѡ���ء��е�ID��������С�������û�ȫ����ѯ����
        /// </summary>
        /// <param name="CountryID">ѡ��õ��ء��е�ID</param>
        /// <returns></returns>
        DataSet SelAreaManCountByCountryID(int CountryID);
        #endregion

        #region SelAreaInfoByAreaId ����С��Id��С����ѯ����
        /// <summary>
        /// ����С��Id��С����ѯ����
        /// </summary>
        /// <param name="AreaId">С��Id</param>
        /// <returns></returns>
        DataSet SelAreaInfoByAreaId(int AreaId);
        #endregion
    }
}
