using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DAL.Dao.Customer
{
    public interface IUserIdeaTypeInfo
    {
        #region SelUserIdeaTypeInfo 将所有的意见类型查询出来
        /// <summary>
        /// 将所有的意见类型查询出来
        /// </summary>
        /// <returns></returns>
        DataSet SelUserIdeaTypeInfo();
        #endregion
    }
}
