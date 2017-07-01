using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.help
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
            context.Put("cpagetitle", "帮助中心");
            ShowList(ref context);
        }

        protected void ShowList(ref VelocityContext context)
        {
            int uid = this.GetUserID();
            int recount = 20;
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            SqlConditionInfo[] st = null; DataTable dt = null;
            string keys = GetString("kwd");
            if (!string.IsNullOrEmpty(keys))
            {
                st = new SqlConditionInfo[1];
                st[0] = new SqlConditionInfo("@Key", keys, TypeCode.String);
                st[0].Blur = 3;
                dt = JuSNS.Home.UtilPage.GetPage("user_helpLike_aspx", PageIndex, recount, out ReCount, out PgCount, st);
            }
            else
            {
                dt = JuSNS.Home.UtilPage.GetPage("user_help_aspx", PageIndex, recount, out ReCount, out PgCount, null);
            }
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> infolist = new List<Hashtable>();
            int isadmin = JuSNS.Home.User.User.Instance.GetUserInfo(uid).IsAdmin;
            if (isadmin > 0)
            {
                context.Put("isadmin", true);
            }
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable info = new Hashtable();
                bool isOp = false;
                info.Add("id", dr["id"]);
                info.Add("title", dr["title"]);
                info.Add("helpid", dr["helpid"]);
                string opSTR = string.Empty;
                if (isadmin > 0) isOp = true;
                if (isOp)
                {
                    opSTR += "<a href=\"javascript:;\" onclick=\"deleteAll(" + dr["id"] + "," + uid + ",'help')\" class=\"showok1\" title=\"删除\"></a>";
                    info.Add("showedit", "<a href=\"../help/new"+ExName+"?hid=" + dr["id"] + "\" class=\"edit1\" title=\"编辑\"></a>");
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
