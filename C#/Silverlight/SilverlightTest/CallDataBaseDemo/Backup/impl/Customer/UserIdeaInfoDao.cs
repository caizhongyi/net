using System;
using System.Collections.Generic;
using System.Text;
using DAL.Dao.Customer;
using Model;

namespace DAL.impl.Customer
{
    class UserIdeaInfoDao : IUserIdeaInfo
    {
        Util util = new Util();

        #region InsertIdearByUserID 将用户提出来的意见添加到数据库中
        /// <summary>
        /// 将用户提出来的意见添加到数据库中
        /// </summary>
        /// <param name="customerIdeaInfo">用户意见的详细信息</param>
        /// <returns></returns>
        public int InsertIdearByUserID(T_CustomerIdeaInfo customerIdeaInfo)
        {
            //添加数据要有顺序
            string cmdText = "insert into T_CustomerIdeaInfo values('" + customerIdeaInfo.ITitle + "','" + customerIdeaInfo.ITId + "','" + customerIdeaInfo.IContent + "','" + customerIdeaInfo.ITime + "','" + customerIdeaInfo.ICId + "','"+customerIdeaInfo.IRemark+"')";
            return util.GetExecuteNonQuery(cmdText);
        }
        #endregion
    }
}
