using System.Collections.Generic;
using JuSNS.Model;
using System.Reflection;

namespace JuSNS.Factory.App
{
    public interface IWeb
    {
        /// <summary>
        /// 推荐信息
        /// </summary>
        /// <param name="infoid"></param>
        /// <param name="uid"></param>
        /// <param name="flag"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        int RecAll(int infoid, int uid, int flag, string type);
        /// <summary>
        /// 得到帮助信息
        /// </summary>
        /// <param name="helpid"></param>
        /// <returns></returns>
        HelpInfo GetHelp(int helpid);
        HelpInfo GetHelpQ(string q);
        /// <summary>
        /// 插入帮助
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        int InsertHelp(HelpInfo info);
        /// <summary>
        /// 删除帮助
        /// </summary>
        /// <param name="hid"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        int deletehelp(int hid, int uid);
        /// <summary>
        /// 得到用户排行
        /// </summary>
        /// <param name="number"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        List<UserInfo> GetTopUser(int number, int flag);
        /// <summary>
        /// 得到日志列表
        /// </summary>
        /// <param name="number"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        List<BlogInfo> GetBlogList(int number, int flag);
        /// <summary>
        /// 得到新闻列表
        /// </summary>
        /// <param name="number"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        List<NewsInfo> GetNewsList(int number, int flag);
        /// <summary>
        /// 得到社群列表
        /// </summary>
        /// <param name="number"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        List<GroupInfo> GetGroupList(int number, int flag);

        int SetGroupLight(int gid, int uid, int flag);
        /// <summary>
        /// 得到商品列表
        /// </summary>
        /// <param name="number"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        List<ShopGoodsInfo> GetGoodsList(int number, int flag);
        /// <summary>
        /// 得到店铺列表
        /// </summary>
        /// <param name="number"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        List<ShopInfo> GetShopList(int number, int flag);
        /// <summary>
        /// 得到活动列表
        /// </summary>
        /// <param name="number"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        List<AtiveInfo> GetActiveList(int number, int flag);
        /// <summary>
        /// 得到问答列表
        /// </summary>
        /// <param name="number"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        List<AskInfo> GetAskList(int number, int flag);
        /// <summary>
        /// 得到投票列表
        /// </summary>
        /// <param name="number"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        List<VoteInfo> GetVoteList(int number, int flag);
        /// <summary>
        /// 得到相片列表
        /// </summary>
        /// <param name="number"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        List<PhotoInfo> GetPhotoList(int number, int flag);
        /// <summary>
        /// 得到某个群的群会员数量
        /// </summary>
        /// <param name="gid"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        int GetGroupCount(int gid, int flag);
        /// <summary>
        /// 得到微博列表
        /// </summary>
        /// <param name="number"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        List<TwitterInfo> GetTwitterList(int number, int flag);
        /// <summary>
        /// 得到相册
        /// </summary>
        /// <param name="number"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        List<AlbumInfo> GetGAlbumList(int number, int flag);
        /// <summary>
        /// 得到主题
        /// </summary>
        /// <param name="number"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        List<GroupTopicInfo> GetTopicList(int number, int flag);
        /// <summary>
        /// 得到群组分类列表
        /// </summary>
        /// <param name="parentid"></param>
        /// <returns></returns>
        List<GroupClassInfo> GetTopicClassList(int parentid);
        /// <summary>
        /// 设置信息的状态
        /// </summary>
        /// <param name="infoid"></param>
        /// <param name="uid"></param>
        /// <param name="flag"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        int SetAllState(int infoid, int uid, int flag, string type);
        /// <summary>
        /// 更改信息状态
        /// </summary>
        /// <param name="infoid"></param>
        /// <param name="uid"></param>
        /// <param name="flag"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        int CheckInfoState(int infoid, int uid, int flag, string type);
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        int Start(string type);
        /// <summary>
        /// 删除资讯分类
        /// </summary>
        /// <param name="infoid">被操作ID</param>
        /// <param name="uid">操作者</param>
        /// <returns>0失败，1成功</returns>
        int DeleteNewsClass(int infoid, int uid);
        /// <summary>
        /// 插入广告
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        int InsertAds(AdsInfo info);
        /// <summary>
        /// 得到广告
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        AdsInfo GetAdsInfo(int aid);
        /// <summary>
        /// 删除广告
        /// </summary>
        /// <param name="infoid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        int DeleteAds(int infoid, int userid);

        List<AdsInfo> GetAdsList(string ptype, int number);
        /// <summary>
        /// 插入邮件找回密码记录
        /// </summary>
        /// <param name="email"></param>
        /// <param name="vcode"></param>
        /// <returns></returns>
        int InsertEmailNotice(EmailNoticeInfo info);
        /// <summary>
        /// 得到邮件验证类型
        /// </summary>
        /// <param name="vcode"></param>
        /// <returns></returns>
        EmailNoticeInfo GetEmailNoticeInfo(string vcode);
        /// <summary>
        /// 插入举报信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        int InsertReport(ReportInfo info);

        /// <summary>
        /// 删除举报
        /// </summary>
        /// <param name="rid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        int DeleteReport(int rid, int userid);
        /// <summary>
        /// 删除动态
        /// </summary>
        /// <param name="did"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        int DeleteDyn(int did, int userid);

        List<LinksInfo> GetLinksList(int number, int type);

        LinksInfo GetLinksInfo(int lid);

        int DeleteLinks(int lid,int uid);

        int InsertLinks(LinksInfo info);

        bool GetApiKey(string apikey);
        bool ProgrameInstall();
    }

    public sealed partial class DataAccess
    {
        public static IWeb CreateWeb()
        {
            string className = path + ".App.Web";
            object objType =JuSNS.Common.DataCache.GetCache(className);
            if (objType == null)
            {
                try
                {
                    objType = Assembly.Load(path).CreateInstance(className);
                    JuSNS.Common.DataCache.SetCache(className, objType);// 写入缓存
                }
                catch { }
            }
            return (IWeb)objType;
        }
    }
}
