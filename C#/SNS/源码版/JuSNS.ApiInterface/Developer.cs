using System;
using System.Collections.Generic;
using System.Text;
using JuSNS.Factory.ApiInterface;
using JuSNS.Model.ApiInterface;

namespace JuSNS.ApiInterface
{
    /// <summary>
    /// 开发者类
    /// </summary>
    public class Developer
    {
        static private IDeveloper dal;
        static Developer()
        {
            dal = DataAccess.CreateDeveloper();
        }
        /// <summary>
        /// 申请成为开发者
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <returns></returns>
        static int RequestDeveloper(int UserID)
        {
            return dal.RequestDeveloper(UserID);
        }
        /// <summary>
        /// 建立应用程序
        /// </summary>
        /// <param name="info">应用程序信息</param>
        /// <returns></returns>
        static int CreateApp(AppInfo info)
        {
            return dal.CreateApp(info);
        }
        /// <summary>
        /// 编辑应用程序信息
        /// </summary>
        /// <param name="info">应用程序信息</param>
        /// <returns></returns>
        static int EditApp(AppInfo info)
        {
            return dal.EditApp(info);
        }
    }
}
