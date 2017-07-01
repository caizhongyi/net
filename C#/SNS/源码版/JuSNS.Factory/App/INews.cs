using System.Collections.Generic;
using JuSNS.Model;
using System.Reflection;

namespace JuSNS.Factory.App
{
    public interface INews
    {
        int DeleteNews(int nid, int uid);
        List<NewsChannelInfo> GetNewsChannel(int ParentID, int Type);
        int InsertNews(NewsInfo Info);
        int InsertUpdateNewsClass(NewsChannelInfo Info);
        NewsInfo GetNewsInfo(object nid);
        NewsChannelInfo GetNewsChannelInfo(object cid);
        int UpdateNewsState(int nid);
        int UpdateATT(int NID, int UserID);
        NewsCommentInfo GetNewsCommentInfo(int CID);
        List<NewsInfo> GetNewsList(int Number, int Flag, int UserID);
        int InsertNewsComment(NewsCommentInfo Info);
        int DeleteNewsComment(int CID, int UserID);
    }

    public sealed partial class DataAccess
    {
        public static INews CreateNews()
        {
            string className = path + ".App.News";
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
            return (INews)objType;
        }
    }

}
