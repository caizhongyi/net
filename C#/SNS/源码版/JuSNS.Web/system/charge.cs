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
    public class charge : UserPage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        public void ShowInfo(ref VelocityContext context)
        {
            base.Page_Loadno(ref context);
            context.Put("cpagetitle", "充值管理");
            ShowList(ref context);
        }

        protected void ShowList(ref VelocityContext context)
        {
            int recount = 20;
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            DataTable dt = null;
            string q = GetString("q");
            context.Put("q", q);
            if (Input.IsInteger(q))
            {
                SqlConditionInfo[] st = new SqlConditionInfo[1];
                st[0] = new SqlConditionInfo("@Succ", Convert.ToInt32(q), TypeCode.Int32);
                dt = JuSNS.Home.UtilPage.GetPage("manager_Charges_aspx", PageIndex, recount, out ReCount, out PgCount, st);
            }
            else
            {
                dt = JuSNS.Home.UtilPage.GetPage("manager_Charge_aspx", PageIndex, recount, out ReCount, out PgCount, null);
            }
            if (PgCount < 1) { PgCount = 1; }

            List<Hashtable> reclist = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable rec = new Hashtable();
                rec.Add("id", dr["id"]);
                rec.Add("money", dr["money"]);
                rec.Add("ordernumber", dr["ordernumber"]);
                rec.Add("point", dr["point"]);
                rec.Add("gpoint", dr["gpoint"]);
                rec.Add("succ", dr["IsSucces"].ToString().ToLower().Replace("true", "<a href=\"javascript:;\" onclick=\"chargeorder(" + dr["id"] + ",0)\"><span class=\"reshow\" title=\"取消\">成功</span></a>").Replace("false", "<a href=\"javascript:;\" onclick=\"chargeorder(" + dr["id"] + ",1)\" title=\"设为成功\">失败</a>"));
                rec.Add("time", Convert.ToDateTime(dr["CreatTime"]).ToString("yyyy-MM-dd"));
                reclist.Add(rec);
            }
            dt.Dispose();
            context.Put("reclist", reclist);
            context.Put("recordcount", ReCount);
            context.Put("pagelist", JuSNS.MVC.Pager.PagSTR(PageIndex, PgCount, ReCount, recount));
        }
    }
}
