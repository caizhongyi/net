using System.Data;
using JuSNS.Factory;
using JuSNS.Model;

namespace JuSNS.Home
{
    /// <summary>
    /// 通用分页
    /// </summary>
    public class UtilPage
    {
        static private IUtilPage dal;
        /// <summary>
        /// 构造函数
        /// </summary>
        static UtilPage()
        {
            dal = DataAccess.CreateUtilPage();
        }

        static public DataTable GetPage(string pageCode, int pageindex, int pagesize, out int recordcount, out int pagecount, params SqlConditionInfo[] conditions)
        {
            return dal.GetPage(pageCode, pageindex, pagesize, out  recordcount, out  pagecount, conditions);
        }

        static public DataTable GetUserSearchPage(object r, object keys, int ishead, object city, object syear, object eyear, int sex, object userid, int pageindex, int pagesize, out int recordcount, out int pagecount, params SqlConditionInfo[] SqlCondition)
        {
            return dal.GetUserSearchPage(r, keys, ishead, city, syear, eyear, sex, userid, pageindex, pagesize, out recordcount, out pagecount, SqlCondition);
        }

        static public DataTable GetBlogPage(string q, string keys, int classid, string orderby, int userid, int pageindex, int pagesize, out int recordcount, out int pagecount, params SqlConditionInfo[] SqlCondition)
        {
            return dal.GetBlogPage(q, keys, classid, orderby, userid, pageindex, pagesize, out recordcount, out pagecount, SqlCondition);
        }

        static public DataTable GetNewsPage(string q,int pageindex, int pagesize, out int recordcount, out int pagecount, params SqlConditionInfo[] SqlCondition)
        {
            return dal.GetNewsPage(q, pageindex, pagesize, out recordcount, out pagecount, SqlCondition);
        }

        static public DataTable GetNewsInfoPage(string q, string keys, int userid, int pageindex, int pagesize, out int recordcount, out int pagecount, params SqlConditionInfo[] SqlCondition)
        {
            return dal.GetNewsInfoPage(q, keys, userid, pageindex, pagesize, out recordcount, out pagecount, SqlCondition);
        }

        static public DataTable GetAlbumPage(string q, string keys, int userid, int pageindex, int pagesize, out int recordcount, out int pagecount, params SqlConditionInfo[] SqlCondition)
        {
            return dal.GetAlbumPage(q, keys, userid, pageindex, pagesize, out recordcount, out pagecount, SqlCondition);
        }

        static public DataTable GetGroupPage(string q, string keys, int classid, int userid, int pageindex, int pagesize, out int recordcount, out int pagecount, params SqlConditionInfo[] SqlCondition)
        {
            return dal.GetGroupPage(q, keys, classid, userid, pageindex, pagesize, out recordcount, out pagecount, SqlCondition);
        }

        static public DataTable GetGroupTopicPage(string q, int gid, int userid, int pageindex, int pagesize, out int recordcount, out int pagecount, params SqlConditionInfo[] SqlCondition)
        {
            return dal.GetGroupTopicPage(q, gid, userid, pageindex, pagesize, out recordcount, out pagecount, SqlCondition);
        }

        static public DataTable GetAskPage(string q, string keys, int classid, int userid, int pageindex, int pagesize, out int recordcount, out int pagecount, params SqlConditionInfo[] SqlCondition)
        {
            return dal.GetAskPage(q, keys, classid, userid, pageindex, pagesize, out recordcount, out pagecount, SqlCondition);
        }

        static public DataTable GetAtivePage(string q, string keys, int classid, int userid, int pageindex, int pagesize, out int recordcount, out int pagecount, params SqlConditionInfo[] SqlCondition)
        {
            return dal.GetAtivePage(q, keys, classid, userid, pageindex, pagesize, out recordcount, out pagecount, SqlCondition);
        }

        static public DataTable GetGoodsPage(string q, string keys, int classid, int userid, int pageindex, int pagesize, out int recordcount, out int pagecount, params SqlConditionInfo[] SqlCondition)
        {
            return dal.GetGoodsPage(q, keys, classid, userid, pageindex, pagesize, out recordcount, out pagecount, SqlCondition);
        }

        static public DataTable GetMultePage(string q, string keys, int userid, int pageindex, int pagesize, out int recordcount, out int pagecount, params SqlConditionInfo[] SqlCondition)
        {
            return dal.GetMultePage(q, keys, userid, pageindex, pagesize, out recordcount, out pagecount, SqlCondition);
        }

        static public DataTable GetShopPage(string q, string keys, int classid, int userid, int pageindex, int pagesize, out int recordcount, out int pagecount, params SqlConditionInfo[] SqlCondition)
        {
            return dal.GetShopPage(q, keys, classid, userid, pageindex, pagesize, out recordcount, out pagecount, SqlCondition);
        }

        static public DataTable GetSharePage(string q, string keys, string t, int userid, int pageindex, int pagesize, out int recordcount, out int pagecount, params SqlConditionInfo[] SqlCondition)
        {
            return dal.GetSharePage(q, keys, t, userid, pageindex, pagesize, out recordcount, out pagecount, SqlCondition);
        }

        static public DataTable GetVotePage(string q, string keys, int userid, int pageindex, int pagesize, out int recordcount, out int pagecount, params SqlConditionInfo[] SqlCondition)
        {
            return dal.GetVotePage(q, keys, userid, pageindex, pagesize, out recordcount, out pagecount, SqlCondition);
        }

        static public DataTable GetFavoritePage(string q, int classid, string keys, int userid, int pageindex, int pagesize, out int recordcount, out int pagecount, params SqlConditionInfo[] SqlCondition)
        {
            return dal.GetFavoritePage(q, classid, keys, userid, pageindex, pagesize, out recordcount, out pagecount, SqlCondition);
        }

        static public DataTable GetFriendPage(string keys, int userid, int classid, int pageindex, int pagesize, out int recordcount, out int pagecount, params SqlConditionInfo[] SqlCondition)
        {
            return dal.GetFriendPage(keys, userid, classid, pageindex, pagesize, out recordcount, out pagecount, SqlCondition);
        }

        static public DataTable GetDynAllPage(int userid, string dyntype, string killuser, int pageindex, int pagesize, out int recordcount, out int pagecount)
        {
            return dal.GetDynAllPage(userid, dyntype, killuser, pageindex, pagesize, out recordcount, out pagecount);
        }

        static public DataTable GetTwitterPage(string q, string keys, int userid, int pageindex, int pagesize, out int recordcount, out int pagecount, params SqlConditionInfo[] SqlCondition)
        {
            return dal.GetTwitterPage(q, keys, userid, pageindex, pagesize, out recordcount, out pagecount, SqlCondition);
        }

        static public DataTable GetOnlinePage(string q, string keys, int userid, int pageindex, int pagesize, out int recordcount, out int pagecount, params SqlConditionInfo[] SqlCondition)
        {
            return dal.GetOnlinePage(q, keys, userid, pageindex, pagesize, out recordcount, out pagecount, SqlCondition);
        }

    }
}
