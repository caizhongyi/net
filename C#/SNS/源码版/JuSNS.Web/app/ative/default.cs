using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.app.ative
{
    public class @default : BasePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            int isOpen = Convert.ToInt16(Public.GetXMLAtiveValue("isopen"));
            if (isOpen == 0)
            {
                context.Put("redirecturl", root + "/library/page/open" + ExName + "?q=false&app=ative");
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
                pagetitle = JuSNS.Home.User.User.Instance.GetUserInfo(q).TrueName + "的活动";
            }
            else
            {
                switch (q)
                {
                    case "rec":
                        pagetitle = "推荐活动";
                        break;
                    case "city":
                        pagetitle = "同城活动";
                        break;
                    case "friend":
                        pagetitle = "朋友的活动";
                        break;
                    case "my":
                        pagetitle = "我的的活动";
                        break;
                    case "ing":
                        pagetitle = "进行中的活动";
                        break;
                    case "ed":
                        pagetitle = "已结束的活动";
                        break;
                    default:
                        pagetitle = "所有活动";
                        break;
                }
            }
            context.Put("cpagetitle", pagetitle);
            ShowList(ref context, q, classid, pagetitle);
            context.Put("classlist", ShowClassList(ref context, q, classid));
            ShowFriendList(ref context);
        }

        protected void ShowFriendList(ref VelocityContext context)
        {
            int uid = this.GetUserID();
            int Num = 0;
            int recount = Convert.ToInt32(Public.GetXMLAtiveValue("fNumber"));
            List<AtiveInfo> Infolist = JuSNS.Home.App.Ative.Instance.GetFriendAtive(recount, JuSNS.Home.User.User.Instance.GetFriendList(uid),uid);
            List<Hashtable> friendlist = new List<Hashtable>();
            foreach (AtiveInfo info in Infolist)
            {
                Num++;
                Hashtable friend = new Hashtable();
                friend.Add("ativename", info.AtiveName);
                friend.Add("ativeurl", Public.URLWrite(info.Id, "ative"));
                friendlist.Add(friend);
            }
            context.Put("friendlist", friendlist);
            context.Put("friendcount", Num);
        }

        protected string ShowClassList(ref VelocityContext context, string q, int parentid)
        {
            string list = string.Empty;
            List<AtiveClassInfo> infolist = JuSNS.Home.App.Ative.Instance.GetAtiveClassList(parentid);
            if (parentid > 0)
            {
                context.Put("classname", "下级分类");
            }
            else
            {
                context.Put("classname", "按分类查看");
            }
            int n = 0;
            foreach (AtiveClassInfo info in infolist)
            {
                list += "<li><a href=\"../ative?q=" + q + "&classid=" + info.Id + "\">" + info.ClassName + "</a></li>";
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
            string picpath = Public.GetXMLAtiveValue("picpath");
            context.Put("ativepicpath", Public.GetXMLAtiveValue("picpath"));
            int recount = Convert.ToInt32(Public.GetXMLAtiveValue("PageNumber"));
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
            if (q == "city") FriendSTR = JuSNS.Home.User.User.Instance.GetUserInfo(uid).City.ToString();
            DataTable dt = JuSNS.Home.UtilPage.GetAtivePage(q, FriendSTR, classid, uid, PageIndex, recount, out ReCount, out PgCount, st);
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
                info.Add("headpic", this.GetHeadImage(dr["userid"], 2));
                info.Add("ativeurl", Public.URLWrite(dr["id"], "ative"));
                info.Add("userid", dr["userid"]);
                string states = string.Empty;
                DateTime ntime = DateTime.Now;
                DateTime etime = Convert.ToDateTime(dr["EndTime"]);
                if ((ntime - etime).Days > 0)
                {
                    states = "<span class=\"green\">[已结束]</span>";
                }
                info.Add("state", states);
                info.Add("photo", picpath + "/" + dr["photo"]);
                info.Add("ativename", dr["ativename"]);
                info.Add("classid", dr["classid"]);
                info.Add("classname", dr["classname"]);
                info.Add("members", dr["members"]);
                info.Add("starttime", Public.getTimeLEXYearSpan(Convert.ToDateTime(dr["starttime"])));
                info.Add("endtime", Public.getTimeLEXYearSpan(Convert.ToDateTime(dr["endtime"])));
                info.Add("time", Public.getTimeLEXYearSpan(Convert.ToDateTime(dr["PostTime"])));
                string opSTR = string.Empty;
                if (Convert.ToInt32(dr["userid"]) == uid || isadmin >0) isOp = true;
                if (isOp) opSTR += "<a href=\"javascript:;\" onclick=\"deleteAll(" + dr["id"] + "," + uid + ",'ative')\" class=\"showok1\" title=\"删除\"></a>";
                if (Convert.ToInt32(dr["userid"]) == uid)
                {
                    info.Add("showedite", true);
                }
                if (isadmin > 0)
                {
                    if (Convert.ToBoolean(dr["isrec"]))
                    {
                        info.Add("showrec", "<a href=\"javascript:;\" onclick=\"RecAll(" + dr["id"] + "," + uid + ",0,'ative')\" class=\"showrec1\"></a>");
                    }
                    else
                    {
                        info.Add("showrec", "<a href=\"javascript:;\" onclick=\"RecAll(" + dr["id"] + "," + uid + ",1,'ative')\" class=\"showrec\"></a>");
                    }
                }
                else
                {
                    info.Add("showrec", string.Empty);
                }
                info.Add("showop", opSTR);
                infolist.Add(info);
            }
            dt.Dispose();
            context.Put("infolist", infolist);
            context.Put("recordcount", ReCount);
            context.Put("pagelist", JuSNS.MVC.Pager.PagSTR(PageIndex, PgCount, ReCount, recount));
        }
    }
}
