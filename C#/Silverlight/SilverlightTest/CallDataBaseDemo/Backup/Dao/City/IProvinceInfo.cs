using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DAL.Dao.City
{
    public interface IProvinceInfo
    {
        #region SelProvince 将所有的省份都查询出来
        /// <summary>
        /// 将所有的省份都查询出来
        /// </summary>
        /// <returns></returns>
        DataSet SelProvince();
        #endregion

        DataSet SelProvinceView();

        #region SelProvinceByPId 根据省份Id将所有的省份都查询出来
        /// <summary>
        /// 根据省份Id将所有的省份都查询出来
        /// </summary>
        /// <param name="Pid">省份Id</param>
        /// <returns></returns>
        DataSet SelProvinceByPId(int Pid);
        #endregion
    }
}
