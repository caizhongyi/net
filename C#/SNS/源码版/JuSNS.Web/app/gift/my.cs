using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.app.gift
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
            context.Put("cpagetitle", "我收到的礼物");
            ShowList(ref context);
        }

        protected void ShowList(ref VelocityContext context)
        {
            int recount = 30;
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            SqlConditionInfo[] st = null; DataTable dt = null;
            st = new SqlConditionInfo[1];
            st[0] = new SqlConditionInfo("@UserID", this.UserID, TypeCode.Int32);
            dt = JuSNS.Home.UtilPage.GetPage("user_giftmy_aspx", PageIndex, recount, out ReCount, out PgCount, st);
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> infolist = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable info = new Hashtable();
                info.Add("id", dr["id"]);
                info.Add("giftname", dr["giftname"]);
                info.Add("truename", dr["truename"]);
                info.Add("userid", dr["userid"]);
                info.Add("giftcontent", Input.FilterHTML(dr["giftcontent"].ToString()));
                info.Add("spaceurl", this.GetSpaceURL(dr["userid"]));
                info.Add("contentall", Input.FilterHTML(dr["content"].ToString()));
                info.Add("content",Input.GetSubString(Input.FilterHTML(dr["content"].ToString()),38));
                string giftPath = Public.GetXMLGiftValue("picPath");
                info.Add("giftpic", giftPath + "/" + dr["pic"]);
                info.Add("time", Public.getTimeLEXYearSpan(Convert.ToDateTime(dr["PostTime"])));
                infolist.Add(info);
            }
            dt.Dispose();
            context.Put("infolist", infolist);
            context.Put("recordcount", ReCount);
            context.Put("pagelist", JuSNS.MVC.Pager.PagSTR(PageIndex, PgCount, ReCount, recount));
        }

    }
}
