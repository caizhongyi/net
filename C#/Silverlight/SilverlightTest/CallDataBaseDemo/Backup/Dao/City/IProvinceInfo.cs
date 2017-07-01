using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DAL.Dao.City
{
    public interface IProvinceInfo
    {
        #region SelProvince �����е�ʡ�ݶ���ѯ����
        /// <summary>
        /// �����е�ʡ�ݶ���ѯ����
        /// </summary>
        /// <returns></returns>
        DataSet SelProvince();
        #endregion

        DataSet SelProvinceView();

        #region SelProvinceByPId ����ʡ��Id�����е�ʡ�ݶ���ѯ����
        /// <summary>
        /// ����ʡ��Id�����е�ʡ�ݶ���ѯ����
        /// </summary>
        /// <param name="Pid">ʡ��Id</param>
        /// <returns></returns>
        DataSet SelProvinceByPId(int Pid);
        #endregion
    }
}
