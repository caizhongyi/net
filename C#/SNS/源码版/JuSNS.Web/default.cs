using System;
using System.Web;
using System.Net;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using JuSNS.Common;
using JuSNS.Config;
using JuSNS.Home;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web
{
    /// <summary>
    /// 社区首页
    /// </summary>
    public class @default : BasePage
    {
        /// <summary>
        /// 是否开启手机注册
        /// </summary>
        public string isRegMobile = Public.GetXMLValue("isRegMobile");
        /// <summary>
        /// 是否开启验证码
        /// </summary>
        public string loginCode = Public.GetXMLValue("loginCode");
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="context"></param>
        public override void Page_Load(ref VelocityContext context)
        {
            if (!JuSNS.Home.App.Web.Instance.ProgrameInstall())
            {
                //程式還未安裝！請先安裝程式
                HttpContext.Current.Response.Redirect(root + "/install/default" + ExName);
                //PageError("程序还未安装！请先安装程序", root + "/install/default" + ExName);
            }
            string showIndexPage = Public.GetXMLValue("showIndexPage");
            if (showIndexPage == "1")
            {
                context.Put("redirecturl", "homes" + ExName);
            }
            else
            {
                ShowInfo(ref context);
            }
        }

        /// <summary>
        /// 基本信息
        /// </summary>
        /// <param name="context"></param>
        protected void ShowInfo(ref NVelocity.VelocityContext context)
        {
            base.Page_Load(ref context);
            int uid = GetUserID();
            context.Put("cpagetitle", "首页");
            if (uid == 0)
            {
                context.Put("islogin", uid);
            }
            if (loginCode == "1")
            {
                context.Put("isvcode", string.Empty);
            }
            context.Put("friendinfo", JuSNS.Home.User.User.Instance.GetNote(this.GetUserID(), 0, 0));
            context.Put("groupinfo", JuSNS.Home.User.User.Instance.GetNote(this.GetUserID(), 0, 1));
            context.Put("noteinfo", JuSNS.Home.User.User.Instance.GetNote(this.GetUserID(), 0, 2));
            context.Put("boxinfo", JuSNS.Home.User.User.Instance.GetNote(this.GetUserID(), 0, 3));
            context.Put("celinfo", JuSNS.Home.User.User.Instance.GetNote(this.GetUserID(), 0, 4));
            if (Public.GetXMLValue("IsOnline") == "1")
            {
                context.Put("useronline", JuSNS.Home.User.User.Instance.GetOnlineCount(1));
                context.Put("guestonline", JuSNS.Home.User.User.Instance.GetOnlineCount(0));
            }
            UserInfo mdl = null;
            if (uid > 0)
            {
                mdl = JuSNS.Home.User.User.Instance.GetUserInfo(uid);
                context.Put("memberleves", Public.GetMemberlevels(mdl.MemberLevels));
                context.Put("mlnumber", mdl.MemberLevels);
                if (mdl.IsVip)
                {
                    context.Put("isvip", true);
                }
                if (mdl.IsAdmin > 0)
                {
                    context.Put("isadmin", true);
                    context.Put("systempath", Public.GetXMLValue("systempath"));
                }
            }
            context.Put("announce", GetSystem(10));
            this.ShowHotBlogUserList(ref context); this.ShowHotBlogList(ref context);  this.ShowTopBlogList(ref context); this.ShowTopNewsList(ref context);
            this.ShowTopGroupList(ref context); this.ShowRecGroupList(ref context); this.ShowGroupListNew(ref context); this.ShowGroupListLig(ref context);
            this.ShowTwitterListNew(ref context); this.ShowNewsListRec(ref context); this.ShowNewsListNew(ref context); this.ShowTopUserHot(ref context);
            this.ShowTopUserNv(ref context);  this.ShowTopUserNan(ref context); this.ShowTopUserRenqi(ref context); this.ShowTopUserHuoyue(ref context);
            this.ShowTopUserJiFen(ref context); this.ShowTopUserAtt(ref context); this.ShowTopAlbumHot(ref context); this.ShowTopAlbumRec(ref context);
            this.ShowTopicHot(ref context); this.ShowTopicRec(ref context); this.ShowRecGoodsList(ref context); this.ShowRecShopList(ref context);
            this.ShowRecPhotoList(ref context); this.ShowRecActiveList(ref context); this.ShowRecAskList(ref context); this.ShowRecVoteList(ref context);
            if (Public.GetXMLPageValue("linksopen") == "1")
            {
                this.ShowLinksList0(ref context);
                this.ShowLinksList1(ref context);
                context.Put("linksopen", true);
            }
            context.Put("groupclasslist", ShowGroupClassList(0));
            context.Put("urls", GetString("urls"));
        }


        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="context"></param>
        public override void Page_PostBack(ref NVelocity.VelocityContext context)
        {
            ShowInfo(ref context);
        }
        /// <summary>
        /// 得到公告
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        protected string GetSystem(int number)
        {
            string listSTR = string.Empty;
            List<NewsInfo> infolist = JuSNS.Home.App.News.Instance.GetNewsList(number, 6, 0);
            foreach (NewsInfo info in infolist)
            {
                string title = info.Title;
                if (!string.IsNullOrEmpty(info.Color) && info.Color.Length > 3)
                {
                    title = "<span style=\"color:" + info.Color + "\">" + title + "</span>";
                }
                if (info.Bold == 1)
                {
                    title = "<strong>" + title + "</strong>";
                }
                if (info.Italic == 1)
                {
                    title = "<em>" + title + "</em>";
                }
                listSTR += "<li><a href=\"" + Public.URLWrite(info.Id, "news") + "\" target=\"_blank\">" + title + "</a></li>\r\n";
            }
            return listSTR;
        }
        /// <summary>
        /// 推荐资讯
        /// </summary>
        /// <param name="context"></param>
        protected void ShowNewsListRec(ref NVelocity.VelocityContext context)
        {
            int recount = Convert.ToInt32(Public.GetXMLPageValue("newsrecnumber"));
            List<NewsInfo> Infolist = JuSNS.Home.App.Web.Instance.GetNewsList(recount, 2);
            List<Hashtable> recnewslist = new List<Hashtable>();
            int m = 0;
            foreach (NewsInfo info in Infolist)
            {
                Hashtable recnews = new Hashtable();
                if (m == 0)
                {
                    recnews.Add("title", Input.GetSubString(info.Title,34));
                }
                else
                {
                    string title = info.Title;
                    if (!string.IsNullOrEmpty(info.Color) && info.Color.Length > 3)
                    {
                        title = "<span style=\"color:" + info.Color + "\">" + title + "</span>";
                    }
                    if (info.Bold == 1)
                    {
                        title = "<strong>" + title + "</strong>";
                    }
                    if (info.Italic == 1)
                    {
                        title = "<em>" + title + "</em>";
                    }
                    recnews.Add("title", title);
                }
                recnews.Add("cnumber", m);
                recnews.Add("content", Input.GetSubString(Input.FilterHTML(info.Content), 70));
                recnews.Add("channelname", info.ChannelName);
                recnews.Add("classid", info.ClassID);
                recnews.Add("newsurl", Public.URLWrite(info.Id, "news"));
                recnewslist.Add(recnews);
                m++;
            }
            context.Put("recnewslist", recnewslist);
        }
        /// <summary>
        /// 用户排行
        /// </summary>
        /// <param name="context"></param>
        protected void ShowTopUserHot(ref NVelocity.VelocityContext context)
        {
            int recount = Convert.ToInt32(Public.GetXMLPageValue("topusernumber"));
            List<UserInfo> Infolist = JuSNS.Home.App.Web.Instance.GetTopUser(recount, 1);
            List<Hashtable> topuserhotlist = new List<Hashtable>();
            foreach (UserInfo info in Infolist)
            {
                Hashtable topuserhot = new Hashtable();
                topuserhot.Add("userid", info.UserID);
                topuserhot.Add("truename", Input.GetSubString(info.TrueName, 8));
                topuserhot.Add("spaceurl", this.GetSpaceURL(info.UserID));
                topuserhot.Add("headpic", this.GetHeadImage(info.UserID));
                topuserhotlist.Add(topuserhot);
            }
            context.Put("topuserhotlist", topuserhotlist);
        }
        /// <summary>
        /// 用户排行（女）
        /// </summary>
        /// <param name="context"></param>
        protected void ShowTopUserNv(ref NVelocity.VelocityContext context)
        {
            int recount = Convert.ToInt32(Public.GetXMLPageValue("topusernumber"));
            List<UserInfo> Infolist = JuSNS.Home.App.Web.Instance.GetTopUser(recount,2);
            List<Hashtable> topusernvlist = new List<Hashtable>();
            foreach (UserInfo info in Infolist)
            {
                Hashtable topusernv = new Hashtable();
                topusernv.Add("userid", info.UserID);
                topusernv.Add("truename", Input.GetSubString(info.TrueName, 8));
                topusernv.Add("spaceurl", this.GetSpaceURL(info.UserID));
                topusernv.Add("headpic", this.GetHeadImage(info.UserID));
                topusernvlist.Add(topusernv);
            }
            context.Put("topusernvlist", topusernvlist);
        }
        /// <summary>
        /// 用户排行（男）
        /// </summary>
        /// <param name="context"></param>
        protected void ShowTopUserNan(ref NVelocity.VelocityContext context)
        {
            int recount = Convert.ToInt32(Public.GetXMLPageValue("topusernumber"));
            List<UserInfo> Infolist = JuSNS.Home.App.Web.Instance.GetTopUser(recount,3);
            List<Hashtable> topusernanlist = new List<Hashtable>();
            foreach (UserInfo info in Infolist)
            {
                Hashtable topusernan = new Hashtable();
                topusernan.Add("userid", info.UserID);
                topusernan.Add("truename", Input.GetSubString(info.TrueName, 8));
                topusernan.Add("spaceurl", this.GetSpaceURL(info.UserID));
                topusernan.Add("headpic", this.GetHeadImage(info.UserID));
                topusernanlist.Add(topusernan);
            }
            context.Put("topusernanlist", topusernanlist);
        }
        /// <summary>
        /// 人气排行
        /// </summary>
        /// <param name="context"></param>
        protected void ShowTopUserRenqi(ref NVelocity.VelocityContext context)
        {
            int recount = Convert.ToInt32(Public.GetXMLPageValue("topusernumber"));
            List<UserInfo> Infolist = JuSNS.Home.App.Web.Instance.GetTopUser(recount, 4);
            List<Hashtable> topuserrenqilist = new List<Hashtable>();
            foreach (UserInfo info in Infolist)
            {
                Hashtable topuserrenqi = new Hashtable();
                topuserrenqi.Add("userid", info.UserID);
                topuserrenqi.Add("truename", Input.GetSubString(info.TrueName, 8));
                topuserrenqi.Add("spaceurl", this.GetSpaceURL(info.UserID));
                topuserrenqi.Add("headpic", this.GetHeadImage(info.UserID));
                topuserrenqilist.Add(topuserrenqi);
            }
            context.Put("topuserrenqilist", topuserrenqilist);
        }
        /// <summary>
        /// 活跃用户排行
        /// </summary>
        /// <param name="context"></param>
        protected void ShowTopUserHuoyue(ref NVelocity.VelocityContext context)
        {
            int recount = Convert.ToInt32(Public.GetXMLPageValue("topusernumber"));
            List<UserInfo> Infolist = JuSNS.Home.App.Web.Instance.GetTopUser(recount, 5);
            List<Hashtable> topuserhuoyuelist = new List<Hashtable>();
            foreach (UserInfo info in Infolist)
            {
                Hashtable topuserhuoyue = new Hashtable();
                topuserhuoyue.Add("userid", info.UserID);
                topuserhuoyue.Add("truename", Input.GetSubString(info.TrueName, 8));
                topuserhuoyue.Add("spaceurl", this.GetSpaceURL(info.UserID));
                topuserhuoyue.Add("headpic", this.GetHeadImage(info.UserID));
                topuserhuoyuelist.Add(topuserhuoyue);
            }
            context.Put("topuserhuoyuelist", topuserhuoyuelist);
        }
        /// <summary>
        /// 积分用户排行
        /// </summary>
        /// <param name="context"></param>
        protected void ShowTopUserJiFen(ref NVelocity.VelocityContext context)
        {
            int recount = Convert.ToInt32(Public.GetXMLPageValue("topusernumber"));
            List<UserInfo> Infolist = JuSNS.Home.App.Web.Instance.GetTopUser(recount,6);
            List<Hashtable> topuserjifenlist = new List<Hashtable>();
            foreach (UserInfo info in Infolist)
            {
                Hashtable topuserjifen = new Hashtable();
                topuserjifen.Add("userid", info.UserID);
                topuserjifen.Add("truename", Input.GetSubString(info.TrueName, 8));
                topuserjifen.Add("spaceurl", this.GetSpaceURL(info.UserID));
                topuserjifen.Add("headpic", this.GetHeadImage(info.UserID));
                topuserjifenlist.Add(topuserjifen);
            }
            context.Put("topuserjifenlist", topuserjifenlist);
        }
        /// <summary>
        /// 关注排行
        /// </summary>
        /// <param name="context"></param>
        protected void ShowTopUserAtt(ref NVelocity.VelocityContext context)
        {
            int recount = Convert.ToInt32(Public.GetXMLPageValue("topusernumber"));
            List<UserInfo> Infolist = JuSNS.Home.App.Web.Instance.GetTopUser(recount,8);
            List<Hashtable> topuserattlist = new List<Hashtable>();
            foreach (UserInfo info in Infolist)
            {
                Hashtable topuseratt = new Hashtable();
                topuseratt.Add("userid", info.UserID);
                topuseratt.Add("truename", Input.GetSubString(info.TrueName, 8));
                topuseratt.Add("spaceurl", this.GetSpaceURL(info.UserID));
                topuseratt.Add("headpic", this.GetHeadImage(info.UserID));
                topuserattlist.Add(topuseratt);
            }
            context.Put("topuserattlist", topuserattlist);
        }
        /// <summary>
        /// 最新新闻
        /// </summary>
        /// <param name="context"></param>
        protected void ShowNewsListNew(ref NVelocity.VelocityContext context)
        {
            int recount = Convert.ToInt32(Public.GetXMLPageValue("newsnewnumber"));
            List<NewsInfo> Infolist = JuSNS.Home.App.Web.Instance.GetNewsList(recount,1);
            List<Hashtable> newnewslist = new List<Hashtable>();
            foreach (NewsInfo info in Infolist)
            {
                Hashtable newnews = new Hashtable();
                string title = Input.GetSubString(info.Title, 42);
                if (!string.IsNullOrEmpty(info.Color) && info.Color.Length > 3)
                {
                    title = "<span style=\"color:" + info.Color + "\">" + title + "</span>";
                }
                if (info.Bold == 1)
                {
                    title = "<strong>" + title + "</strong>";
                }
                if (info.Italic == 1)
                {
                    title = "<em>" + title + "</em>";
                }
                newnews.Add("title", title);
                newnews.Add("channelname", info.ChannelName);
                newnews.Add("classid", info.ClassID);
                newnews.Add("newsurl", Public.URLWrite(info.Id, "news"));
                newnewslist.Add(newnews);
            }
            context.Put("newnewslist", newnewslist);
        }
        /// <summary>
        /// 博客用户列表
        /// </summary>
        /// <param name="context"></param>
        protected void ShowHotBlogUserList(ref VelocityContext context)
        {
            int recount = Convert.ToInt32(Public.GetXMLPageValue("hotblogusernumber"));
            List<UserInfo> Infolist = JuSNS.Home.App.Web.Instance.GetTopUser(recount, 0);
            List<Hashtable> bloguserlist = new List<Hashtable>();
            foreach (UserInfo info in Infolist)
            {
                Hashtable bloguser = new Hashtable();
                bloguser.Add("userid", info.UserID);
                bloguser.Add("truename", info.TrueName);
                bloguser.Add("spaceurl", this.GetSpaceURL(info.UserID));
                bloguser.Add("headpic", this.GetHeadImage(info.UserID));
                bloguserlist.Add(bloguser);
            }
            context.Put("bloguserlist", bloguserlist);
        }
        /// <summary>
        /// 博客列表（热点）
        /// </summary>
        /// <param name="context"></param>
        protected void ShowHotBlogList(ref VelocityContext context)
        {
            int recount = Convert.ToInt32(Public.GetXMLPageValue("hotblognumber"));
            List<BlogInfo> Infolist = JuSNS.Home.App.Web.Instance.GetBlogList(recount, 2);
            List<Hashtable> bloghotlist = new List<Hashtable>();
            foreach (BlogInfo info in Infolist)
            {
                Hashtable bloghot = new Hashtable();
                bloghot.Add("userid", info.UserID);
                bloghot.Add("title", Input.GetSubString(info.Title, 44));
                bloghot.Add("titleall", info.Title);
                bloghot.Add("blogurl", Public.URLWrite(info.ID, "blog"));
                bloghotlist.Add(bloghot);
            }
            context.Put("bloghotlist", bloghotlist);
        }
        /// <summary>
        /// 博客排行
        /// </summary>
        /// <param name="context"></param>
        protected void ShowTopBlogList(ref VelocityContext context)
        {
            int recount = Convert.ToInt32(Public.GetXMLPageValue("topNumber"));
            List<BlogInfo> Infolist = JuSNS.Home.App.Web.Instance.GetBlogList(recount, 0);
            List<Hashtable> topbloglist = new List<Hashtable>();
            foreach (BlogInfo info in Infolist)
            {
                Hashtable topblog = new Hashtable();
                topblog.Add("userid", info.UserID);
                topblog.Add("title", Input.GetSubString(info.Title, 42));
                topblog.Add("blogurl", Public.URLWrite(info.ID, "blog"));
                topbloglist.Add(topblog);
            }
            context.Put("topbloglist", topbloglist);
        }
        /// <summary>
        /// 新闻排行
        /// </summary>
        /// <param name="context"></param>
        protected void ShowTopNewsList(ref VelocityContext context)
        {
            int recount = Convert.ToInt32(Public.GetXMLPageValue("topNumber"));
            List<NewsInfo> Infolist = JuSNS.Home.App.Web.Instance.GetNewsList(recount, 0);
            List<Hashtable> topnewslist = new List<Hashtable>();
            foreach (NewsInfo info in Infolist)
            {
                Hashtable topnews = new Hashtable();
                topnews.Add("userid", info.UserID);
                string title = Input.GetSubString(info.Title, 42);
                if (!string.IsNullOrEmpty(info.Color) && info.Color.Length > 3)
                {
                    title = "<span style=\"color:" + info.Color + "\">" + title + "</span>";
                }
                if (info.Bold == 1)
                {
                    title = "<strong>" + title + "</strong>";
                }
                if (info.Italic == 1)
                {
                    title = "<em>" + title + "</em>";
                }
                topnews.Add("title", title);
                topnews.Add("newsurl", Public.URLWrite(info.Id, "news"));
                topnewslist.Add(topnews);
            }
            context.Put("topnewslist", topnewslist);
        }
        /// <summary>
        /// 社群排行
        /// </summary>
        /// <param name="context"></param>
        protected void ShowTopGroupList(ref VelocityContext context)
        {
            int recount = Convert.ToInt32(Public.GetXMLPageValue("topNumber"));
            List<GroupInfo> Infolist = JuSNS.Home.App.Web.Instance.GetGroupList(recount, 0);
            List<Hashtable> topgrouplist = new List<Hashtable>();
            foreach (GroupInfo info in Infolist)
            {
                Hashtable topgroup = new Hashtable();
                topgroup.Add("userid", info.UserID);
                topgroup.Add("members", info.Members);
                topgroup.Add("groupname", Input.GetSubString(info.GroupName, 38));
                topgroup.Add("groupurl", Public.URLWrite(info.Id, "group"));
                topgrouplist.Add(topgroup);
            }
            context.Put("topgrouplist", topgrouplist);
        }
        /// <summary>
        /// 推荐社群排行
        /// </summary>
        /// <param name="context"></param>
        protected void ShowRecGroupList(ref VelocityContext context)
        {
            int recount = Convert.ToInt32(Public.GetXMLPageValue("recgroup"));
            List<GroupInfo> Infolist = JuSNS.Home.App.Web.Instance.GetGroupList(recount, 2);
            List<Hashtable> recgrouplist = new List<Hashtable>();
            foreach (GroupInfo info in Infolist)
            {
                Hashtable recgroup = new Hashtable();
                recgroup.Add("userid", info.UserID);
                recgroup.Add("members", info.Members);
                recgroup.Add("topiccount", JuSNS.Home.App.Web.Instance.GetGroupCount(info.Id, 0));
                recgroup.Add("replaycount", JuSNS.Home.App.Web.Instance.GetGroupCount(info.Id, 1));
                recgroup.Add("headpic", Public.GetXMLGroupValue("GroupPicPath") + "/" + info.Portrait);
                recgroup.Add("groupname", Input.GetSubString(info.GroupName, 18));
                recgroup.Add("groupurl", Public.URLWrite(info.Id, "group"));
                recgrouplist.Add(recgroup);
            }
            context.Put("recgrouplist", recgrouplist);
        }
        /// <summary>
        /// 推荐商品排行
        /// </summary>
        /// <param name="context"></param>
        protected void ShowRecGoodsList(ref VelocityContext context)
        {
            int recount = Convert.ToInt32(Public.GetXMLPageValue("recgoods"));
            List<ShopGoodsInfo> Infolist = JuSNS.Home.App.Web.Instance.GetGoodsList(recount, 2);
            List<Hashtable> recgoodslist = new List<Hashtable>();
            foreach (ShopGoodsInfo info in Infolist)
            {
                Hashtable recgoods = new Hashtable();
                recgoods.Add("spaceurl", this.GetSpaceURL(info.UserID));
                recgoods.Add("truename", info.TrueName);
                recgoods.Add("mprice", info.MPrice.ToString("0.00"));
                recgoods.Add("photo", Public.GetXMLShopValue("shopPicPath") + "/" + info.Pic);
                recgoods.Add("goodsname", Input.GetSubString(info.GoodsName, 18));
                recgoods.Add("goodsurl", Public.URLWrite(info.Id, "goods"));
                recgoodslist.Add(recgoods);
            }
            context.Put("recgoodslist", recgoodslist);
        }
        /// <summary>
        /// 推荐店铺排行
        /// </summary>
        /// <param name="context"></param>
        protected void ShowRecShopList(ref VelocityContext context)
        {
            int recount = Convert.ToInt32(Public.GetXMLPageValue("recshop"));
            List<ShopInfo> Infolist = JuSNS.Home.App.Web.Instance.GetShopList(recount, 2);
            List<Hashtable> recshoplist = new List<Hashtable>();
            foreach (ShopInfo info in Infolist)
            {
                Hashtable recshop = new Hashtable();
                recshop.Add("spaceurl", this.GetSpaceURL(info.UserID));
                recshop.Add("truename", info.TrueName);
                recshop.Add("photo", Public.GetXMLShopValue("shopPicPath") + "/" + info.Pic);
                recshop.Add("shopname", Input.GetSubString(info.ShopName, 18));
                recshop.Add("shopurl", Public.URLWrite(info.Id, "shop"));
                recshoplist.Add(recshop);
            }
            context.Put("recshoplist", recshoplist);
        }
        /// <summary>
        /// 推荐图片排行
        /// </summary>
        /// <param name="context"></param>
        protected void ShowRecPhotoList(ref VelocityContext context)
        {
            int recount = Convert.ToInt32(Public.GetXMLPageValue("recphoto"));
            List<PhotoInfo> Infolist = JuSNS.Home.App.Web.Instance.GetPhotoList(recount, 2);
            List<Hashtable> recphotolist = new List<Hashtable>();
            foreach (PhotoInfo info in Infolist)
            {
                Hashtable recphoto = new Hashtable();
                recphoto.Add("spaceurl", this.GetSpaceURL(info.UserID));
                recphoto.Add("truename", info.TrueName);
                recphoto.Add("photo", this.GetSmallPic(info.FilePath, 1));
                recphoto.Add("photourl", Public.URLWrite(info.Id, "photo"));
                recphotolist.Add(recphoto);
            }
            context.Put("recphotolist", recphotolist);
        }
        /// <summary>
        /// 推荐活动排行
        /// </summary>
        /// <param name="context"></param>
        protected void ShowRecActiveList(ref VelocityContext context)
        {
            int recount = Convert.ToInt32(Public.GetXMLPageValue("recactive"));
            List<AtiveInfo> Infolist = JuSNS.Home.App.Web.Instance.GetActiveList(recount, 2);
            List<Hashtable> recactivelist = new List<Hashtable>();
            foreach (AtiveInfo info in Infolist)
            {
                Hashtable recactive = new Hashtable();
                recactive.Add("spaceurl", this.GetSpaceURL(info.UserID));
                recactive.Add("truename", info.TrueName);
                recactive.Add("activeurl", Public.URLWrite(info.Id, "ative"));
                recactive.Add("starttime", info.StartTime.ToString("yyyy年MM月dd日"));
                recactive.Add("endtime", info.EndTime.ToString("yyyy年MM月dd日"));
                recactive.Add("members", info.Members);
                recactive.Add("activename", info.AtiveName);
                recactivelist.Add(recactive);
            }
            context.Put("recactivelist", recactivelist);
        }
        /// <summary>
        /// 推荐问答排行
        /// </summary>
        /// <param name="context"></param>
        protected void ShowRecAskList(ref VelocityContext context)
        {
            int recount = Convert.ToInt32(Public.GetXMLPageValue("recask"));
            List<AskInfo> Infolist = JuSNS.Home.App.Web.Instance.GetAskList(recount, 2);
            List<Hashtable> recasklist = new List<Hashtable>();
            foreach (AskInfo info in Infolist)
            {
                Hashtable recask = new Hashtable();
                recask.Add("spaceurl", this.GetSpaceURL(info.UserID));
                recask.Add("truename", info.TrueName);
                recask.Add("askurl", Public.URLWrite(info.Id, "ask"));
                recask.Add("jifen", info.JiFen);
                recask.Add("isjinji", info.IsJinji == 1 ? "紧急" : "普通");
                recask.Add("title", info.Title);
                recasklist.Add(recask);
            }
            context.Put("recasklist", recasklist);
        }
        /// <summary>
        /// 推荐投票排行
        /// </summary>
        /// <param name="context"></param>
        protected void ShowRecVoteList(ref VelocityContext context)
        {
            int recount = Convert.ToInt32(Public.GetXMLPageValue("recvote"));
            List<VoteInfo> Infolist = JuSNS.Home.App.Web.Instance.GetVoteList(recount, 2);
            List<Hashtable> recvotelist = new List<Hashtable>();
            foreach (VoteInfo info in Infolist)
            {
                Hashtable recvote = new Hashtable();
                recvote.Add("spaceurl", this.GetSpaceURL(info.UserID));
                recvote.Add("truename", info.TrueName);
                recvote.Add("voteurl", Public.URLWrite(info.Id, "vote"));
                recvote.Add("time", Public.getTimeEXTSpan(info.PostTime));
                recvote.Add("mode", info.Mode == 1 ? "单选" : "多选");
                recvote.Add("title", info.Title);
                recvotelist.Add(recvote);
            }
            context.Put("recvotelist", recvotelist);
        }

        protected void ShowLinksList0(ref VelocityContext context)
        {
            int recount = Convert.ToInt32(Public.GetXMLPageValue("linksNumber"));
            List<LinksInfo> Infolist = JuSNS.Home.App.Web.Instance.GetLinksList(recount, 0);
            List<Hashtable> links0list = new List<Hashtable>();
            foreach (LinksInfo info in Infolist)
            {
                Hashtable links0 = new Hashtable();
                links0.Add("url", info.URL);
                links0.Add("linkname", info.LinkName);
                links0list.Add(links0);
            }
            context.Put("links0list", links0list);
        }

        protected void ShowLinksList1(ref VelocityContext context)
        {
            int recount = Convert.ToInt32(Public.GetXMLPageValue("linksNumber"));
            List<LinksInfo> Infolist = JuSNS.Home.App.Web.Instance.GetLinksList(recount, 1);
            List<Hashtable> links1list = new List<Hashtable>();
            string linkpath = Public.GetXMLPageValue("linkspath");
            foreach (LinksInfo info in Infolist)
            {
                Hashtable links1 = new Hashtable();
                links1.Add("url", info.URL);
                links1.Add("pic", root + linkpath + "/" + info.Pic);
                links1.Add("linkname", info.LinkName);
                links1list.Add(links1);
            }
            context.Put("links1list", links1list);
        }

        /// <summary>
        /// 最新群组排行
        /// </summary>
        /// <param name="context"></param>
        protected void ShowGroupListNew(ref VelocityContext context)
        {
            int recount = Convert.ToInt32(Public.GetXMLPageValue("groupdynumber"));
            List<GroupInfo> Infolist = JuSNS.Home.App.Web.Instance.GetGroupList(recount,1);
            List<Hashtable> groupcountlist2 = new List<Hashtable>();
            foreach (GroupInfo info in Infolist)
            {
                Hashtable groupcount2 = new Hashtable();
                groupcount2.Add("userid", info.UserID);
                groupcount2.Add("members", info.Members);
                groupcount2.Add("topiccount", JuSNS.Home.App.Web.Instance.GetGroupCount(info.Id, 0));
                groupcount2.Add("replaycount", JuSNS.Home.App.Web.Instance.GetGroupCount(info.Id, 1));
                groupcount2.Add("headpic", root + Public.GetXMLGroupValue("GroupPicPath") + "/" + info.Portrait);
                groupcount2.Add("groupname", Input.GetSubString(info.GroupName, 38));
                groupcount2.Add("groupurl", Public.URLWrite(info.Id, "group"));
                groupcountlist2.Add(groupcount2);
            }
            context.Put("groupcountlist2", groupcountlist2);
        }
        /// <summary>
        /// 名人机构排行
        /// </summary>
        /// <param name="context"></param>
        protected void ShowGroupListLig(ref VelocityContext context)
        {
            int recount = Convert.ToInt32(Public.GetXMLPageValue("groupdynumber"));
            List<GroupInfo> Infolist = JuSNS.Home.App.Web.Instance.GetGroupList(recount,3);
            List<Hashtable> groupcountlist3 = new List<Hashtable>();
            foreach (GroupInfo info in Infolist)
            {
                Hashtable groupcount3 = new Hashtable();
                groupcount3.Add("userid", info.UserID);
                groupcount3.Add("members", info.Members);
                groupcount3.Add("topiccount", JuSNS.Home.App.Web.Instance.GetGroupCount(info.Id, 0));
                groupcount3.Add("replaycount", JuSNS.Home.App.Web.Instance.GetGroupCount(info.Id, 1));
                groupcount3.Add("headpic", root + Public.GetXMLGroupValue("GroupPicPath") + "/" + info.Portrait);
                groupcount3.Add("groupname", Input.GetSubString(info.GroupName, 38));
                groupcount3.Add("groupurl", Public.URLWrite(info.Id, "group"));
                groupcountlist3.Add(groupcount3);
            }
            context.Put("groupcountlist3", groupcountlist3);
        }
        /// <summary>
        /// 微博动态
        /// </summary>
        /// <param name="context"></param>
        protected void ShowTwitterListNew(ref VelocityContext context)
        {
            int recount = Convert.ToInt32(Public.GetXMLPageValue("twitternumber"));
            List<TwitterInfo> Infolist = JuSNS.Home.App.Web.Instance.GetTwitterList(recount, 0);
            List<Hashtable> twitterlist = new List<Hashtable>();
            foreach (TwitterInfo info in Infolist)
            {
                Hashtable twitter = new Hashtable();
                twitter.Add("spaceurl", this.GetSpaceURL(info.UserID));
                twitter.Add("content", Input.ReplaceSmaile(Input.FilterHTML(info.Content)));
                twitter.Add("truename", info.TrueName);
                twitter.Add("userid", info.UserID);
                twitter.Add("twitterurl", Public.URLWrite(info.ID,"twitter"));
                twitter.Add("headpic", this.GetHeadImage(info.UserID, 0));
                twitterlist.Add(twitter);
            }
            context.Put("twitterlist", twitterlist);
        }
        /// <summary>
        /// 热门相册排行
        /// </summary>
        /// <param name="context"></param>
        protected void ShowTopAlbumHot(ref VelocityContext context)
        {
            int recount = Convert.ToInt32(Public.GetXMLPageValue("albumnumber"));
            List<AlbumInfo> Infolist = JuSNS.Home.App.Web.Instance.GetGAlbumList(recount, 0);
            List<Hashtable> albumlist = new List<Hashtable>();
            foreach (AlbumInfo info in Infolist)
            {
                Hashtable album = new Hashtable();
                album.Add("albumurl", Public.URLWrite(info.AlbumID,"album"));
                album.Add("imgurl", this.GetSmallPic(JuSNS.Home.App.Album.Instance.CoverPath(info.AlbumID), 1));
                album.Add("title", info.Title);
                albumlist.Add(album);
            }
            context.Put("albumlist", albumlist);
        }
        /// <summary>
        /// 相册推荐排行
        /// </summary>
        /// <param name="context"></param>
        protected void ShowTopAlbumRec(ref VelocityContext context)
        {
            int recount = Convert.ToInt32(Public.GetXMLPageValue("albumnumber"));
            List<AlbumInfo> Infolist = JuSNS.Home.App.Web.Instance.GetGAlbumList(recount, 2);
            List<Hashtable> album1list = new List<Hashtable>();
            foreach (AlbumInfo info in Infolist)
            {
                Hashtable album1 = new Hashtable();
                album1.Add("albumurl", Public.URLWrite(info.AlbumID, "album"));
                album1.Add("imgurl", this.GetSmallPic(JuSNS.Home.App.Album.Instance.CoverPath(info.AlbumID), 1));
                album1.Add("title", info.Title);
                album1list.Add(album1);
            }
            context.Put("album1list", album1list);
        }
        /// <summary>
        /// 热门话题
        /// </summary>
        /// <param name="context"></param>
        protected void ShowTopicHot(ref VelocityContext context)
        {
            int recount = Convert.ToInt32(Public.GetXMLPageValue("showtopicnumber"));
            int m = 0;
            List<GroupTopicInfo> Infolist = JuSNS.Home.App.Web.Instance.GetTopicList(recount, 0);
            List<Hashtable> topichotlist = new List<Hashtable>();
            foreach (GroupTopicInfo info in Infolist)
            {
                Hashtable topichot = new Hashtable();
                topichot.Add("m", m);
                topichot.Add("title", info.Title);
                topichot.Add("content", Input.GetSubString(Input.FilterHTML(info.Content), 90));
                topichot.Add("spaceurl", this.GetSpaceURL(info.UserID));
                topichot.Add("headpic", this.GetHeadImage(info.UserID, 2));
                topichot.Add("truename", info.TrueName);
                topichot.Add("topicurl", Public.URLWrite(info.Id,"topic"));
                topichotlist.Add(topichot);
                m++;
            }
            context.Put("topichotlist", topichotlist);
        }
        /// <summary>
        ///推荐话题
        /// </summary>
        /// <param name="context"></param>
        protected void ShowTopicRec(ref VelocityContext context)
        {
            int recount = Convert.ToInt32(Public.GetXMLPageValue("showtopicnumber"));
            int m = 0;
            List<GroupTopicInfo> Infolist = JuSNS.Home.App.Web.Instance.GetTopicList(recount,2);
            List<Hashtable> topicreclist = new List<Hashtable>();
            foreach (GroupTopicInfo info in Infolist)
            {
                Hashtable topicrec = new Hashtable();
                topicrec.Add("m", m);
                topicrec.Add("title", info.Title);
                topicrec.Add("content", Input.GetSubString(Input.FilterHTML(info.Content), 90));
                topicrec.Add("spaceurl", this.GetSpaceURL(info.UserID));
                topicrec.Add("headpic", this.GetHeadImage(info.UserID, 2));
                topicrec.Add("truename", info.TrueName);
                topicrec.Add("topicurl", Public.URLWrite(info.Id, "topic"));
                topicreclist.Add(topicrec);
                m++;
            }
            context.Put("topicreclist", topicreclist);
        }
        /// <summary>
        /// 群组分类
        /// </summary>
        /// <param name="parentid"></param>
        /// <returns></returns>
        protected string  ShowGroupClassList(int parentid)
        {
            string listSTR = string.Empty;
            List<GroupClassInfo> Infolist = JuSNS.Home.App.Web.Instance.GetTopicClassList(parentid);
            int m = 1;
            foreach (GroupClassInfo info in Infolist)
            {
                if (parentid == 0)
                {
                    listSTR += "<li><div class=\"title\"><a href=\"" + root + "/app/group?classid=" + info.ID + "\">" + info.ClassName + "</a></div>";
                    listSTR += ShowGroupClassList(info.ID);
                    listSTR += "</li>";
                }
                else
                {
                    if (m == Infolist.Count)
                    {
                        listSTR += "<a href=\"" + root + "/app/group?classid=" + info.ID + "\">" + info.ClassName + "</a>";
                    }
                    else
                    {
                        listSTR += "<a href=\"" + root + "/app/group?classid=" + info.ID + "\">" + info.ClassName + "</a> | ";
                    }
                }
                m++;
            }
            return listSTR;
        }

        
    }
}
