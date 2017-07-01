using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.app.share
{
    public class @default : BasePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            int uid = this.GetUserID();
            int isopen = Convert.ToInt32(Public.GetXMLShareValue("isopen"));
            if (isopen == 0)
            {
                if (uid == 0)
                {
                    context.Put("redirecturl", root + "/library/page/open" + ExName + "?q=false&app=share");
                }
            }
            ShowInfo(ref context);
        }


        protected void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            string q = GetString("q");
            string pagetitle = string.Empty;
            switch (q)
            {
                case "rec":
                    pagetitle = "推荐的分享";
                    break;
                case "my":
                    pagetitle = "我的分享";
                    break;
                case "friend":
                    pagetitle = "朋友的分享";
                    break;
                default:
                    pagetitle = "所有的分享";
                    break;
            }
            context.Put("q", q);
            ShowClass(ref context, q, pagetitle);
        }

        protected void ShowClass(ref VelocityContext context, string q, string pagetitle)
        {
            string listSTR = string.Empty;
            string t=GetString("t");
            for (int i = 0; i < 18; i++)
            {
                if (i.ToString() == t)
                {
                    listSTR += "<li class=\"current\"><a href=\"../share/default" + ExName + "?q=" + q + "&t=" + i + "\">" + JuSNS.Common.Share.lib.GetShareType((EnumShareType)i) + "</a></li>";
                }
                else
                {
                    listSTR += "<li><a href=\"../share/default" + ExName + "?q=" + q + "&t=" + i + "\">" + JuSNS.Common.Share.lib.GetShareType((EnumShareType)i) + "</a></li>";
                }
            }
            context.Put("classlist", listSTR);
            ShowList(ref context, q, t, pagetitle);
        }
        protected void ShowList(ref VelocityContext context, string q, string t, string pagetitle)
        {
            int uid = this.GetUserID();
            int recount = Convert.ToInt32(Public.GetXMLShareValue("PageNumber"));
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            string kwd = GetString("kwd");
            SqlConditionInfo[] st = null;
            string FriendSTR = string.Empty;
            context.Put("q", q);
            if (!string.IsNullOrEmpty(kwd))
            {
                st = new SqlConditionInfo[1];
                st[0] = new SqlConditionInfo("@kwd", kwd, TypeCode.String);
                st[0].Blur = 3;
                context.Put("cpagetitle", "搜索关于” " + kwd + "“的" + pagetitle);
                context.Put("kwd", kwd);
            }
            else
            {
                context.Put("cpagetitle", pagetitle);
            }
            if (q == "friend") FriendSTR = JuSNS.Home.User.User.Instance.GetFriendList(uid);
            DataTable dt = JuSNS.Home.UtilPage.GetSharePage(q, FriendSTR, t, uid, PageIndex, recount, out ReCount, out PgCount, st);
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> infolist = new List<Hashtable>();
            int isadmin = JuSNS.Home.User.User.Instance.GetUserInfo(uid).IsAdmin;
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable info = new Hashtable();
                bool isOp = false;
                info.Add("id", dr["id"]);
                info.Add("truename", dr["truename"]);
                info.Add("title", dr["title"]);
                string contentSTR = dr["content"].ToString();
                if ((EnumShareType)Convert.ToInt32(dr["sharetype"]) == EnumShareType.ForVote)
                {
                    contentSTR += "<br />" + GetOption(dr["infoid"], JuSNS.Home.App.Vote.Instance.GetVoteInfo(dr["infoid"]).Mode);
                }
                info.Add("content", contentSTR);
                info.Add("sharetitle", JuSNS.Common.Share.lib.GetShareType((EnumShareType)Convert.ToInt32(dr["sharetype"])));
                int infoid = Convert.ToInt32(dr["infoid"]);
                string pic = string.Empty;
                string webURL=Convert.ToString(dr["webURL"]);
                if (!string.IsNullOrEmpty(webURL))
                {
                    string tvSTR = Public.ShowPlayer(webURL);
                    if (!string.IsNullOrEmpty(tvSTR))
                    {
                        info.Add("vodie", tvSTR);
                    }
                }
                switch ((EnumShareType)Convert.ToInt32(dr["sharetype"]))
                {
                    case EnumShareType.ForWeb:
                        info.Add("webURL", webURL);
                        break;
                    case EnumShareType.ForActive:
                        AtiveInfo ainfo = JuSNS.Home.App.Ative.Instance.GetAtiveInfo(infoid);
                        pic = ainfo.Photo;
                        if (!string.IsNullOrEmpty(pic))
                        {
                            pic = Public.GetXMLAtiveValue("picpath") + "/" + pic;
                        }
                        break;
                    case EnumShareType.ForAlbum:
                        pic = this.GetSmallPic(JuSNS.Home.App.Album.Instance.CoverPath(infoid), 1);
                        break;
                    case EnumShareType.ForGoods:
                        ShopGoodsInfo sginfo = JuSNS.Home.App.Shop.Instance.GetGoodsInfo(infoid);
                        pic = sginfo.Pic;
                        if (!string.IsNullOrEmpty(pic))
                        {
                            pic = Public.GetXMLShopValue("shopPicPath") + "/" + pic;
                        } break;
                    case EnumShareType.ForGroup:
                        GroupInfo ginfo = JuSNS.Home.App.Group.Instance.GetGroupInfo(infoid);
                        pic = ginfo.Portrait;
                        if (!string.IsNullOrEmpty(pic))
                        {
                            pic = Public.GetXMLGroupValue("GroupPicPath") + "/" + pic;
                        }
                        break;
                    case EnumShareType.ForMulte:
                        ShopMulteBuyInfo minfo = JuSNS.Home.App.Shop.Instance.GetMulteBuyInfo(infoid);
                        pic = minfo.Pic;
                        if (!string.IsNullOrEmpty(pic))
                        {
                            pic = Public.GetXMLShopValue("shopPicPath") + "/" + pic;
                        }
                        break;
                    case EnumShareType.ForPhoto:
                        PhotoInfo mdl = JuSNS.Home.App.Photo.Instance.GetInfo(infoid);
                        if (mdl.PhotoType == 0)
                        {
                            pic = Public.GetOrgHeadPic(mdl.FilePath);
                        }
                        else
                        {
                            pic = Public.GetOrgPic(mdl.FilePath);
                        }
                        break;
                    case EnumShareType.ForShop:
                        ShopInfo sinfo = JuSNS.Home.App.Shop.Instance.GetShopForID(infoid);
                        pic = sinfo.Pic;
                        if (!string.IsNullOrEmpty(pic))
                        {
                            pic = Public.GetXMLShopValue("shopPicPath") + "/" + pic;
                        }
                        break;
                    case EnumShareType.ForFriend:
                        pic = this.GetHeadImage(infoid, 2);
                        break;
                }
                if (!string.IsNullOrEmpty(pic) && pic.Length > 15)
                {
                    info.Add("pic", pic);
                }
                info.Add("infoid", infoid);
                info.Add("spaceurl", this.GetSpaceURL(dr["userid"]));
                info.Add("headpic", this.GetHeadImage(dr["userid"], 2));
                info.Add("time", Public.getTimeLEXYearSpan(Convert.ToDateTime(dr["PostTime"])));
                string opSTR = string.Empty;
                if (Convert.ToInt32(dr["userid"]) == uid || isadmin >0) isOp = true;
                if (isOp) opSTR += "<a href=\"javascript:;\" onclick=\"deleteAll(" + dr["id"] + "," + uid + ",'share')\" class=\"showok1\" title=\"删除\"></a>";
                info.Add("showop", opSTR);
                infolist.Add(info);
            }
            dt.Dispose();
            context.Put("infolist", infolist);
            context.Put("recordcount", ReCount);
            context.Put("pagelist", JuSNS.MVC.Pager.PagSTR(PageIndex, PgCount, ReCount, recount));
        }

        protected string GetOption(object vid, object Mode)
        {
            StringBuilder s = new StringBuilder();
            List<VoteOptionInfo> list = JuSNS.Home.App.Vote.Instance.OptionList(Convert.ToInt32(vid));
            for (int i = 0; i < list.Count && i < 3; i++)
            {
                string o = string.Empty;
                if (Convert.ToInt32(Mode) == 1)//单选
                {
                    o = "<img src=\"" + root + "/template/" + JuSNS.Config.UiConfig.SkinStyle + "/images/radio.gif\" style=\"border:0px;\" align=\"absmiddle\" /> &nbsp;";
                    s.Append(o + Input.HtmlDecode(list[i].OptionName) + "<br />");
                }
                else//复选
                {
                    o = "<img src=\"" + root + "/template/" + JuSNS.Config.UiConfig.SkinStyle + "/images/checkbox.gif\" style=\"border:0px;\" align=\"absmiddle\" /> &nbsp;";
                    s.Append(o + Input.HtmlDecode(list[i].OptionName) + "<br />");
                }
            }
            if (list.Count > 3)
            {
                s.Append(" &nbsp; …");
            }
            return s.ToString();
        }

        public override void Page_PostBack(ref VelocityContext context)
        {
            ShareInfo mdl = new ShareInfo();
            mdl.Comments = 0;
            mdl.Content = GetString("txtcontent");
            mdl.Id = GetInt("sid", 0);
            mdl.Infoid = 0;
            byte ischeck = Convert.ToByte(Public.GetXMLShareValue("ShareCheck"));
            string passSTR = string.Empty;
            if (ischeck == 1)
            {
                passSTR = "但需要审核";
            }
            mdl.IsLock = ischeck;
            mdl.IsRec = false;
            mdl.PostIP = Public.GetClientIP();
            mdl.PostTime = DateTime.Now;
            string webURL = GetString("txtwebURL");
            if (!string.IsNullOrEmpty(webURL))
            {
                string fileExt = webURL.Substring(webURL.LastIndexOf(".")).ToLower();
                if (fileExt == ".mp3" || fileExt == ".wma")
                {
                    mdl.ShareType = (byte)EnumShareType.ForMusic;
                }
                else if (fileExt == ".swf")
                {
                    mdl.ShareType = (byte)EnumShareType.ForFlash;
                }
                else
                {
                    mdl.ShareType = (byte)EnumShareType.ForWeb;
                }
                mdl.Title = GetString("txttitle");
                mdl.UserID = this.GetUserID();
                mdl.WebURL = GetString("txtwebURL");
                int n = JuSNS.Home.App.Share.Instance.InsertShare(mdl);
                if (n > 0)
                {
                    if (ischeck == 1)
                    {
                        context.Put("rights", "分享成功，但是需要审核后才能显示！");
                    }
                    else
                    {
                        JuSNS.Home.User.User.Instance.InsertDyn(new DynInfo(0, this.GetUserID(), 0, (int)EnumDynType.CreatShare, string.Empty, DateTime.Now, n, string.Empty));
                        //更新积分
                        JuSNS.Home.User.User.Instance.UpdateInte(this.GetUserID(), Public.JSplit(12), 0, 0, "分享信息");
                        context.Put("redirecturl", "../share/default" + ExName + "?q=" + GetString("q") + "&t=" + GetString("t") + "");
                    }
                }
                else
                {
                    context.Put("errors", "分享发生错误");
                }
            }
            else
            {
                context.Put("errors", "请填写网页地址");
            }
            ShowInfo(ref context);
        }
    }
}