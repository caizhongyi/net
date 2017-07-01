using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;


namespace JuSNS.Web.app.group
{
    public class grouptopic : BasePage
    {
        public string groupname = Public.GetXMLValue("group", "~/config/base/menu.xml");
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref VelocityContext context)
        {
            int uid = this.GetUserID();
            base.Page_Load(ref context);
            int gid = GetInt("gid", 0);
            context.Put("groupname", groupname);
            context.Put("cpagetitle", "最新话题");
            ShowList(ref context);
        }

        protected void ShowList(ref VelocityContext context)
        {
            int uid = this.GetUserID();
            int recount = Convert.ToInt32(Public.GetXMLGroupValue("TopicPageNumber"));
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            DataTable dt = JuSNS.Home.UtilPage.GetPage("user_groupalltopic_aspx", PageIndex, recount, out ReCount, out PgCount, null);
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> infolist = new List<Hashtable>();
            int isadmin = JuSNS.Home.User.User.Instance.GetUserInfo(uid).IsAdmin;
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable info = new Hashtable();
                bool isOp = false;
                info.Add("id", dr["id"]);
                info.Add("truename", dr["truename"]);
                info.Add("headpic", this.GetHeadImage(dr["userid"], 0));
                info.Add("spaceurl", this.GetSpaceURL(dr["userid"]));
                info.Add("topicurl", Public.URLWrite(dr["id"], "topic"));
                info.Add("userid", dr["userid"]);
                info.Add("gid", dr["groupid"]);
                info.Add("title", Input.GetSubString(dr["title"].ToString(), 40));
                info.Add("titleall", dr["title"]);
                info.Add("clicks", dr["clicks"]);
                info.Add("replynumber", dr["Replynumber"]);
                info.Add("time", Public.getTimeEXPINSpan(Convert.ToDateTime(dr["PostTime"])));
                if (Input.IsDate(dr["lastposttime"].ToString()))
                {
                    info.Add("replytime", Public.getTimeEXPINSpan(Convert.ToDateTime(dr["lastposttime"])));
                }
                else
                {
                    info.Add("replytime", "&nbsp;");
                }
                string opSTR = string.Empty;
                string opSTR1 = string.Empty;
                if (Convert.ToInt32(dr["userid"]) == uid || isadmin >0)
                {
                    isOp = true;
                }
                if (isOp) opSTR += "<a href=\"javascript:;\" onclick=\"deleteAll(" + dr["id"] + "," + uid + ",'grouptopic')\" title=\"删除\" class=\"showok1\"></a>";
                info.Add("showop", opSTR);
                info.Add("groupname", Input.GetSubString(dr["GroupName"].ToString(), 16));
                info.Add("groupnameall", dr["GroupName"]);
                if (isadmin > 0)
                {
                    if (Convert.ToInt32(dr["isrec"]) == 1)
                    {
                        info.Add("showrec", "<a href=\"javascript:;\" onclick=\"RecAll(" + dr["id"] + "," + uid + ",0,'topic')\" class=\"showrec1\"></a>");
                    }
                    else
                    {
                        info.Add("showrec", "<a href=\"javascript:;\" onclick=\"RecAll(" + dr["id"] + "," + uid + ",1,'topic')\" class=\"showrec\"></a>");
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
