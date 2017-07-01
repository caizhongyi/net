using JuSNS.Factory.App;
using JuSNS.Model;

namespace JuSNS.Home.App
{
    public class TWrite
    {
        static readonly private TWrite _instance = new TWrite();
        JuSNS.Factory.App.ITWrite dal;
        private TWrite()
        {
            dal = DataAccess.CreateTWrite();
        }

        /// <summary>
        /// 取得实例
        /// </summary>
        static public TWrite Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// 得到最新的微博
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public string GetTwritterNew(object UserID)
        {
            return dal.GetTwritterNew(UserID);
        }

        /// <summary>
        /// 得到最新的微博
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="tid"></param>
        /// <returns></returns>
        public string GetTwritterNew(object UserID,out int tid)
        {
            return dal.GetTwritterNew(UserID,out tid);
        }

        /// <summary>
        /// 插入微博客日志
        /// </summary>
        /// <param name="Info">微博客实体类</param>
        /// <returns>0失败，1成功</returns>
        public int InserTwitter(TwitterInfo Info)
        {
            return dal.InserTwitter(Info);
        }

        /// <summary>
        /// 删除微博
        /// </summary>
        /// <param name="TID">微博ID</param>
        /// <param name="UserID">用户ID</param>
        /// <returns>0失败，1成功</returns>
        public int DeleteTwitter(int TID, int UserID)
        {
            return dal.DeleteTwitter(TID, UserID);
        }

        /// <summary>
        /// 得到评论数
        /// </summary>
        /// <param name="tid">微博ID</param>
        /// <returns>评论数量</returns>
        public int GetTwitterComments(object tid)
        {
            return dal.GetTwitterComments(tid);
        }

        /// <summary>
        /// 得到制定微博的类型
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        public TwitterInfo GetTwitterInfo(object tid)
        {
            return dal.GetTwitterInfo(tid);
        }
        /// <summary>
        /// 得到微博单个评论信息
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public TwitterCommentInfo GetTwitterCommentInfo(object cid)
        {
            return dal.GetTwitterCommentInfo(cid);
        }

        /// <summary>
        /// 插入微博评论
        /// </summary>
        /// <param name="info">微博评论实体类</param>
        /// <returns>0失败，1成功</returns>
        public int InserTwitterComment(TwitterCommentInfo info)
        {
            return dal.InserTwitterComment(info);
        }

        /// <summary>
        /// 删除博客评论
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public int DeleteTwitterComment(int cid, int uid)
        {
            return dal.DeleteTwitterComment(cid, uid);
        }
    }
}
