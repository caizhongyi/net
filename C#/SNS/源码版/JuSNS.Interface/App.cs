using System.Collections.Generic;
using JuSNS.Factory.App;
using JuSNS.Model;

namespace JuSNS.App
{
    public class App
    {
        static readonly private App _instance = new App();
        JuSNS.Factory.App.IApp dal;
        private App()
        {
            dal = DataAccess.CreateApp();
        }

        /// <summary>
        /// 取得实例
        /// </summary>
        static public App Instance
        {
            get { return _instance; }
        }
         
        /// <summary>
        /// 是否是开发者
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns>返回true或false</returns>
        public bool IsDeveloper(int userid)
        {
            return dal.IsDeveloper(userid);
        }

        /// <summary>
        /// 成为开发者
        /// </summary>
        /// <param name="info">AppDeveloperInfo实体类</param>
        /// <returns>0成功，1失败</returns>
        public int InsertDev(AppDeveloperInfo info)
        {
            return dal.InsertDev(info);
        }

        /// <summary>
        /// 得到开发者信息
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns>AppDeveloperInfo实体类</returns>
        public AppDeveloperInfo GetDevInfo(int userid)
        {
            return dal.GetDevInfo(userid);
        }

        /// <summary>
        /// 添加/更新应用程序
        /// </summary>
        /// <param name="info">AppInfo实体类</param>
        /// <returns>0失败，1成功</returns>
        public int InsertApp(AppInfo info)
        {
            return dal.InsertApp(info);
        }

        /// <summary>
        /// 得到应用程序信息
        /// </summary>
        /// <param name="appid">应用程序ID</param>
        /// <returns>AppInfo实体类</returns>
        public AppInfo GetAppInfo(int appid)
        {
            return dal.GetAppInfo(appid);
        }

        /// <summary>
        /// 删除应用程序
        /// </summary>
        /// <param name="appid">应用程序ID</param>
        /// <param name="userid">操作用户ID</param>
        /// <returns>0失败，1成功</returns>
        public int DeleteApp(int appid, int userid)
        {
            return dal.DeleteApp(appid, userid);
        }
        /// <summary>
        /// 删除应用开发者
        /// </summary>
        /// <param name="appid">应用程序ID</param>
        /// <param name="userid">操作用户ID</param>
        /// <returns>0失败，1成功</returns>
        public int DeleteAppdev(int appid, int userid)
        {
            return dal.DeleteAppdev(appid, userid);
        }

        /// <summary>
        /// 判断是否安装应用程序
        /// </summary>
        /// <param name="appid">appid</param>
        /// <param name="userid">当前用户ID</param>
        /// <returns>true或false</returns>
        public bool IsSetupApp(int appid, int userid)
        {
            return dal.IsSetupApp(appid, userid);
        }
        /// <summary>
        /// 更新应用程序点击次数
        /// </summary>
        /// <param name="appid">appid</param>
        /// <returns>0失败，1成功</returns>
        public int UpdateAppClick(int appid)
        {
            return dal.UpdateAppClick(appid);
        }
        /// <summary>
        /// 更新应用程序安装量
        /// </summary>
        /// <param name="appid">appid</param>
        /// <returns>0失败，1成功</returns>
        public int UpdateAppSetup(int appid)
        {
            return dal.UpdateAppSetup(appid);
        }

        /// <summary>
        /// 安装/卸载应用程序
        /// </summary>
        /// <param name="appid">appid</param>
        /// <param name="userid">安装用户ID</param>
        /// <param name="flag">1安装，0卸载</param>
        /// <returns>0失败，1成功</returns>
        public int InsertSetupApp(int appid, int userid, int flag)
        {
            return dal.InsertSetupApp(appid, userid, flag);
        }

    }
}
