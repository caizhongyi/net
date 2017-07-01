using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.app.calend
{
    public class my : UserPage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            context.Put("cpagetitle", "日历管理");
            ShowList(ref context);
        }

        protected void ShowList(ref VelocityContext context)
        {
            int uid = this.UserID;
            int recount = 20;
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            SqlConditionInfo[] st = null; DataTable dt = null;
            st = new SqlConditionInfo[1];
            st[0] = new SqlConditionInfo("@UserID", uid, TypeCode.Int32);
            dt = JuSNS.Home.UtilPage.GetPage("user_calend_aspx", PageIndex, recount, out ReCount, out PgCount, st);
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> infolist = new List<Hashtable>();
            int isadmin = JuSNS.Home.User.User.Instance.GetUserInfo(uid).IsAdmin;
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable info = new Hashtable();
                bool isOp = false;
                info.Add("id", dr["id"]);
                info.Add("title", dr["title"]);
                info.Add("starttime", Convert.ToDateTime(dr["starttime"]).ToString("yyyy-MM-dd HH:mm"));
                info.Add("endtime", Convert.ToDateTime(dr["endtime"]).ToString("yyyy-MM-dd HH:mm"));
                info.Add("tixingtime", Convert.ToDateTime(dr["starttime"]).AddDays(-Convert.ToInt32(dr["NoteNumber"])).ToString("yyyy-MM-dd HH:mm"));
                info.Add("content", Input.GetSubString(Input.FilterHTML(dr["content"].ToString()),46));
                info.Add("contentall", Input.FilterHTML(dr["content"].ToString()));
                info.Add("time", Public.getTimeLEXYearSpan(Convert.ToDateTime(dr["PostTime"])));
                string opSTR = string.Empty;
                if (isadmin >0) isOp = true;
                if (isOp || Convert.ToInt32(dr["UserID"]) == uid) opSTR += "<a href=\"javascript:;\" onclick=\"deleteAll(" + dr["id"] + "," + uid + ",'calend')\" class=\"showok1\" title=\"删除\"></a>";
                if (Convert.ToInt32(dr["UserID"]) == uid)
                {
                    info.Add("showedit", "<a href=\"../calend?cid=" + dr["id"] + "\" class=\"edit1\" title=\"编辑\"></a>");
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
