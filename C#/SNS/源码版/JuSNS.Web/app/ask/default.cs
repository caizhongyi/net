using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.app.ask
{
    public class @default : BasePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            int isOpen = Convert.ToInt16(Public.GetXMLAskValue("isopen"));
            if (isOpen == 0)
            {
                context.Put("redirecturl", root + "/library/page/open" + ExName + "?q=false&app=ask");
            }
            else
            {
                ShowInfo(ref context);
            }
        }

        protected void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            string q = GetString("q");
            int classid = GetInt("classid", 0);
            string pagetitle = string.Empty;
            if (JuSNS.Common.Input.IsInteger(q))
            {
                pagetitle = JuSNS.Home.User.User.Instance.GetUserInfo(q).TrueName + "的所有问题";
            }
            else
            {
                switch (q)
                {
                    case "quick":
                        pagetitle = "紧急问题";
                        break;
                    case "all":
                        pagetitle = "所有问题";
                        break;
                    case "ok":
                        pagetitle = "已解决问题";
                        break;
                    case "my":
                        pagetitle = "我的问题";
                        break;
                    case "friend":
                        pagetitle = "朋友的问题";
                        break;
                    case "hight":
                        pagetitle = "高分悬赏问题";
                        break;
                    default:
                        pagetitle = "待解决问题";
                        break;
                }
            }
            context.Put("cpagetitle", pagetitle);
            context.Put("classlist", ShowClassList(ref context, q, classid));
            ShowList(ref context, q, classid, pagetitle);
        }

        protected string ShowClassList(ref VelocityContext context, string q, int parentid)
        {
            string list = string.Empty;
            List<AskClassInfo> infolist = JuSNS.Home.App.Ask.Instance.GetAskClass(parentid);
            if (parentid > 0)
            {
                context.Put("classname", "下级分类");
            }
            else
            {
                context.Put("classname", "按分类查看");
            }
            int n = 0;
            foreach (AskClassInfo info in infolist)
            {
                list += "<li><a href=\"../ask?q=" + q + "&classid=" + info.Id + "\">" + info.ClassName + "</a></li>";
                n++;
            }
            if (n == 0)
            {
                list += "<li>无下级分类</li>";
            }
            return list;
        }

        protected void ShowList(ref VelocityContext context, string q, int classid, string pagetitle)
        {
            int uid = this.GetUserID();
            int recount = Convert.ToInt32(Public.GetXMLAskValue("PageNumber"));
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
            if (q == "friend") FriendSTR = JuSNS.Home.User.User.Instance.GetFriendList(uid);
            DataTable dt = JuSNS.Home.UtilPage.GetAskPage(q, FriendSTR, classid, uid, PageIndex, recount, out ReCount, out PgCount, st);
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> infolist = new List<Hashtable>();
            int isadmin = JuSNS.Home.User.User.Instance.GetUserInfo(uid).IsAdmin;
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable info = new Hashtable();
                bool isOp = false;
                info.Add("id", dr["id"]);
                info.Add("truename", dr["truename"]);
                info.Add("spaceurl", this.GetSpaceURL(dr["userid"]));
                info.Add("headpic", this.GetHeadImage(dr["userid"], 1));
                info.Add("askurl", Public.URLWrite(dr["id"], "ask"));
                info.Add("userid", dr["userid"]);
                string states = string.Empty;
                int isclose = Convert.ToInt32(dr["isClose"]);
                if (isclose == 1)
                {
                    states = "<span class=\"green\">[已解决]</span>";
                }
                info.Add("state", states);
                int isjinji = Convert.ToInt32(dr["isJinji"]);
                string jistr = string.Empty;
                if (isjinji == 1)
                {
                    jistr = "<span class=\"jinji\">急</span>";
                }
                info.Add("jinji", jistr);
                if (!string.IsNullOrEmpty(dr["pic"].ToString()))
                {
                    info.Add("title", dr["title"] + "<a class=\"reshow\" title=\"有图\">[图]</a>");
                }
                else
                {
                    info.Add("title", dr["title"]);
                }
                info.Add("classid", dr["classid"]);
                info.Add("classname", dr["classname"]);
                info.Add("time", Public.getTimeLEXYearSpan(Convert.ToDateTime(dr["PostTime"])));
                string opSTR = string.Empty;
                if (Convert.ToInt32(dr["userid"]) == uid || isadmin >0) isOp = true;
                if (isOp) opSTR += "<a href=\"javascript:;\" onclick=\"deleteAll(" + dr["id"] + "," + uid + ",'ask')\" class=\"showok1\"></a>";
                info.Add("showop", opSTR);
                if (isadmin > 0)
                {
                    if (Convert.ToBoolean(dr["isrec"]))
                    {
                        info.Add("showrec", "<a href=\"javascript:;\" onclick=\"RecAll(" + dr["id"] + "," + uid + ",0,'ask')\" class=\"showrec1\"></a>");
                    }
                    else
                    {
                        info.Add("showrec", "<a href=\"javascript:;\" onclick=\"RecAll(" + dr["id"] + "," + uid + ",1,'ask')\" class=\"showrec\"></a>");
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