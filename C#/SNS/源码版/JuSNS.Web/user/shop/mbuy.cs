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
    public class mbuy : BasePage
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
                    string picpath = Public.GetXMLShopValue("shopPicPath");
                    context.Put("shoppicpath", picpath);
                    ShowList(ref context, sid);
                    ShowMulteList(ref context, sid);
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

        protected void ShowMulteList(ref VelocityContext context, int sid)
        {
            string picpath = Public.GetXMLShopValue("shopPicPath");
            int recount = 10;
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            SqlConditionInfo[] st = new SqlConditionInfo[1];
            st[0] = new SqlConditionInfo("@ShopID", sid, TypeCode.Int32);
            DataTable dt = JuSNS.Home.UtilPage.GetPage("user_shopmulte_aspx", PageIndex, recount, out ReCount, out PgCount, st);
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> multelist = new List<Hashtable>();
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable multe = new Hashtable();
                multe.Add("id", dr["id"]);
                multe.Add("title", Input.GetSubString(dr["title"].ToString(), 30));
                multe.Add("multeurl", Public.URLWrite(dr["id"], "multe"));
                string states = string.Empty;
                DateTime ntime = DateTime.Now;
                DateTime etime = Convert.ToDateTime(dr["EndTime"]);
                if ((ntime - etime).Days > 0)
                {
                    multe.Add("timeout", true);
                }
                multe.Add("starttime", Convert.ToDateTime(dr["starttime"]).ToString("MM月dd日"));
                multe.Add("endtime", Convert.ToDateTime(dr["endtime"]).ToString("MM月dd日"));
                multe.Add("joinmember", dr["joinmember"]);
                multe.Add("photo", picpath + "/" + dr["pic"]);
                multelist.Add(multe);
            }
            dt.Dispose();
            context.Put("multelist", multelist);
            context.Put("pagelist", JuSNS.MVC.Pager.PagSTR(PageIndex, PgCount, ReCount, recount));
        }
    }
}
