using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.home.magic
{
    public class record : UserPage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        public void ShowInfo(ref VelocityContext context)
        {
            base.Page_Loadno(ref context);
            context.Put("cpagetitle", "道具使用记录");
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
            int q = GetInt("q", 0);
            SqlConditionInfo[] st = new SqlConditionInfo[2];
            st[0] = new SqlConditionInfo("@UserID", this.UserID, TypeCode.Int32);
            st[1] = new SqlConditionInfo("@R", q, TypeCode.Int32);
            DataTable dt = JuSNS.Home.UtilPage.GetPage("user_magic_record_aspx", PageIndex, recount, out ReCount, out PgCount, st);
            if (PgCount < 1) { PgCount = 1; }

            List<Hashtable> reclist = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable rec = new Hashtable();
                MagicInfo mdl = JuSNS.Home.User.User.Instance.GetMagicInfo(dr["mid"]);
                rec.Add("id", dr["id"]);
                rec.Add("mid", dr["mid"]);
                rec.Add("mnum", mdl.Number);
                rec.Add("mdesc", dr["MDesc"]);
                rec.Add("mname", mdl.MName);
                //0获得1赠送2使用
                if (dr["mType"].ToString() == "2")
                {
                    rec.Add("number", dr["Num"] + "次");
                }
                else
                {
                    rec.Add("number", dr["Num"] + "个");
                }
                rec.Add("time", Public.getTimeEXTSpan(Convert.ToDateTime(dr["PostTime"])));
                reclist.Add(rec);
            }
            dt.Dispose();
            context.Put("reclist", reclist);
            context.Put("recordcount", ReCount);
            context.Put("pagelist", JuSNS.MVC.Pager.PagSTR(PageIndex, PgCount, ReCount, recount));
        }
    }
}