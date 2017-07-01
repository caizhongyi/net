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
    public class news : UserPage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            int uid = this.GetUserID();
            ShopInfo mdl = JuSNS.Home.App.Shop.Instance.GetShopForUserID(uid);
            if (mdl != null)
            {
                context.Put("isshop", true);
            }
            int isOpen = Convert.ToInt16(Public.GetXMLShopValue("isopen"));
            if (isOpen == 0)
            {
                context.Put("redirecturl", root + "/library/page/open" + ExName + "?q=false&app=shop");
            }
            else
            {
                int ismember = Convert.ToInt16(Public.GetXMLShopValue("ismember"));
                if (ismember != 1)
                {
                    if (uid == 0)
                    {
                        context.Put("redirecturl", root + "/library/page/error" + ExName + "?error=Err_TimeOut&urls=" + root + "/login" + ExName + "?urls=" + HttpContext.Current.Request.Url);
                    }
                }
                ShowInfo(ref context);
            }
        }


        protected void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            ShowList(ref context);
            context.Put("cpagetitle", "管理公告");
            string shoptitle = Public.GetXMLShopValue("shopname");
            context.Put("shop", shoptitle);
        }


        protected void ShowList(ref VelocityContext context)
        {
            int uid = this.GetUserID();
            int recount = 20;
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            SqlConditionInfo[] st = new SqlConditionInfo[1];
            st[0] = new SqlConditionInfo("@ShopID", JuSNS.Home.App.Shop.Instance.GetShopForUserID(this.GetUserID()).Id, TypeCode.Int32);
            DataTable dt = JuSNS.Home.UtilPage.GetPage("user_shopnews_aspx", PageIndex, recount, out ReCount, out PgCount, st);
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> infolist = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable info = new Hashtable();
                info.Add("id", dr["id"]);
                info.Add("title", dr["title"]);
                info.Add("shopnewsurl", root+"/user/shop/info" + ExName + "?nid=" + dr["id"]);
                info.Add("time", Public.getTimeLEXYearSpan(Convert.ToDateTime(dr["creatTime"])));
                string opSTR = "<a href=\"javascript:;\" onclick=\"deleteAll(" + dr["id"] + "," + uid + ",'shopnews')\" class=\"showok1\" title=\"删除\"></a>";
                info.Add("showop", opSTR);
                info.Add("showedit", "<a href=\"shopnews" + ExName + "?nid=" + dr["id"] + "\" class=\"edit1\" title=\"编辑\"></a>");
                infolist.Add(info);
            }
            dt.Dispose();
            context.Put("infolist", infolist);
            context.Put("recordcount", ReCount);
            context.Put("pagelist", JuSNS.MVC.Pager.PagSTR(PageIndex, PgCount, ReCount, recount));
        }
    }
}