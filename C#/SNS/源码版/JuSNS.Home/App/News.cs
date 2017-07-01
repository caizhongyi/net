using System.Collections.Generic;
using JuSNS.Factory.App;
using JuSNS.Model;

namespace JuSNS.Home.App
{
    public class News
    {
        static readonly private News _instance = new News();
        JuSNS.Factory.App.INews dal;
        private News()
        {
            dal = DataAccess.CreateNews();
        }

        /// <summary>
        /// 取得实例
        /// </summary>
        static public News Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// 删除新闻
        /// </summary>
        /// <param name="nid"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public int DeleteNews(int nid, int uid)
        {
            return dal.DeleteNews(nid, uid);
        }

        /// <summary>
        /// 得到资讯分类
        /// </summary>
        /// <param name="ParentID">父ID</param>
        /// <param name="Type">0资讯，1文章，2资源</param>
        /// <returns>List对象</returns>
        public List<NewsChannelInfo> GetNewsChannel(int ParentID, int Type)
        {
            return dal.GetNewsChannel(ParentID, Type);
        }

        /// <summary>
        /// 插入/修改资讯
        /// </summary>
        /// <param name="Info"></param>
        /// <returns></returns>
        public int InsertUpdateNews(NewsInfo Info)
        {
            return dal.InsertNews(Info);
        }
        /// <summary>
        /// 插入/修改资讯分类
        /// </summary>
        /// <param name="Info"></param>
        /// <returns></returns>
        public int InsertUpdateNewsClass(NewsChannelInfo Info)
        {
            return dal.InsertUpdateNewsClass(Info);
        }

        /// <summary>
        /// 得到资讯信息
        /// </summary>
        /// <param name="nid"></param>
        /// <returns></returns>
        public NewsInfo GetNewsInfo(object nid)
        {
            return dal.GetNewsInfo(nid);
        }
        /// <summary>
        /// 得到资讯分类信息
        /// </summary>
        /// <param name="nid"></param>
        /// <returns></returns>
        public NewsChannelInfo GetNewsChannelInfo(object cid)
        {
            return dal.GetNewsChannelInfo(cid);
        }

        /// <summary>
        /// 更新新闻点击率
        /// </summary>
        /// <param name="BID"></param>
        /// <returns></returns>
        public int UpdateNewsState(int nid)
        {
            return dal.UpdateNewsState(nid);
        }

        /// <summary>
        /// 更新关注
        /// </summary>
        /// <param name="BID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public int UpdateATT(int NID, int UserID)
        {
            return dal.UpdateATT(NID, UserID);
        }

        /// <summary>
        /// 得到评论具体信息
        /// </summary>
        /// <param name="CID"></param>
        /// <returns></returns>
        public NewsCommentInfo GetNewsCommentInfo(int CID)
        {
            return dal.GetNewsCommentInfo(CID);
        }

        /// <summary>
        /// 调用某个类型的日志
        /// </summary>
        /// <param name="Number">调用数量</param>
        /// <param name="Flag">0点击排行,1关注排行,2分享排行,3推荐,4评论排行,5最新更新,6系统</param>
        /// <param name="UserID">用户ID，如果大于0则表示调用用户的日志</param>
        /// <returns>List列表</returns>
        public List<NewsInfo> GetNewsList(int Number, int Flag, int UserID)
        {
            return dal.GetNewsList(Number, Flag, UserID);
        }

        /// <summary>
        /// 插入日志评论
        /// </summary>
        /// <param name="Info"></param>
        /// <returns></returns>
        public int InsertNewsComment(NewsCommentInfo Info)
        {
            return dal.InsertNewsComment(Info);
        }

        /// <summary>
        /// 删除资讯评论
        /// </summary>
        /// <param name="CID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public int DeleteNewsComment(int CID, int UserID)
        {
            return dal.DeleteNewsComment(CID, UserID);
        }

    }
}
