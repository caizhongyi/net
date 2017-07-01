using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace DAL.Dao.Customer
{
    public interface IUserIdeaInfo
    {
        #region InsertIdearByUserID 将用户提出来的意见添加到数据库中
        /// <summary>
        /// 将用户提出来的意见添加到数据库中
        /// </summary>
        /// <param name="customerIdeaInfo">用户意见的详细信息</param>
        /// <returns></returns>
        int InsertIdearByUserID(T_CustomerIdeaInfo customerIdeaInfo);
        #endregion
    }
}
