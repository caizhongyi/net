using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Dao.AdvInfo
{
    public interface IAdvPriceInfo
    {
        #region GetAdvPriceByAdvId ���ݹ��Id�����ĵ��۲�ѯ����
        /// <summary>
        /// ���ݹ��Id�����ĵ��۲�ѯ����
        /// </summary>
        /// <param name="AdvId">���ID</param>
        /// <returns></returns>
        double GetAdvPriceByAdvId(int AdvId);
        #endregion
    }
}
