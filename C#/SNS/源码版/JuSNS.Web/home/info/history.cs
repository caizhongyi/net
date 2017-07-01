using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.home.info
{
    public class history : UserPage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        public void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            this.historylist(ref context);
        }

        protected void historylist(ref VelocityContext context)
        {
            int uid = this.UserID;
            int recount = 20;
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            SqlConditionInfo[] st = null; DataTable dt = null;
            if (!string.IsNullOrEmpty(GetString("uf")))
            {
                st = new SqlConditionInfo[2];
                st[0] = new SqlConditionInfo("@UserID", uid, TypeCode.Int32);
                st[1] = new SqlConditionInfo("@UTF", Convert.ToInt32(GetString("uf")), TypeCode.Int32);
                dt = JuSNS.Home.UtilPage.GetPage("user_history_UT_aspx", PageIndex, recount, out ReCount, out PgCount, st);
            }
            else
            {
                st = new SqlConditionInfo[1];
                st[0] = new SqlConditionInfo("@UserID", uid, TypeCode.Int32);
                dt = JuSNS.Home.UtilPage.GetPage("user_history_aspx", PageIndex, recount, out ReCount, out PgCount, st);
            }
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> infolist = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable info = new Hashtable();
                info.Add("id", dr["id"]);
                info.Add("Content", dr["Content"]);
                info.Add("Point", dr["Point"]);
                info.Add("GPoint", dr["GPoint"]);
                info.Add("Money", dr["Money"]);
                info.Add("UTF", dr["UTF"].ToString() == "0" ? "增加" : "<span class=\"reshow\">减少</span>");
                info.Add("time", Public.getTimeLEXYearSpan(Convert.ToDateTime(dr["CreatTime"])));
                infolist.Add(info);
            }
            dt.Dispose();
            context.Put("infolist", infolist);
            context.Put("recordcount", ReCount);
            context.Put("pagelist", JuSNS.MVC.Pager.PagSTR(PageIndex, PgCount, ReCount, recount));
        }
    }
}
