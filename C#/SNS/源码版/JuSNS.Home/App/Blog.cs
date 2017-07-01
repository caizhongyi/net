using System.Collections.Generic;
using JuSNS.Factory.App;
using JuSNS.Model;

namespace JuSNS.Home.App
{
    public class Blog
    {
        static readonly private Blog _instance = new Blog();
        JuSNS.Factory.App.IBlog dal;
        private Blog()
        {
            dal = DataAccess.CreateBlog();
        }

        /// <summary>
        /// 取得实例
        /// </summary>
        static public Blog Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// 得到日志分类
        /// </summary>
        /// <param name="userid">0表示系统分类，其他表示用户的分类</param>
        /// <param name="ParentID">父ID</param>
        /// <returns>List对象</returns>
        public List<BlogClassInfo> GetBlogClass(int userid, int parentid)
        {
            return dal.GetBlogClass(userid, parentid);
        }

        /// <summary>
        /// 增加日志分类
        /// </summary>
        /// <param name="SortName">分类名称</param>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        public int AddSortBlogClass(string sortname, int userid)
        {
            return dal.AddSortBlogClass(sortname, userid);
        }

        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="bid"></param>
        /// <returns></returns>
        public int DeleteBlogClass(int userid, int bid)
        {
            return dal.DeleteBlogClass(userid, bid);
        }

        /// <summary>
        /// 更新或新加日志
        /// </summary>
        /// <param name="Info">日志实体类</param>
        /// <returns></returns>
        public int UpdateBlog(BlogInfo info)
        {
            return dal.UpdateBlog(info);
        }

        /// <summary>
        /// 得到日志信息
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="bid"></param>
        /// <returns></returns>
        public BlogInfo GetBlogInfo(int userid, object bid)
        {
            return dal.GetBlogInfo(userid, bid);
        }
        /// <summary>
        /// 得到日志信息
        /// </summary>
        /// <param name="bid"></param>
        /// <returns></returns>
        public BlogInfo GetBlogInfo(object bid)
        {
            return dal.GetBlogInfo(bid);
        }

        /// <summary>
        /// 插入日志分类
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int InsertBlogClass(BlogClassInfo info)
        {
            return dal.InsertBlogClass(info);
        }

        /// <summary>
        /// 得到日志分类信息
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="bid"></param>
        /// <returns></returns>
        public BlogClassInfo GetBlogClassInfo(object cid)
        {
            return dal.GetBlogClassInfo(cid);
        }

        /// <summary>
        /// 得到博客分类名称
        /// </summary>
        /// <param name="bid"></param>
        /// <returns></returns>
        public string GetClassName(int bid)
        {
            return dal.GetClassName(bid);
        }

        /// <summary>
        /// 删除博客
        /// </summary>
        /// <param name="bid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public int DeleteBlog(int bid, int userid)
        {
            return dal.DeleteBlog(bid, userid);
        }

        /// <summary>
        /// 删除日志评论
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public int DeleteBlogComment(int cid, int userid)
        {
            return dal.DeleteBlogComment(cid, userid);
        }

        /// <summary>
        /// 更新关注
        /// </summary>
        /// <param name="bid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public int UpdateATT(int bid, int userid)
        {
            return dal.UpdateATT(bid, userid);
        }

        /// <summary>
        /// 更新点击率
        /// </summary>
        /// <param name="bid"></param>
        /// <returns></returns>
        public int UpdateBlogState(int bid,int userid)
        {
            return dal.UpdateBlogState(bid, userid);
        }

        /// <summary>
        /// 插入日志评论
        /// </summary>
        /// <param name="Info"></param>
        /// <returns></returns>
        public int InsertBlogComment(BlogCommentInfo info)
        {
            return dal.InsertBlogComment(info);
        }

        /// <summary>
        /// 得到脚印
        /// </summary>
        /// <param name="number">调用数量</param>
        /// <param name="fid">日志ID</param>
        /// <returns></returns>
        public List<BlogFootInfo> GetBlogFoot(int number, int fid)
        {
            return dal.GetBlogFoot(number, fid);
        }

        /// <summary>
        /// 得到评论具体信息
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public BlogCommentInfo GetBlogCommentInfo(int cid)
        {
            return dal.GetBlogCommentInfo(cid);
        }

        /// <summary>
        /// 调用某个类型的日志
        /// </summary>
        /// <param name="number">调用数量</param>
        /// <param name="flag">0点击排行,1关注排行,2分享排行,3推荐,4评论排行,5最新更新</param>
        /// <param name="userid">用户ID，如果大于0则表示调用用户的日志</param>
        /// <returns>List列表</returns>
        public List<BlogInfo> GetBlogList(int number, int flag,int userid)
        {
            return dal.GetBlogList(number, flag, userid);
        }
    }
}
