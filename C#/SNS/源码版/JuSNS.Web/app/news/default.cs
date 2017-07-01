using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.app.news
{
    public class @default : BasePage
    {
        public string contentname = Public.GetXMLValue("news","~/config/base/menu.xml");
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            context.Put("contentname", contentname);
            context.Put("cpagetitle", contentname);
            context.Put("classlist", GetClassList(0,string.Empty));
            ShowList(ref context);
            //显示方式
            string list = GetString("list");
            if (string.IsNullOrEmpty(list))
            {
                int ContentShow = Convert.ToInt32(Public.GetXMLBaseValue("ContentShow"));
                if (ContentShow == 0)
                {
                    list = "list";
                }
                else
                {
                    list = "fg";
                }
            }
            switch (list)
            {
                case "fg":
                    context.Put("alist", "a-list1");
                    context.Put("afg", "a-fg");
                    break;
                default:
                    context.Put("alist", "a-list");
                    context.Put("afg", "a-fg1");
                    break;
            }
        }

        protected string GetClassList(int parentid, string TmpSTR)
        {
            string listSTR = string.Empty;
            int q = GetInt("q", 0);
            if (parentid == 0)
            {
                listSTR = "<li><a href=\"../news\">全部" + contentname + "</a></li>";
                if (q == 0)
                {
                    listSTR = "<li class=\"current\"><a href=\"../news\">全部" + contentname + "</a></li>";
                }
            }
            List<NewsChannelInfo> Infolist = JuSNS.Home.App.News.Instance.GetNewsChannel(parentid, 0);
            foreach (NewsChannelInfo info in Infolist)
            {
                if (q == info.Id)
                {
                    listSTR += "<li class=\"current\">" + TmpSTR + "<a href=\"../news?q=" + info.Id + "\">" + info.ChannelName + "</a></li>";
                }
                else
                {
                    listSTR += "<li>" + TmpSTR + "<a href=\"../news?q=" + info.Id + "\">" + info.ChannelName + "</a></li>";
                }
                listSTR += GetClassList(info.Id, " ······ ");
            }
            return listSTR;
        }

        public void ShowList(ref VelocityContext context)
        {
            int uid = this.GetUserID();
            string q = GetString("q");
            string list = GetString("list");
            int recount = Convert.ToInt32(Public.GetXMLBaseValue("contentPreNumber"));
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            string kwd = GetString("kwd");
            SqlConditionInfo[] st = null;
            context.Put("q", q);
            string ptitle = "最新" + contentname + "";
            switch (q)
            {
                case "rec":
                    ptitle = "推荐榜 - " + contentname + "";
                    break;
                case "click":
                    ptitle = "点击排行榜 - " + contentname + "";
                    break;
                case "comments":
                    ptitle = "评论排行榜 - " + contentname + "";
                    break;
                case "att":
                    ptitle = "关注排行榜 - " + contentname + "";
                    break;
                case "share":
                    ptitle = "分享排行榜 - " + contentname + "";
                    break;
                case "ram":
                    ptitle = "随便看看 - " + contentname + "";
                    break;
            }
            context.Put("cpagetitle", ptitle);
            if (!string.IsNullOrEmpty(kwd))
            {
                st = new SqlConditionInfo[1];
                st[0] = new SqlConditionInfo("@kwd", kwd, TypeCode.String);
                st[0].Blur = 3;
                context.Put("cpagetitle", "搜索 " + kwd + " 的" + contentname + "");
            }
            context.Put("kwd", kwd);
            DataTable dt = JuSNS.Home.UtilPage.GetNewsPage(q, PageIndex, recount, out ReCount, out PgCount, st);
            if (PgCount < 1) { PgCount = 1; }
            int contentNumber = Convert.ToInt32(Public.GetXMLBaseValue("ContentShowNumber"));
            List<Hashtable> infolist = new List<Hashtable>();
            int isadmin = JuSNS.Home.User.User.Instance.GetUserInfo(uid).IsAdmin;
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable info = new Hashtable();
                info.Add("id", dr["id"]);
                string title = Input.GetSubString(Input.FilterHTML(dr["title"].ToString()), 48);
                info.Add("titlefilter", title);
                if (dr["color"] != null)
                {
                    if (!string.IsNullOrEmpty(dr["color"].ToString())) title = "<font style=\"color:" + dr["color"] + "\">" + title + "</font>";
                }
                if (dr["bold"]!=null)
                {
                    if (JuSNS.Common.Input.IsInteger(dr["bold"])&&Convert.ToByte(dr["bold"]) == 1) title = "<strong>" + title + "</strong>";
                }
                if (dr["Italic"]!=null)
                {
                    if (JuSNS.Common.Input.IsInteger(dr["Italic"]) && Convert.ToByte(dr["Italic"]) == 1) title = "<em>" + title + "</em>";
                }
                info.Add("title", title);
                info.Add("click", dr["click"]);
                info.Add("classid", dr["classid"]);
                info.Add("channelname", dr["channelname"]);
                info.Add("share", dr["ShareNumber"]);
                info.Add("att", dr["attnumber"]);
                info.Add("comment", dr["Comments"]); int userid = Convert.ToInt32(dr["userid"]);
                info.Add("userid", userid);
                if (userid > 0)
                {
                    info.Add("spaceurl", this.GetSpaceURL(userid));
                    info.Add("headpic", this.GetHeadImage(userid,0));
                }
                info.Add("url", Public.URLWrite(dr["id"], "news"));
                int ContentShow = Convert.ToInt32(Public.GetXMLBaseValue("ContentShow"));
                if (list == "list")
                {
                    ContentShow = 0;
                }
                else if (list == "fg")
                {
                    ContentShow = 1;
                }
                info.Add("contentshow", ContentShow);
                info.Add("time", Public.getTimeLEXYearSpan(Convert.ToDateTime(dr["postTime"])));
                if (ContentShow == 1)
                {
                    string content = string.Empty;
                    if (!string.IsNullOrEmpty(dr["Pic"].ToString()))
                    {
                        content += "<img align=\"right\" src=\"" + root + Public.GetXMLBaseValue("ContentPath") + "/" + dr["Pic"] + "\" style=\"width:70px;height:90px;\" />";
                    }
                    content += Input.GetSubString(Input.FilterHTML(dr["content"].ToString()), contentNumber);
                    info.Add("content", content);
                }
                bool isOp = false;
                if (Convert.ToInt32(dr["userid"]) == uid || isadmin > 0)
                {
                    isOp = true;
                    info.Add("edit", string.Empty);
                }
                if (isOp)
                {
                    info.Add("showop", "<a href=\"javascript:;\" onclick=\"deletenews(" + dr["id"] + "," + uid + ")\" class=\"showok1\"></a>");
                }
                else
                {
                    info.Add("showop", string.Empty);
                }
                if (isadmin > 0)
                {
                    if (Convert.ToBoolean(dr["isrec"]))
                    {
                        info.Add("showrec", "<a href=\"javascript:;\" onclick=\"RecAll(" + dr["id"] + "," + uid + ",0,'news')\" class=\"showrec1\"></a>");
                    }
                    else
                    {
                        info.Add("showrec", "<a href=\"javascript:;\" onclick=\"RecAll(" + dr["id"] + "," + uid + ",1,'news')\" class=\"showrec\"></a>");
                    }
                }
                else
                {
                    info.Add("showrec", string.Empty);
                }
                infolist.Add(info);
            }
            dt.Dispose();
            context.Put("infolist", infolist);
            context.Put("recordcount", ReCount);
            context.Put("pagelist", JuSNS.MVC.Pager.PagSTR(PageIndex, PgCount, ReCount, recount));
        }
    }
}
