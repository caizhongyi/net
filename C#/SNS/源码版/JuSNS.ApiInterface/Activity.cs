using System;
using System.Collections.Generic;
using System.Text;
using JuSNS.Factory.ApiInterface;
using JuSNS.Model.ApiInterface;

namespace JuSNS.ApiInterface
{
    public class Activity
    {
        static private IActivity dal;
        static Activity()
        {
            dal = DataAccess.CreateActivity();
        }
        /// <summary>
        /// 添加动态信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        static public int Add(AppActiveInfo info)
        {
            return dal.Add(info);
        }
        /// <summary>
        /// 删除动态
        /// </summary>
        /// <param name="AppID"></param>
        /// <param name="ActiveID"></param>       
        /// <returns></returns>
        static public int Del(int AppID, int ActiveID)
        {
            return dal.Del(AppID, ActiveID);
        }
        /// <summary>
        /// 取得某人的动态
        /// </summary>
        /// <param name="AppID"></param>
        /// <param name="UserID"></param>
        /// <param name="Num"></param>
        /// <returns></returns>
        static public List<AppActiveInfo> GetList(int AppID, int UserID, int Num)
        {
            return dal.GetList(AppID, UserID, Num);
        }
        /// <summary>
        /// 取得某人包括其好友的动态
        /// </summary>
        /// <param name="AppID"></param>
        /// <param name="UserID"></param>
        /// <param name="Num"></param>
        /// <returns></returns>
        static public List<AppActiveInfo> GetFriendActivity(int AppID, int UserID, int Num)
        {
            return dal.GetFriendActivity(AppID, UserID, Num);
        }
        /// <summary>
        /// 取得动态详细信息
        /// </summary>
        /// <param name="AppID"></param>
        /// <param name="ActiveID">动态ID</param>        
        /// <returns></returns>
        static public AppActiveInfo GetInfo(int AppID, int ActiveID)
        {
            return dal.GetInfo(AppID, ActiveID);
        }
    }
}
