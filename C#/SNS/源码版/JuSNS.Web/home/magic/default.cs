using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.home.magic
{
    public class @default : UserPage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        public void ShowInfo(ref VelocityContext context)
        {
            base.Page_Loadno(ref context);
            context.Put("cpagetitle", "道具中心");
            string charge = Public.GetXMLValue("charge");
            if (charge.IndexOf(",") == -1)
            {
                context.Put("errors", "参数配置错误：ConfigError");
            }
            else
            {
                string[] chargeARR = charge.Split(',');
                context.Put("pointvalue", chargeARR[0]);
                context.Put("gpointvalue", chargeARR[1]);
            }
            ShowList(ref context);
        }

        protected void ShowList(ref VelocityContext context)
        {
            int recount = 20;
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            DataTable dt = null;
            string q= GetString("q");
            if (!string.IsNullOrEmpty(q))
            {
                dt = JuSNS.Home.UtilPage.GetPage("user_magic_hot_aspx", PageIndex, recount, out ReCount, out PgCount, null);
            }
            else
            {
                dt = JuSNS.Home.UtilPage.GetPage("user_magic_aspx", PageIndex, recount, out ReCount, out PgCount, null);
            }
            if (PgCount < 1) { PgCount = 1; }

            List<Hashtable> magiclist = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable magic = new Hashtable();
                magic.Add("id", dr["id"]);
                magic.Add("magicpic", root + "/uploads/magic/" + dr["pic"]);
                magic.Add("mname", dr["mname"]);
                magic.Add("mdesc", dr["mdesc"]);
                magic.Add("number", dr["number"]);
                magic.Add("gpoint", dr["gpoint"]);
                magic.Add("point", dr["point"]);
                magic.Add("buynumber", dr["buynumber"]);
                magiclist.Add(magic);
            }
            dt.Dispose();
            context.Put("magiclist", magiclist);
            context.Put("recordcount", ReCount);
            context.Put("pagelist", JuSNS.MVC.Pager.PagSTR(PageIndex, PgCount, ReCount, recount));
        }
    }
}