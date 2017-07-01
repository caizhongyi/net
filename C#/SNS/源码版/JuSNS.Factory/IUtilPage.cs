using System.Data;
using System.Reflection;
using JuSNS.Model;

namespace JuSNS.Factory
{
    /// <summary>
    /// 通用分页
    /// </summary>
    public interface IUtilPage
    {
        DataTable GetPage(string pageCode, int pageIndex, int pagesize, out int recordcount, out int pagecount, params SqlConditionInfo[] conditions);
        DataTable GetUserSearchPage(object r, object keys, int ishead, object city, object syear, object eyear, int sex, object UserID, int PageIndex, int pagesize, out int recordcount, out int pagecount, params SqlConditionInfo[] SqlCondition);
        DataTable GetBlogPage(string q, string keys, int classid, string orderby, int UserID, int PageIndex, int pagesize, out int recordcount, out int pagecount, params SqlConditionInfo[] SqlCondition);
        DataTable GetNewsPage(string q, int PageIndex, int pagesize, out int recordcount, out int pagecount, params SqlConditionInfo[] SqlCondition);
        DataTable GetNewsInfoPage(string q, string keys, int UserID, int PageIndex, int pagesize, out int recordcount, out int pagecount, params SqlConditionInfo[] SqlCondition);
        DataTable GetAlbumPage(string q, string keys, int UserID, int PageIndex, int pagesize, out int recordcount, out int pagecount, params SqlConditionInfo[] SqlCondition);
        DataTable GetGroupPage(string q, string keys, int classid, int userid, int PageIndex, int pagesize, out int recordcount, out int pagecount, params SqlConditionInfo[] SqlCondition);
        DataTable GetGroupTopicPage(string q, int gid, int UserID, int PageIndex, int pagesize, out int recordcount, out int pagecount, params SqlConditionInfo[] SqlCondition);
        DataTable GetAskPage(string q, string keys, int classid, int UserID, int PageIndex, int pagesize, out int recordcount, out int pagecount, params SqlConditionInfo[] SqlCondition);
        DataTable GetAtivePage(string q, string keys, int classid, int UserID, int PageIndex, int pagesize, out int recordcount, out int pagecount, params SqlConditionInfo[] SqlCondition);
        DataTable GetGoodsPage(string q, string keys, int classid, int UserID, int PageIndex, int pagesize, out int recordcount, out int pagecount, params SqlConditionInfo[] SqlCondition);
        DataTable GetMultePage(string q, string keys, int userid, int pageindex, int pagesize, out int recordcount, out int pagecount, params SqlConditionInfo[] SqlCondition);
        DataTable GetShopPage(string q, string keys, int classid, int UserID, int PageIndex, int pagesize, out int recordcount, out int pagecount, params SqlConditionInfo[] SqlCondition);
        DataTable GetSharePage(string q, string keys, string t, int userid, int pageindex, int pagesize, out int recordcount, out int pagecount, params SqlConditionInfo[] SqlCondition);
        DataTable GetVotePage(string q, string keys, int userid, int pageindex, int pagesize, out int recordcount, out int pagecount, params SqlConditionInfo[] SqlCondition);
        DataTable GetFavoritePage(string q, int classid, string keys, int userid, int pageindex, int pagesize, out int recordcount, out int pagecount, params SqlConditionInfo[] SqlCondition);
        DataTable GetFriendPage(string keys, int userid, int classid, int pageindex, int pagesize, out int recordcount, out int pagecount, params SqlConditionInfo[] SqlCondition);
        DataTable GetDynAllPage(int userid,string dyntype, string killuser, int pageindex, int pagesize, out int recordcount, out int pagecount);
        DataTable GetTwitterPage(string q, string keys, int userid, int pageindex, int pagesize, out int recordcount, out int pagecount, params SqlConditionInfo[] SqlCondition);
        DataTable GetOnlinePage(string q, string keys, int userid, int pageindex, int pagesize, out int recordcount, out int pagecount, params SqlConditionInfo[] SqlCondition);
    }

    public sealed partial class DataAccess
    {
        public static IUtilPage CreateUtilPage()
        {
            string className = path + ".UtilPage";
            return (IUtilPage)Assembly.Load(path).CreateInstance(className);
        }
    }
}
