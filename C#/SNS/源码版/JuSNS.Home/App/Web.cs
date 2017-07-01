using System.Collections.Generic;
using JuSNS.Factory.App;
using JuSNS.Model;

namespace JuSNS.Home.App
{
    public class Web
    {
        static readonly private Web _instance = new Web();
        JuSNS.Factory.App.IWeb dal;
        private Web()
        {
            dal = DataAccess.CreateWeb();
        }

        /// <summary>
        /// 取得实例
        /// </summary>
        static public Web Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// 推荐
        /// </summary>
        /// <param name="infoid"></param>
        /// <param name="uid"></param>
        /// <param name="flag"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public int RecAll(int infoid, int uid, int flag, string type)
        {
            return dal.RecAll(infoid, uid, flag, type);
        }

        /// <summary>
        /// 得到帮助
        /// </summary>
        /// <param name="helpid"></param>
        /// <returns></returns>
        public HelpInfo GetHelp(int helpid)
        {
            return dal.GetHelp(helpid);
        }

        /// <summary>
        /// 得到帮助
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        public HelpInfo GetHelpQ(string q)
        {
            return dal.GetHelpQ(q);
        }

        /// <summary>
        /// 添加或更新帮助
        /// </summary>
        /// <param name="info"></param>
        /// <returns>0失败，1成功</returns>
        public int InsertHelp(HelpInfo info)
        {
            return dal.InsertHelp(info);
        }

        public int deletehelp(int hid, int uid)
        {
            return dal.deletehelp(hid, uid);
        }

        /// <summary>
        /// 得到用户排行
        /// </summary>
        /// <param name="number">调用数量</param>
        /// <param name="flag">0表示热门Blog用户，1热门用户，2热门美女，3热门帅哥，4人气排行，5活跃度，6积分排行，7推荐用户，8被关注度排行</param>
        /// <returns>List列表实体</returns>
        public List<UserInfo> GetTopUser(int number,int flag)
        {
            return dal.GetTopUser(number, flag);
        }

        /// <summary>
        /// 得到博客列表
        /// </summary>
        /// <param name="number">数量</param>
        /// <param name="flag">0热门博客，1最新博客，2推荐博客</param>
        /// <returns></returns>
        public List<BlogInfo> GetBlogList(int number, int flag)
        {
            return dal.GetBlogList(number, flag);
        }

        /// <summary>
        /// 得到新闻列表
        /// </summary>
        /// <param name="number">数量</param>
        /// <param name="flag">0热门，1最新，2推荐，3图片</param>
        /// <returns></returns>
        public List<NewsInfo> GetNewsList(int number, int flag)
        {
            return dal.GetNewsList(number, flag);
        }

        /// <summary>
        /// 得到群组列表
        /// </summary>
        /// <param name="number">数量</param>
        /// <param name="flag">0热门，1最新，2推荐，3名人机构</param>
        /// <returns></returns>
        public List<GroupInfo> GetGroupList(int number, int flag)
        {
            return dal.GetGroupList(number, flag);
        }
        /// <summary>
        /// 设置名人机构
        /// </summary>
        /// <param name="gid"></param>
        /// <param name="uid"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public int SetGroupLight(int gid, int uid, int flag)
        {
            return dal.SetGroupLight(gid, uid,flag);
        }

        ///<summary>
        /// 得到商品列表
        /// </summary>
        /// <param name="number">数量</param>
        /// <param name="flag">0热门，1最新，2推荐</param>
        /// <returns></returns>
        public List<ShopGoodsInfo> GetGoodsList(int number, int flag)
        {
            return dal.GetGoodsList(number, flag);
        }

        ///<summary>
        /// 得到店铺列表
        /// </summary>
        /// <param name="number">数量</param>
        /// <param name="flag">0热门，1最新，2推荐</param>
        /// <returns></returns>
        public List<ShopInfo> GetShopList(int number, int flag)
        {
            return dal.GetShopList(number, flag);
        }

        ///<summary>
        /// 得到活动列表
        /// </summary>
        /// <param name="number">数量</param>
        /// <param name="flag">0热门，1最新，2推荐</param>
        /// <returns></returns>
        public List<AtiveInfo> GetActiveList(int number, int flag)
        {
            return dal.GetActiveList(number, flag);
        }

        ///<summary>
        /// 得到问答列表
        /// </summary>
        /// <param name="number">数量</param>
        /// <param name="flag">0热门，1最新，2推荐</param>
        /// <returns></returns>
        public List<AskInfo> GetAskList(int number, int flag)
        {
            return dal.GetAskList(number, flag);
        }

        ///<summary>
        /// 得到投票列表
        /// </summary>
        /// <param name="number">数量</param>
        /// <param name="flag">0热门，1最新，2推荐</param>
        /// <returns></returns>
        public List<VoteInfo> GetVoteList(int number, int flag)
        {
            return dal.GetVoteList(number, flag);
        }

        ///<summary>
        /// 得到相片列表
        /// </summary>
        /// <param name="number">数量</param>
        /// <param name="flag">0热门，1最新，2推荐</param>
        /// <returns></returns>
        public List<PhotoInfo> GetPhotoList(int number, int flag)
        {
            return dal.GetPhotoList(number, flag);
        }

        /// <summary>
        /// 得到群组主题数量
        /// </summary>
        /// <param name="gid">群组ID</param>
        /// <param name="flag">0主题数，1回复数</param>
        /// <returns></returns>
        public int GetGroupCount(int gid,int flag)
        {
            return dal.GetGroupCount(gid,flag);
        }

        /// <summary>
        /// 得到微博列表
        /// </summary>
        /// <param name="number">数量</param>
        /// <param name="flag">0最新，1推荐</param>
        /// <returns></returns>
        public List<TwitterInfo> GetTwitterList(int number, int flag)
        {
            return dal.GetTwitterList(number, flag);
        }

        /// <summary>
        /// 得到相册列表
        /// </summary>
        /// <param name="number">数量</param>
        /// <param name="flag">0热门，1最新，2推荐</param>
        /// <returns></returns>
        public List<AlbumInfo> GetGAlbumList(int number, int flag)
        {
            return dal.GetGAlbumList(number, flag);
        }

        /// <summary>
        /// 得到话题列表
        /// </summary>
        /// <param name="number">数量</param>
        /// <param name="flag">0热门，1最新，2推荐</param>
        /// <returns></returns>
        public List<GroupTopicInfo> GetTopicList(int number, int flag)
        {
            return dal.GetTopicList(number, flag);
        }

        /// <summary>
        /// 得到社区分类
        /// </summary>
        /// <param name="parentid">顶级目录</param>
        /// <returns></returns>
        public List<GroupClassInfo> GetTopicClassList(int parentid)
        {
            return dal.GetTopicClassList(parentid);
        }

        /// <summary>
        /// 设置信息状态
        /// </summary>
        /// <param name="infoid">被操作的信息</param>
        /// <param name="uid">操作者</param>
        /// <param name="flag">设置值</param>
        /// <param name="type">类型</param>
        /// <returns>0失败，1成功</returns>
        public int SetAllState(int infoid, int uid, int flag, string type)
        {
            return dal.SetAllState(infoid, uid, flag, type);
        }

        /// <summary>
        /// 设置信息状态
        /// </summary>
        /// <param name="infoid">操作信息ID</param>
        /// <param name="uid">操作者</param>
        /// <param name="flag">操作类型</param>
        /// <param name="type">需要设置的值</param>
        /// <returns>0失败，1成功</returns>
        public int CheckInfoState(int infoid, int uid, int flag, string type)
        {
            return dal.CheckInfoState(infoid, uid, flag, type);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="type">初始化类型</param>
        /// <returns>0失败，1成功</returns>
        public int Start(string type)
        {
            return dal.Start(type);
        }

        /// <summary>
        /// 删除资讯分类
        /// </summary>
        /// <param name="infoid">被操作ID</param>
        /// <param name="uid">操作者</param>
        /// <returns>0失败，1成功</returns>
        public int DeleteNewsClass(int infoid, int uid)
        {
            return dal.DeleteNewsClass(infoid, uid);
        }

        /// <summary>
        /// 插入广告/更新广告
        /// </summary>
        /// <param name="info">AdsInfo实体类</param>
        /// <returns>0失败，1成功</returns>
        public int InsertAds(AdsInfo info)
        {
            return dal.InsertAds(info);
        }

        /// <summary>
        /// 得到广告信息
        /// </summary>
        /// <param name="aid">广告ID</param>
        /// <returns>AdsInfo实体类</returns>
        public AdsInfo GetAdsInfo(int aid)
        {
            return dal.GetAdsInfo(aid);
        }

        /// <summary>
        /// 删除广告
        /// </summary>
        /// <param name="infoid">广告ID</param>
        /// <param name="userid">操作者ID</param>
        /// <returns>0失败，1成功</returns>
        public int DeleteAds(int infoid, int userid)
        {
            return dal.DeleteAds(infoid, userid);
        }

        /// <summary>
        /// 得到广告列表
        /// </summary>
        /// <param name="ptype">广告位</param>
        /// <param name="number">调用数量</param>
        /// <returns>List列表类</returns>
        public List<AdsInfo> GetAdsList(string ptype,int number)
        {
            return dal.GetAdsList(ptype, number);
        }

        /// <summary>
        /// 找回密码插入数据记录
        /// </summary>
        /// <param name="info">EmailNoticeInfo实体类</param>
        /// <returns>0失败，1成功</returns>
        public int InsertEmailNotice(EmailNoticeInfo info)
        {
            return dal.InsertEmailNotice(info);
        }

        /// <summary>
        /// 得到邮件通知信息
        /// </summary>
        /// <param name="vcode">验证码</param>
        /// <returns>EmailNoticeInfo实体类</returns>
        public EmailNoticeInfo GetEmailNoticeInfo(string vcode)
        {
            return dal.GetEmailNoticeInfo(vcode);
        }

        /// <summary>
        /// 插入举报信息
        /// </summary>
        /// <param name="info">ReportInfo实体类</param>
        /// <returns>0失败，1成功</returns>
        public int InsertReport(ReportInfo info)
        {
            return dal.InsertReport(info);
        }

        /// <summary>
        /// 删除举报
        /// </summary>
        /// <param name="rid">举报ID</param>
        /// <param name="userid">操作用户</param>
        /// <returns>0失败，1成功</returns>
        public int DeleteReport(int rid, int userid)
        {
            return dal.DeleteReport(rid, userid);
        }

        /// <summary>
        /// 删除动态
        /// </summary>
        /// <param name="did"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public int DeleteDyn(int did, int userid)
        {
            return dal.DeleteDyn(did, userid);
        }

        /// <summary>
        /// 得到友情链接
        /// </summary>
        /// <param name="number"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<LinksInfo> GetLinksList(int number, int type)
        {
            return dal.GetLinksList(number, type);
        }

        /// <summary>
        /// 得到链接信息
        /// </summary>
        /// <param name="lid"></param>
        /// <returns></returns>
        public LinksInfo GetLinksInfo(int lid)
        {
            return dal.GetLinksInfo(lid);
        }
        /// <summary>
        /// 删除友情链接
        /// </summary>
        /// <param name="lid"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public int DeleteLinks(int lid,int uid)
        {
            return dal.DeleteLinks(lid,uid);
        }
        /// <summary>
        /// 更新友情链接（增加/更新）
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int InsertLinks(LinksInfo info)
        {
            return dal.InsertLinks(info);
        }

        /// <summary>
        /// 得到APIkey是否存在
        /// </summary>
        /// <param name="apikey"></param>
        /// <returns></returns>
        public bool GetApiKey(string apikey)
        {
            return dal.GetApiKey(apikey);
        }

        /// <summary>
        /// 检查程序是否已经安装
        /// </summary>
        /// <returns></returns>
        public bool ProgrameInstall()
        {
            return dal.ProgrameInstall();
        }
    }
}
