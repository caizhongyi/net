using System;
using System.Collections.Generic;
using System.Text;
using DAL.Dao.City;
using System.Data;

namespace DAL.impl.City
{
    //ʡ��
    class ProvinceInfoDao:IProvinceInfo
    {
        Util util = new Util();

        #region SelProvince �����е�ʡ�ݶ���ѯ����
        /// <summary>
        /// �����е�ʡ�ݶ���ѯ����
        /// </summary>
        /// <returns></returns>
        public DataSet SelProvince() 
        {
            //�����е�ʡ�ݶ���ѯ����
            string cmdText = "select * from T_ProvinceInfo";
            return util.GetDataSet(cmdText);
        }
        #endregion

        public DataSet SelProvinceView()
        {
            string cmdText = "select PId,PName,ProvinceName,sum(case when ProvinceName is null then 0 else 1 end) as PeloleNum from T_ProvinceInfo left join  T_CustomerInfo  on  T_ProvinceInfo.PName=T_CustomerInfo.ProvinceName group by  PId,PName,ProvinceName ";
            return util.GetDataSet(cmdText);
        }

        #region SelProvinceByPId ����ʡ��Id�����е�ʡ�ݶ���ѯ����
        /// <summary>
        /// ����ʡ��Id�����е�ʡ�ݶ���ѯ����
        /// </summary>
        /// <param name="Pid">ʡ��Id</param>
        /// <returns></returns>
        public DataSet SelProvinceByPId(int Pid)
        {
            //�����е�ʡ�ݶ���ѯ����
            string cmdText = "select * from T_ProvinceInfo where PId='" + Pid + "'";
            return util.GetDataSet(cmdText);
        }
        #endregion
    }
}
