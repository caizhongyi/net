using System;
using System.Text;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.app.shop
{
    public class order : UserPage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }
        protected void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            string shoptitle = Public.GetXMLShopValue("shopname");
            string q = GetString("q");
            context.Put("shop", shoptitle);
            context.Put("q", q);
            context.Put("cpagetitle", "我的购物车");
            ShowList(ref context, q);
        }

        protected void ShowList(ref VelocityContext context, string q)
        {
            int uid = this.GetUserID();
            int recount = 30;
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            SqlConditionInfo[] st = null;
            DataTable dt = null;
            if (!string.IsNullOrEmpty(q))
            {
                st = new SqlConditionInfo[2];
                st[0] = new SqlConditionInfo("@UserID", this.UserID, TypeCode.Int32);
                st[1] = new SqlConditionInfo("@IsLock", Convert.ToInt32(q), TypeCode.Int32);
                dt = JuSNS.Home.UtilPage.GetPage("user_ordercats_aspx", PageIndex, recount, out ReCount, out PgCount, st);
            }
            else
            {
                st = new SqlConditionInfo[1];
                st[0] = new SqlConditionInfo("@UserID", this.UserID, TypeCode.Int32);
                dt = JuSNS.Home.UtilPage.GetPage("user_ordercat_aspx", PageIndex, recount, out ReCount, out PgCount, st);
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
                if (Convert.ToInt32(dr["islock"])==0)
                {
                    if (Convert.ToBoolean(dr["ispost"]))
                    {
                        string postSTR = string.Empty;
                        if (!Convert.ToBoolean(dr["IsRevice"]))
                        {
                            postSTR = "<a href=\"javascript:;\" onclick=\"reviceorder("+dr["id"]+","+dr["UserID"]+")\">确认收货</a>";
                        }
                        else
                        {
                            postSTR = "已确认";
                        }
                        ispost = "<span class=\"green\">已发货</span>| " + postSTR + " | <a href=\"comment" + ExName + "?gid=" + dr["goodsid"] + "\">评价</a>";
                    }
                    else
                    {
                        ispost = "等待发货";
                    }
                }
                info.Add("state", Convert.ToInt32(dr["islock"]) == 0 ? "<span class=\"green\">成功</span>" : "<span class=\"reshow\">未支付</span>");
                info.Add("pay", Convert.ToInt32(dr["islock"]) == 0 ? ispost : "<a href=\"buy" + ExName + "?ordernumber=" + dr["ordernumber"] + "&gid=" + dr["goodsid"] + "&orderid=" + dr["id"] + "\" title=\"点击支付\">支付订单</a>");

                infolist.Add(info);
            }
            dt.Dispose();
            context.Put("infolist", infolist);
            context.Put("recordcount", ReCount);
            context.Put("pagelist", JuSNS.MVC.Pager.PagSTR(PageIndex, PgCount, ReCount, recount));
        }

    }
}
