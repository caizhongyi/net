using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DAL.Dao.City
{
    public interface ICityInfo
    {
        #region SelCityByProvinceID ������ѡ��ʡ�ݵ�ID����ʡ����������ȫ����ѯ����
        /// <summary>
        /// ������ѡ��ʡ�ݵ�ID����ʡ����������ȫ����ѯ����
        /// </summary>
        /// <param name="ProvinceID">��ѡ���ʡ�ݵ�ID</param>
        /// <returns></returns>
        DataSet SelCityByProvinceID(int ProvinceID);
        #endregion

        DataSet SelCityByProvinceIDView(int ProvinceID);

        #region SelCityByCityID ���ݳ���ID���ó��в�ѯ����
        /// <summary>
        /// ���ݳ���ID���ó��в�ѯ����
        /// </summary>
        /// <param name="CityId">����ID</param>
        /// <returns></returns>
        DataSet SelCityByCityID(int CityId);
        #endregion
    }
}
