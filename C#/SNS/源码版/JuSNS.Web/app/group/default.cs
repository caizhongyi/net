using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using JuSNS.Common;
using JuSNS.Config;
using JuSNS.Home;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.app.group
{
    public class @default : BasePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            string q = GetString("q");
            context.Put("q", q);
            int IsLight = Convert.ToInt32(Public.GetXMLGroupValue("IsLight"));
            if (IsLight == 1)
            {
                context.Put("islight", IsLight);
            }
            int classid = GetInt("classid", 0);
            ShowList(ref context, q, classid);
            context.Put("classlist", ShowClassList(0, classid, " ", q));
        }

        protected string ShowClassList(int parentid, int classid,string tmpstr,string q)
        {
            string listSTR = string.Empty;
            List<GroupClassInfo> infolist = JuSNS.Home.App.Group.Instance.GetClassList(parentid);
            foreach (GroupClassInfo info in infolist)
            {
                if (classid == info.ID)
                {
                    listSTR += "<li class=\"current\"><a href=\"../group?classid=" + info.ID + "&q=" + q + "\">" + tmpstr + info.ClassName + "</a></li>";
                }
                else
                {
                    listSTR += "<li><a href=\"../group?classid=" + info.ID + "&q=" + q + "\">" + tmpstr + info.ClassName + "</a></li>";
                }
                listSTR += ShowClassList(info.ID, classid, " -- ", q);
            }
            return listSTR;
        }

        protected void ShowList(ref VelocityContext context, string q, int classid)
        {
            int uid = this.GetUserID();
            string title = string.Empty;
            string groupname = Public.GetXMLValue("group", "~/config/base/menu.xml");
            context.Put("groupname", groupname);
            switch (q)
            {
                case "friend":
                    title = "朋友的" + groupname;
                    break;
                case "join":
                    title = "我加入的" + groupname;
                    break;
                case "my":
                    title = "我创建的" + groupname;
                    break;
                case "rec":
                    title = "推荐的" + groupname;
                    break;
                case "light":
                    title = "名人机构";
                    break;
                default:
                    title = groupname;
                    break;
            }
            context.Put("cpagetitle", title);

            int recount = Convert.ToInt32(Public.GetXMLGroupValue("PageNumber"));
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
                context.Put("cpagetitle", "搜索 " + kwd + " 的" + groupname);
                context.Put("kwd", kwd);
            }
            if (q == "friend") FriendSTR = JuSNS.Home.User.User.Instance.GetFriendList(uid);
            if (q == "join") FriendSTR = JuSNS.Home.App.Group.Instance.GetJoinGroup(uid);
            DataTable dt = JuSNS.Home.UtilPage.GetGroupPage(q, FriendSTR, classid, uid, PageIndex, recount, out ReCount, out PgCount, st);
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> infolist = new List<Hashtable>();
            int isadmin = JuSNS.Home.User.User.Instance.GetUserInfo(uid).IsAdmin;
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable info = new Hashtable();
                bool isOp = false;
                string lightSTR = string.Empty;
                if (Convert.ToBoolean(dr["Islight"]))
                {
                    lightSTR += "<img src=\"" + root + "/template/" + UiConfig.SkinStyle + "/images/light.gif\" title=\"名人机构\" /> ";
                }
                info.Add("groupname", lightSTR + Input.GetSubString(dr["GroupName"].ToString(), 40));
                info.Add("id", dr["id"]);
                info.Add("truename", dr["truename"]);
                info.Add("spaceurl", this.GetSpaceURL(dr["userid"]));
                info.Add("groupurl", Public.URLWrite(dr["id"],"group"));
                info.Add("userid", dr["userid"]);
                info.Add("bulletin", dr["Bulletin"]);
                info.Add("classid", dr["classid"]);
                info.Add("classname", JuSNS.Home.App.Group.Instance.GetClassName(Convert.ToInt32(dr["classid"])));
                info.Add("members", dr["Members"]);
                info.Add("time", Public.getTimeLEXYearSpan(Convert.ToDateTime(dr["PostTime"])));
                info.Add("pic", Public.GetGroupPortrait(dr["Portrait"]));
                string opSTR = string.Empty;
                if (Convert.ToInt32(dr["userid"]) == uid || isadmin >0)  isOp = true;
                if (!JuSNS.Home.App.Group.Instance.IsJoinGroup(Convert.ToInt32(dr["id"]), uid))
                {
                    opSTR += "<li><a href=\"javascript:;\" onclick=\"JoinGroup(" + dr["id"] + "," + uid + ")\">加入" + groupname + "</a></li>";
                }
                else
                {
                    opSTR += "<li><a href=\"javascript:;\" onclick=\"OutGroup(" + dr["id"] + "," + uid + ")\">退出" + groupname + "</a></li>";
                }
                if (isOp)
                {
                    opSTR += "<li><a href=\"javascript:;\" onclick=\"deleteAll(" + dr["id"] + "," + uid + ",'group')\">删除" + groupname + "</a></li>";
                }
                bool isgroupadmin = JuSNS.Home.App.Group.Instance.isGroupAdmin(Convert.ToInt32(dr["id"]), uid);
                if (isgroupadmin)
                {
                    opSTR += "<li><a href=\"new" + ExName + "?gid=" + dr["id"] + "\">设置" + groupname + "</a></li>";
                }
                info.Add("showop", opSTR);
                if (isadmin > 0)
                {
                    if (Convert.ToInt32(dr["isrec"]) == 1)
                    {
                        info.Add("showrec", "<a href=\"javascript:;\" onclick=\"RecAll(" + dr["id"] + "," + uid + ",0,'group')\" class=\"showrec1\"></a>");
                    }
                    else
                    {
                        info.Add("showrec", "<a href=\"javascript:;\" onclick=\"RecAll(" + dr["id"] + "," + uid + ",1,'group')\" class=\"showrec\"></a>");
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
