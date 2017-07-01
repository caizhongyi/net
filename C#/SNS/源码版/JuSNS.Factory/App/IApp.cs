using System.Collections.Generic;
using JuSNS.Model;
using System.Reflection;

namespace JuSNS.Factory.App
{
    public interface IApp
    {
        /// <summary>
        /// 是否是开发者
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        bool IsDeveloper(int userid);
        /// <summary>
        /// 插入开发者信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        int InsertDev(AppDeveloperInfo info);
        /// <summary>
        /// 得到开发者信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        AppDeveloperInfo GetDevInfo(int userid);
        /// <summary>
        /// 插入应用程序
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        int InsertApp(AppInfo info);
        /// <summary>
        /// 得到应用程序信息
        /// </summary>
        /// <param name="appid"></param>
        /// <returns></returns>
        AppInfo GetAppInfo(int appid);
        /// <summary>
        /// 删除应用程序
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        int DeleteApp(int appid, int userid);

        /// <summary>
        /// 删除应用开发者
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        int DeleteAppdev(int appid, int userid);

        bool IsSetupApp(int appid, int userid);

        int UpdateAppClick(int appid);

        int UpdateAppSetup(int appid);

        int InsertSetupApp(int appid, int userid, int flag);

    }

    public sealed partial class DataAccess
    {
        public static IApp CreateApp()
        {
            string className = path + ".App.App";
            object objType = JuSNS.Common.DataCache.GetCache(className);
            if (objType == null)
            {
                try
                {
                    objType = Assembly.Load(path).CreateInstance(className);
                    JuSNS.Common.DataCache.SetCache(className, objType);// 写入缓存
                }
                catch { }
            }
            return (IApp)objType;
        }
    }


}
