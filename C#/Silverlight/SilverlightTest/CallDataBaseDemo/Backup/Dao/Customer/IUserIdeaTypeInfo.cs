using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DAL.Dao.Customer
{
    public interface IUserIdeaTypeInfo
    {
        #region SelUserIdeaTypeInfo �����е�������Ͳ�ѯ����
        /// <summary>
        /// �����е�������Ͳ�ѯ����
        /// </summary>
        /// <returns></returns>
        DataSet SelUserIdeaTypeInfo();
        #endregion
    }
}
