using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.user.shop
{
    public class news : BasePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            int uid = this.GetUserID();
            int isOpen = Convert.ToInt16(Public.GetXMLShopValue("isopen"));
            if (isOpen == 0)
            {
                context.Put("redirecturl", root + "/library/page/open" + ExName + "?q=false&app=shop");
            }
            else
            {
                ShowInfo(ref context);
            }
        }


        protected void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            int sid = GetInt("sid", 0);
            context.Put("sid", sid);
            ShopInfo info = JuSNS.Home.App.Shop.Instance.GetShopForID(sid);
            if (info == null)
            {
                context.Put("errors", "错误的参数");
            }
            else
            {
                if (info.IsLock == 0)
                {
                    context.Put("cpagetitle", info.ShopName + " - 店铺管理");
                    context.Put("shopname", info.ShopName);
                    context.Put("shopurl", Public.URLWrite(sid, "shop"));
                    context.Put("shopbg", Public.GetXMLShopValue("shopPicPath") + "/" + info.Pic);
                    context.Put("jointime", info.PostTime.ToString("yyyy年MM月dd日"));
                    context.Put("address", JuSNS.Home.Other.Area.Instance.GetAreaInfo(info.AreaID).Name + info.ShopAddress);
                    context.Put("tel", info.Tel);
                    context.Put("fax", info.Fax);
                    context.Put("linkman", info.LinkMan);
                    context.Put("userid", info.UserID);
                    context.Put("content", Input.GetSubString(info.Content, 80));
                    ShowList(ref context, sid);
                    ShowListPage(ref context, sid);
                }
                else
                {
                    context.Put("errors", "此店铺未开放");
                }
            }
        }


        protected void ShowList(ref VelocityContext context, int sid)
        {
            int uid = this.GetUserID();
            int recount = 6;
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            SqlConditionInfo[] st = new SqlConditionInfo[1];
            st[0] = new SqlConditionInfo("@ShopID", sid, TypeCode.Int32);
            DataTable dt = JuSNS.Home.UtilPage.GetPage("user_shopnews_aspx", PageIndex, recount, out ReCount, out PgCount, st);
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> infolist = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable info = new Hashtable();
                info.Add("id", dr["id"]);
                info.Add("title", Input.GetSubString(dr["title"].ToString(), 36));
                info.Add("shopnewsurl", root + "/user/shop/info" + ExName + "?nid=" + dr["id"]);
                infolist.Add(info);
            }
            dt.Dispose();
            context.Put("infolist", infolist);
            context.Put("recordcount", ReCount);
        }


        protected void ShowListPage(ref VelocityContext context, int sid)
        {
            int uid = this.GetUserID();
            int recount = 16;
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            SqlConditionInfo[] st = new SqlConditionInfo[1];
            st[0] = new SqlConditionInfo("@ShopID", sid, TypeCode.Int32);
            DataTable dt = JuSNS.Home.UtilPage.GetPage("user_shopnews_aspx", PageIndex, recount, out ReCount, out PgCount, st);
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> info1list = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable info1 = new Hashtable();
                info1.Add("id", dr["id"]);
                info1.Add("title", dr["title"]);
                info1.Add("time", Public.getTimeEXPINSpan(Convert.ToDateTime(dr["creatTime"])));
                info1.Add("shopnewsurl", root + "/user/shop/info" + ExName + "?nid=" + dr["id"]);
                info1list.Add(info1);
            }
            dt.Dispose();
            context.Put("info1list", info1list);
            context.Put("recordcount1", ReCount);
            context.Put("pagelist", JuSNS.MVC.Pager.PagSTR(PageIndex, PgCount, ReCount, recount));
        }

    }
}
