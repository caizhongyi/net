using System;
using System.Collections.Generic;
using System.Text;
using JuSNS.Factory.ApiInterface;

namespace JuSNS.ApiInterface
{
    /// <summary>
    /// 朋友类
    /// </summary>
    public class Friend
    {
        static private IFriend dal;
        static Friend()
        {
            dal = DataAccess.CreateFriend();
        }
        /// <summary>
        /// 得所指定用户所有好友
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <returns></returns>
        static public int[] Get(int UserID)
        {
            return dal.Get(UserID);
        }
        /// <summary>
        /// 是否是朋友
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="FriendID">朋友ID</param>
        /// <returns></returns>
        static public bool AreFriends(int UserID, int FriendID)
        {
            return dal.AreFriends(UserID, FriendID);
        }
        /// <summary>
        /// 取得安装了某APP的好友ID
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="AppID"></param>
        /// <returns></returns>
        static public int[] GetAppUsers(int UserID, int AppID)
        {
            return dal.GetAppUsers(UserID, AppID);
        }
    }
}
