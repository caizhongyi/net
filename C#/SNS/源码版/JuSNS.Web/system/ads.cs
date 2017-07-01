using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.system
{
    public class ads : ManagePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref NVelocity.VelocityContext context)
        {
            base.Page_Loadno(ref context);
            context.Put("cpagetitle", "广告管理 - 管理中心");
            this.ShowList(ref context);
        }

        protected void ShowList(ref VelocityContext context)
        {
            int uid = this.UserID;
            int recount = 20;
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            DataTable dt = null;
            dt = JuSNS.Home.UtilPage.GetPage("manager_ads_aspx", PageIndex, recount, out ReCount, out PgCount, null);
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> infolist = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable info = new Hashtable();
                info.Add("id", dr["id"]);
                info.Add("title", dr["title"]);
                info.Add("content", dr["content"]);
                info.Add("bpath", Public.GetXMLPageValue("adspic"));
                info.Add("pic", dr["pic"]);
                info.Add("url", dr["url"]);
                info.Add("positionType", dr["positionType"]);
                info.Add("islock", Public.GetEnumStateOp(dr["islock"],Convert.ToInt32(dr["id"]),this.UserID,"ads"));
                info.Add("EndTime", Convert.ToDateTime(dr["EndTime"]).ToString("yyyy-MM-dd"));
                infolist.Add(info);
            }
            dt.Dispose();
            context.Put("infolist", infolist);
            context.Put("recordcount", ReCount);
            context.Put("pagelist", JuSNS.MVC.Pager.PagSTR(PageIndex, PgCount, ReCount, recount));
        }
    }
}
