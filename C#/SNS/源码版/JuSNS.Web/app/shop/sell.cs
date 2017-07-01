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
    public class sell : UserPage
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
            context.Put("cpagetitle", "销售记录");
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
            st = new SqlConditionInfo[1];
            st[0] = new SqlConditionInfo("@UserID", this.UserID, TypeCode.Int32);
            dt = JuSNS.Home.UtilPage.GetPage("user_ordersell_aspx", PageIndex, recount, out ReCount, out PgCount, st);
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> infolist = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable info = new Hashtable();
                info.Add("id", dr["id"]);
                info.Add("goodsname", Input.GetSubString(dr["goodsname"].ToString(),24));
                info.Add("spaceurl", this.GetSpaceURL(dr["userid"]));
                info.Add("username", JuSNS.Home.User.User.Instance.GetUserInfo(dr["userid"]).TrueName);
                info.Add("goodsurl", Public.URLWrite(dr["goodsid"], "goods"));
                info.Add("ordernumber", dr["ordernumber"]);
                info.Add("money", Convert.ToDouble(dr["money"]).ToString("0.00"));
                info.Add("gpoint", dr["gpoint"]);
                info.Add("time", Public.getTimeLEXYearSpan(Convert.ToDateTime(dr["posttime"])));
                info.Add("state", Convert.ToInt32(dr["islock"]) == 0 ? "<span class=\"green\">已支付</span>" : "<span class=\"reshow\">未支付</span>");
                string ReseSTR = string.Empty;
                if (Convert.ToBoolean(dr["IsRevice"]))
                {
                    ReseSTR = "已确认";
                }
                else
                {
                    ReseSTR = "等待确认";
                }
                info.Add("pay", Convert.ToBoolean(dr["ispost"]) == true ? "已发货 | " + ReseSTR : "<a href=\"javascript:;\" onclick=\"postorder('" + dr["ordernumber"] + "'," + dr["goodsid"] + "," + dr["id"] + "," + this.UserID + ")\" title=\"点击发货\">发货</a>");

                infolist.Add(info);
            }
            dt.Dispose();
            context.Put("infolist", infolist);
            context.Put("recordcount", ReCount);
            context.Put("pagelist", JuSNS.MVC.Pager.PagSTR(PageIndex, PgCount, ReCount, recount));
        }

    }
}

