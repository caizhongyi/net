using System.Collections.Generic;
using JuSNS.Factory.App;
using JuSNS.Model;

namespace JuSNS.Home.App
{
    public class Share
    {
        static readonly private Share _instance = new Share();
        JuSNS.Factory.App.IShare dal;
        private Share()
        {
            dal = DataAccess.CreateShare();
        }

        /// <summary>
        /// 取得实例
        /// </summary>
        static public Share Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// 插入分享
        /// </summary>
        /// <param name="info">分享实体类</param>
        /// <returns>0失败，1成功</returns>
        public int InsertShare(ShareInfo info)
        {
            return dal.InsertShare(info);
        }

        /// <summary>
        /// 删除分享
        /// </summary>
        /// <param name="uid">操作用户ID</param>
        /// <param name="sid">分享ID</param>
        /// <returns>1成功，0失败</returns>
        public int DeleteShare(int sid, int uid)
        {
            return dal.DeleteShare(sid, uid);
        }

        /// <summary>
        /// 得到分享信息
        /// </summary>
        /// <param name="sid">分享ID</param>
        /// <returns>ShareInfo实体类</returns>
        public ShareInfo GetInfo(object sid)
        {
            return dal.GetInfo(sid);
        }
    }
}