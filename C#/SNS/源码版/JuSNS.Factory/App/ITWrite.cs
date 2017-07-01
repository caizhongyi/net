using JuSNS.Model;
using System.Reflection;
namespace JuSNS.Factory.App
{
    public interface ITWrite
    {
        string GetTwritterNew(object UserID);

        string GetTwritterNew(object UserID, out int tid);

        int InserTwitter(TwitterInfo Info);

        int DeleteTwitter(int TID, int UserID);

        int GetTwitterComments(object tid);

        TwitterInfo GetTwitterInfo(object tid);

        TwitterCommentInfo GetTwitterCommentInfo(object cid);

        int InserTwitterComment(TwitterCommentInfo info);

        int DeleteTwitterComment(int cid, int uid);
    }

    public sealed partial class DataAccess
    {
        public static ITWrite CreateTWrite()
        {
            string className = path + ".App.TWrite";
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
            return (ITWrite)objType;
        }
    }
}
