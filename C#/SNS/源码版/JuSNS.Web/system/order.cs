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
    public class order : ManagePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref NVelocity.VelocityContext context)
        {
            base.Page_Loadno(ref context);
            context.Put("cpagetitle", "订单管理 - 管理中心");
            this.ShowList(ref context);
        }

        protected void ShowList(ref VelocityContext context)
        {
            int uid = this.GetUserID();
            int recount = 30;
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            SqlConditionInfo[] st = null;
            DataTable dt = null;
            string kwd = GetString("kwd");
            if (!string.IsNullOrEmpty(kwd))
            {
                context.Put("kwd", kwd);
                st = new SqlConditionInfo[1];
                st[0] = new SqlConditionInfo("@kwd", kwd, TypeCode.String);
                st[0].Blur = 3;
                dt = JuSNS.Home.UtilPage.GetPage("manager_order_key_aspx", PageIndex, recount, out ReCount, out PgCount, st);
            }
            else
            {
                dt = JuSNS.Home.UtilPage.GetPage("manager_orderall_aspx", PageIndex, recount, out ReCount, out PgCount, null);
            }
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> infolist = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable info = new Hashtable();
                info.Add("id", dr["id"]);
                info.Add("goodsname", dr["goodsname"]);
                info.Add("goodsurl", Public.URLWrite(dr["goodsid"], "goods"));
                info.Add("ordernumber", dr["ordernumber"]);
                info.Add("money", Convert.ToDouble(dr["money"]).ToString("0.00"));
                info.Add("gpoint", dr["gpoint"]);
                info.Add("time", Public.getTimeLEXYearSpan(Convert.ToDateTime(dr["posttime"])));
                string ispost = string.Empty;
                if (Convert.ToInt32(dr["islock"]) == 0)
                {
                    if (Convert.ToBoolean(dr["ispost"]))
                    {
                        string postSTR = string.Empty;
                        if (!Convert.ToBoolean(dr["IsRevice"]))
                        {
                            postSTR = "未确认收货";
                        }
                        else
                        {
                            postSTR = "已确认";
                        }
                        ispost = "<span class=\"green\">已发货</span>| " + postSTR + "";
                    }
                    else
                    {
                        ispost = "等待发货";
                    }
                }
                info.Add("state", Convert.ToInt32(dr["islock"]) == 0 ? "<span class=\"green\">成功</span>" : "<span class=\"reshow\">未支付</span>");
                info.Add("pay", Convert.ToInt32(dr["islock"]) == 0 ? ispost : "");

                infolist.Add(info);
            }
            dt.Dispose();
            context.Put("infolist", infolist);
            context.Put("recordcount", ReCount);
            context.Put("pagelist", JuSNS.MVC.Pager.PagSTR(PageIndex, PgCount, ReCount, recount));
        }
    }
}
