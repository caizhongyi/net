using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.home.box
{
    public class @default : UserPage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            context.Put("cpagetitle", "收件箱");
            ShowList(ref context);
        }

        protected void ShowList(ref VelocityContext context)
        {
            int uid = this.GetUserID();
            int recount = 20;
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            SqlConditionInfo[] st = null;
               st = new SqlConditionInfo[1];
                st[0] = new SqlConditionInfo("@UserID", this.UserID, TypeCode.Int32);
                DataTable dt = JuSNS.Home.UtilPage.GetPage("user_mailbox_aspx", PageIndex, recount, out ReCount, out PgCount, st);
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> infolist = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable info = new Hashtable();
                bool isOp = false;
                info.Add("id", dr["id"]);
                info.Add("sendid", dr["sendid"]);
                info.Add("title", dr["title"]);
                info.Add("content", Input.GetSubString(Input.FilterHTML(dr["content"]), 200));
                info.Add("contentall", dr["content"]);
                info.Add("truename", dr["truename"]);
                int isread = 0;
                if (Convert.ToBoolean(dr["isread"]))
                {
                    isread = 1;
                }
                info.Add("isread", Convert.ToBoolean(dr["isread"]) ? "" : "<span id=\"readflag_" + dr["id"] + "\"><a href=\"javascript:;\" onclick=\"readmail(" + dr["id"] + "," + isread + ")\"><span class=\"reshow\">[未读]</span></a></span>");
                info.Add("isreads", Convert.ToBoolean(dr["isread"]) ? 1 : 0);
                info.Add("spaceurl", this.GetSpaceURL(dr["sendid"]));
                info.Add("headpic", this.GetHeadImage(dr["sendid"], 2));
                info.Add("time", Public.getTimeLEXYearSpan(Convert.ToDateTime(dr["PostTime"])));
                string opSTR = string.Empty;
                if (Convert.ToInt32(dr["userid"]) == this.UserID) isOp = true;
                if (isOp) opSTR += "<a href=\"javascript:;\" onclick=\"deleteAll(" + dr["id"] + "," + uid + ",'mailbox')\" class=\"showok1\" title=\"删除\"></a>";
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
