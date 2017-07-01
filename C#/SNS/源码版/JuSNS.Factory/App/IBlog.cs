using System.Collections.Generic;
using JuSNS.Model;
using System.Reflection;

namespace JuSNS.Factory.App
{
    public interface IBlog
    {
        List<BlogClassInfo> GetBlogClass(int userid, int parentid);
        int AddSortBlogClass(string sortname, int userid);
        int DeleteBlogClass(int userid, int bid);
        int UpdateBlog(BlogInfo Info);
        BlogInfo GetBlogInfo(int userid, object bid);
        BlogInfo GetBlogInfo(object bid);
        BlogClassInfo GetBlogClassInfo(object cid);
        int InsertBlogClass(BlogClassInfo info);
        string GetClassName(int bid);
        int DeleteBlog(int bid, int userid);
        int UpdateATT(int bid, int userid);
        int UpdateBlogState(int bid, int userid);
        int InsertBlogComment(BlogCommentInfo Info);
        int DeleteBlogComment(int cid, int userid);
        List<BlogFootInfo> GetBlogFoot(int number, int fid);
        BlogCommentInfo GetBlogCommentInfo(int cid);
        List<BlogInfo> GetBlogList(int number, int flag, int userid);
    }
    public sealed partial class DataAccess
    {
        public static IBlog CreateBlog()
        {
            string className = path + ".App.Blog";
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
            return (IBlog)objType;
        }
    }
}
