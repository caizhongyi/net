using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.system
{
    public class gift : ManagePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            context.Put("cpagetitle", "礼物管理");
            int classid = GetInt("classid", 0);
            ShowList(ref context, classid);
        }

        protected void ShowList(ref VelocityContext context, int classid)
        {
            int uid = this.UserID;
            int recount = 20;
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            SqlConditionInfo[] st = null; DataTable dt = null;
            if (classid > 0)
            {
                st = new SqlConditionInfo[1];
                st[0] = new SqlConditionInfo("@ClassID", classid, TypeCode.Int32);
                dt = JuSNS.Home.UtilPage.GetPage("user_giftclass1_aspx", PageIndex, recount, out ReCount, out PgCount, st);
            }
            else
            {
                dt = JuSNS.Home.UtilPage.GetPage("user_giftall1_aspx", PageIndex, recount, out ReCount, out PgCount, null);
            }
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> infolist = new List<Hashtable>();
            int isadmin = JuSNS.Home.User.User.Instance.GetUserInfo(uid).IsAdmin;
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable info = new Hashtable();
                info.Add("id", dr["id"]);
                info.Add("giftname", dr["giftname"]);
                info.Add("content", Input.FilterHTML(dr["content"].ToString()));
                string giftPath = Public.GetXMLGiftValue("picPath");
                info.Add("giftpic", giftPath + "/" + dr["pic"]);
                info.Add("giftgpoint", Convert.ToInt32(dr["gpoint"]));
                info.Add("giftpoint", Convert.ToInt32(dr["point"]));
                info.Add("sendnumber", Convert.ToInt32(dr["SendNumber"]));
                info.Add("time", Public.getTimeLEXYearSpan(Convert.ToDateTime(dr["PostTime"])));
                string opSTR = "<a href=\"javascript:;\" onclick=\"deleteAll(" + dr["id"] + "," + uid + ",'gift')\" class=\"showok1\" title=\"删除\"></a>";
                info.Add("showop", opSTR);
                if (Convert.ToBoolean(dr["IsAd"]))
                {
                    info.Add("isvips", true);
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