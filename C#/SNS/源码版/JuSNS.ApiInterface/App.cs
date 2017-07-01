using System;
using System.Collections.Generic;
using System.Text;
using JuSNS.Factory.ApiInterface;
using JuSNS.Model.ApiInterface;

namespace JuSNS.ApiInterface
{
    /// <summary>
    /// 应用程序类
    /// </summary>
    public class App
    {
        static private IApp dal;
        static App()
        {
            dal = DataAccess.CreateApp();
        }
        /// <summary>
        /// 锁定应用程序
        /// </summary>
        /// <param name="AppID">应用程序ID</param>
        /// <returns></returns>
        static public int Lock(int AppID)
        {
            return dal.Lock(AppID);
        }
        /// <summary>
        /// 编辑应用程序
        /// </summary>
        /// <param name="info">应用程序ID</param>
        /// <returns></returns>
        static public int EditApp(AppInfo info)
        {
            return dal.EditApp(info);
        }
        /// <summary>
        /// 删除应用程序
        /// </summary>
        /// <param name="AppID">应用程序ID</param>
        /// <returns></returns>
        static public int Delete(int AppID)
        {
            return dal.Delete(AppID);
        }
        /// <summary>
        /// 审核开发者
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <returns></returns>
        static public int VerifyDeveloper(int UserID)
        {
            return dal.VerifyDeveloper(UserID);
        }
        /// <summary>
        /// 锁定开发者
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <returns></returns>
        static public int LockDeveloper(int UserID)
        {
            return dal.LockDeveloper(UserID);
        }
        /// <summary>
        /// 验证AppKey
        /// </summary>
        /// <param name="key">AppKey</param>
        /// <returns></returns>
        static public bool VerifyKey(string key, out int AppID)
        {
            return dal.VerifyKey(key,out AppID);
        }
        /// <summary>
        /// 取得应用程序信息
        /// </summary>
        /// <param name="AppID"></param>
        /// <returns></returns>
        static public AppInfo GetAppInfo(int AppID)
        {
            return dal.GetAppInfo(AppID);
        }
    }
}
