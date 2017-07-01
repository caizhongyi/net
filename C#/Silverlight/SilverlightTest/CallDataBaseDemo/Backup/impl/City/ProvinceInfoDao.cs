using System;
using System.Collections.Generic;
using System.Text;
using DAL.Dao.City;
using System.Data;

namespace DAL.impl.City
{
    //省份
    class ProvinceInfoDao:IProvinceInfo
    {
        Util util = new Util();

        #region SelProvince 将所有的省份都查询出来
        /// <summary>
        /// 将所有的省份都查询出来
        /// </summary>
        /// <returns></returns>
        public DataSet SelProvince() 
        {
            //将所有的省份都查询出来
            string cmdText = "select * from T_ProvinceInfo";
            return util.GetDataSet(cmdText);
        }
        #endregion

        public DataSet SelProvinceView()
        {
            string cmdText = "select PId,PName,ProvinceName,sum(case when ProvinceName is null then 0 else 1 end) as PeloleNum from T_ProvinceInfo left join  T_CustomerInfo  on  T_ProvinceInfo.PName=T_CustomerInfo.ProvinceName group by  PId,PName,ProvinceName ";
            return util.GetDataSet(cmdText);
        }

        #region SelProvinceByPId 根据省份Id将所有的省份都查询出来
        /// <summary>
        /// 根据省份Id将所有的省份都查询出来
        /// </summary>
        /// <param name="Pid">省份Id</param>
        /// <returns></returns>
        public DataSet SelProvinceByPId(int Pid)
        {
            //将所有的省份都查询出来
            string cmdText = "select * from T_ProvinceInfo where PId='" + Pid + "'";
            return util.GetDataSet(cmdText);
        }
        #endregion
    }
}
