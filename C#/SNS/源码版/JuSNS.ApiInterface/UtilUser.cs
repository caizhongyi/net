using System;
using System.Collections.Generic;
using System.Text;
using JuSNS.Factory.ApiInterface;

namespace JuSNS.ApiInterface
{
    /// <summary>
    /// 用户工具类
    /// </summary>
    public class UtilUser
    {
        static private IUtilUser dal;
        static UtilUser()
        {
            dal = DataAccess.CreateUtilUser();
        }
        /// <summary>
        /// 添加应用程序
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="ApiID"></param>
        /// <returns></returns>
        static public int AddApi(int UserID, int ApiID)
        {
            return dal.AddApi(UserID, ApiID);
        }
        /// <summary>
        /// 删除应用程序
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="AppID"></param>
        /// <returns></returns>
        static public int DeleteApi(int UserID, int AppID)
        {
            return dal.DeleteApi(UserID, AppID);
        }
    }
}
