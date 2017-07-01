using System;
using System.Collections.Generic;
using System.Text;
using DAL.Dao.AdvInfo;

namespace DAL.impl.AdvInfo
{
    class AdvPriceInfoDao:IAdvPriceInfo
    {
        Util util = new Util();

        #region GetAdvPriceByAdvId ���ݹ��Id�����ĵ��۲�ѯ����
        /// <summary>
        /// ���ݹ��Id�����ĵ��۲�ѯ����
        /// </summary>
        /// <param name="AdvId">���ID</param>
        /// <returns></returns>
        public double GetAdvPriceByAdvId(int AdvId) 
        {
            string cmdText = "select Adv_Unit_Price.Unit_Price from Adv_Unit_Price,Adv_Info where Adv_Info.Adv_Type_Id=Adv_Unit_Price.Adv_Type_Id and Adv_Info.Adv_Id='" + AdvId + "'";
            return util.GetdoubleExecuteScalar(cmdText);
        }
        #endregion
    }
}
