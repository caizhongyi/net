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
    public class dev : ManagePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref NVelocity.VelocityContext context)
        {
            base.Page_Loadno(ref context);
            context.Put("cpagetitle", "应用程序 - 管理中心");
            this.ShowList(ref context);
        }

        protected void ShowList(ref VelocityContext context)
        {
            int uid = this.UserID;
            int recount = 30;
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            SqlConditionInfo[] st = null; DataTable dt = null;
            dt = JuSNS.Home.UtilPage.GetPage("manager_app_dev_aspx", PageIndex, recount, out ReCount, out PgCount, null);
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> infolist = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable info = new Hashtable();
                info.Add("spaceurl", this.GetSpaceURL(dr["userid"]));
                info.Add("truename", dr["truename"]);
                info.Add("username", dr["username"]);
                info.Add("userkey", dr["userkey"]);
                info.Add("tel", dr["tel"]);
                info.Add("mobile", dr["mobile"]);
                info.Add("email", dr["email"]);
                info.Add("jointime", Public.getTimeEXTSpan(Convert.ToDateTime(dr["JoinTime"])));
                info.Add("userid", dr["userid"]);
                info.Add("state", Public.GetEnumStateOp(dr["IsLock"], Convert.ToInt32(dr["userid"]), uid, "appdev"));
                info.Add("islock", Public.GetEnumState(dr["IsLock"]));
                infolist.Add(info);
            }
            dt.Dispose();
            context.Put("infolist", infolist);
            context.Put("recordcount", ReCount);
            context.Put("pagelist", JuSNS.MVC.Pager.PagSTR(PageIndex, PgCount, ReCount, recount));
        }
    }
}