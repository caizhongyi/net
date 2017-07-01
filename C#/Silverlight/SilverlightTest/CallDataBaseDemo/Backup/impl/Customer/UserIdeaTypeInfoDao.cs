using System;
using System.Collections.Generic;
using System.Text;
using DAL.Dao.Customer;
using System.Data;

namespace DAL.impl.Customer
{
    class UserIdeaTypeInfoDao:IUserIdeaTypeInfo
    {
        Util util = new Util();

        #region SelUserIdeaTypeInfo 将所有的意见类型查询出来
        /// <summary>
        /// 将所有的意见类型查询出来
        /// </summary>
        /// <returns></returns>
        public DataSet SelUserIdeaTypeInfo() 
        {
            string cmdText = "select * from T_IdeaTypeInfo";
            return util.GetDataSet(cmdText);
        }
        #endregion
    }
}
