using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.app.favorite
{
    public class @default: BasePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }


        protected void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            string q = GetString("q");
            int classid = GetInt("classid", 0);
            context.Put("q", q);
            string pagetitle = string.Empty;
            if (Input.IsInteger(q))
            {
                pagetitle = JuSNS.Home.User.User.Instance.GetUserInfo(q).TrueName + "的收藏";
            }
            else
            {
                switch (q)
                {
                    case "my":
                        pagetitle = "我的收藏";
                        break;
                    case "friend":
                        pagetitle = "朋友的收藏";
                        break;
                    default:
                        pagetitle = "所有人的收藏";
                        break;
                }
            }
            ShowList(ref context, q, pagetitle, classid);
            context.Put("classlist", ShowClassList(ref context, q, classid));
        }

        protected string ShowClassList(ref VelocityContext context, string q, int classid)
        {
            string list = string.Empty;
            List<FavoriteClassInfo> infolist = JuSNS.Home.User.User.Instance.GetFavorList(this.GetUserID());
            foreach (FavoriteClassInfo info in infolist)
            {
                if (classid == info.Id)
                {
                    list += "<li class=\"current\"  id=\"param1_"+info.Id+"\"><a href=\"../favorite?q=my&classid=" + info.Id + "\">" + info.ClassName + "</a><a href=\"javascript:;\" onclick=\"deleteAll(" + info.Id + "," + this.GetUserID() + ",'favoriteclass')\" class=\"showok1\" title=\"删除\"></a></li>";
                }
                else
                {
                    list += "<li id=\"param1_" + info.Id + "\"><a href=\"../favorite?q=my&classid=" + info.Id + "\">" + info.ClassName + "</a><a href=\"javascript:;\" onclick=\"deleteAll(" + info.Id + "," + this.GetUserID() + ",'favoriteclass')\" class=\"showok1\" title=\"删除\"></a></li>";
                }
            }
            return list;
        }

        protected void ShowList(ref VelocityContext context, string q, string pagetitle, int classid)
        {
            int uid = this.GetUserID();
            int recount = Convert.ToInt32(Public.GetXMLVoteValue("PageNumber"));
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
            DataTable dt = JuSNS.Home.UtilPage.GetFavoritePage(q, classid, FriendSTR, uid, PageIndex, recount, out ReCount, out PgCount, st);
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> infolist = new List<Hashtable>();
            int isadmin = JuSNS.Home.User.User.Instance.GetUserInfo(uid).IsAdmin;
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable info = new Hashtable();
                bool isOp = false;
                info.Add("id", dr["id"]);
                info.Add("userid", dr["userid"]);
                info.Add("title", dr["title"]);
                info.Add("url", dr["url"]);
                info.Add("content", dr["content"]);
                info.Add("truename", dr["truename"]);
                info.Add("spaceurl", this.GetSpaceURL(dr["userid"]));
                info.Add("headpic", this.GetHeadImage(dr["userid"], 2));
                info.Add("time", Public.getTimeLEXYearSpan(Convert.ToDateTime(dr["PostTime"])));
                string opSTR = string.Empty;
                if (Convert.ToInt32(dr["userid"]) == uid || isadmin >0) isOp = true;
                if (isOp) opSTR += "<a href=\"javascript:;\" onclick=\"deleteAll(" + dr["id"] + "," + uid + ",'favorite')\" class=\"showok1\" title=\"删除\"></a>";
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
