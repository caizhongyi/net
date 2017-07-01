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
    public class multebuy : BasePage
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
            string shoptitle = Public.GetXMLShopValue("shopname");
            string q = GetString("q");
            string pagetitle = string.Empty;
            switch (q)
            {
                case "rec":
                    pagetitle = "推荐的团购";
                    break;
                case "my":
                    pagetitle = "我的团购" + shoptitle;
                    break;
                case "friend":
                    pagetitle = "朋友的团购" + shoptitle;
                    break;
                case "city":
                    pagetitle = "同城团购" + shoptitle;
                    break;
                default:
                    pagetitle = "超级团购";
                    break;
            }
            ShowList(ref context, q, pagetitle);
            context.Put("shop", shoptitle);
            context.Put("q", q);
            ShowShopList(ref context);
            ShowGoodsList(ref context);
            ShowMulteList(ref context);
        }

        protected void ShowShopList(ref VelocityContext context)
        {
            int Num = 0;
            int recount = Convert.ToInt32(Public.GetXMLShopValue("IsRecShop"));
            List<ShopInfo> Infolist = JuSNS.Home.App.Shop.Instance.GetShopList(recount, 1);
            List<Hashtable> shoplist = new List<Hashtable>();
            foreach (ShopInfo info in Infolist)
            {
                Num++;
                Hashtable shop = new Hashtable();
                shop.Add("userid", info.UserID);
                shop.Add("shopname", info.ShopName);
                shop.Add("shopurl", Common.Public.URLWrite(info.Id, "shop"));
                shoplist.Add(shop);
            }
            context.Put("shoplist", shoplist);
            context.Put("shopcount", Num);
        }

        protected void ShowGoodsList(ref VelocityContext context)
        {
            int Num = 0;
            int recount = Convert.ToInt32(Public.GetXMLShopValue("IsRecGoods"));
            List<ShopGoodsInfo> Infolist = JuSNS.Home.App.Shop.Instance.GetGoodsList(recount, 1);
            List<Hashtable> goodslist = new List<Hashtable>();
            foreach (ShopGoodsInfo info in Infolist)
            {
                Num++;
                Hashtable goods = new Hashtable();
                goods.Add("userid", info.UserID);
                goods.Add("goodsname", info.GoodsName);
                goods.Add("goodsurl", Common.Public.URLWrite(info.Id, "goods"));
                goodslist.Add(goods);
            }
            context.Put("goodslist", goodslist);
            context.Put("goodscount", Num);
        }

        protected void ShowMulteList(ref VelocityContext context)
        {
            int Num = 0;
            int recount = Convert.ToInt32(Public.GetXMLShopValue("IsRecGoods"));
            List<ShopMulteBuyInfo> Infolist = JuSNS.Home.App.Shop.Instance.GetMulteList(recount, 1);
            List<Hashtable> multelist = new List<Hashtable>();
            foreach (ShopMulteBuyInfo info in Infolist)
            {
                Num++;
                Hashtable multe = new Hashtable();
                multe.Add("userid", info.UserID);
                multe.Add("title", info.Title);
                multe.Add("multeurl", Common.Public.URLWrite(info.Id, "multe"));
                multelist.Add(multe);
            }
            context.Put("multelist", multelist);
            context.Put("multecount", Num);
        }

        protected void ShowList(ref VelocityContext context, string q, string pagetitle)
        {
            int uid = this.GetUserID();
            string picpath = Public.GetXMLShopValue("shopPicPath");
            context.Put("shoppicpath", picpath);
            int recount = Convert.ToInt32(Public.GetXMLShopValue("PageNumber"));
            int ReCount = 0;
            int PgCount = 1;
            int PageIndex = GetQueryInt("page", 1);
            string kwd = GetString("kwd");
            SqlConditionInfo[] st = null;
            string FriendSTR = string.Empty;
            context.Put("q", q);
            if (!string.IsNullOrEmpty(kwd))
            {
                st = new SqlConditionInfo[1];
                st[0] = new SqlConditionInfo("@kwd", kwd, TypeCode.String);
                st[0].Blur = 3;
                context.Put("cpagetitle", "搜索关于” " + kwd + "“的" + pagetitle);
                context.Put("kwd", kwd);
            }
            else
            {
                context.Put("cpagetitle", pagetitle);
            }
            if (q == "friend") FriendSTR = JuSNS.Home.User.User.Instance.GetFriendList(uid);
            if (q == "city") FriendSTR = JuSNS.Home.User.User.Instance.GetUserInfo(uid).City.ToString();
            DataTable dt = JuSNS.Home.UtilPage.GetMultePage(q, FriendSTR, uid, PageIndex, recount, out ReCount, out PgCount, st);
            if (PgCount < 1) { PgCount = 1; }
            List<Hashtable> infolist = new List<Hashtable>();
            int isadmin = JuSNS.Home.User.User.Instance.GetUserInfo(uid).IsAdmin;
            foreach (DataRow dr in dt.Rows)
            {
                Hashtable info = new Hashtable();
                bool isOp = false;
                info.Add("id", dr["id"]);
                info.Add("truename", dr["truename"]);
                info.Add("spaceurl", this.GetSpaceURL(dr["userid"]));
                info.Add("headpic", this.GetHeadImage(dr["userid"], 2));
                info.Add("multeurl", Public.URLWrite(dr["id"], "multe"));
                info.Add("userid", dr["userid"]);
                string states = string.Empty;
                DateTime ntime = DateTime.Now;
                DateTime etime = Convert.ToDateTime(dr["EndTime"]);
                if ((ntime - etime).Days > 0)
                {
                    states = "<span class=\"green\">[已过期]</span>";
                }
                info.Add("photo", picpath + "/" + dr["pic"]);
                info.Add("title", dr["title"]);
                DateTime stime = Convert.ToDateTime(dr["starttime"]);
                if ((etime - stime).Days < 100)
                {
                    info.Add("timeout", stime.ToString("yyyy-MM-dd") + " 至 " + etime.ToString("yyyy-MM-dd"));
                }
                info.Add("price", dr["price"]);
                info.Add("minmember", Convert.ToInt32(dr["minmember"]));
                info.Add("maxmember", Convert.ToInt32(dr["maxmember"]));
                info.Add("joinmember", Convert.ToInt32(dr["joinmember"]));
                info.Add("time", Public.getTimeLEXYearSpan(Convert.ToDateTime(dr["PostTime"])));
                string opSTR = string.Empty;
                if (Convert.ToInt32(dr["userid"]) == uid || isadmin >0) isOp = true;
                if (isOp) opSTR += "<a href=\"javascript:;\" onclick=\"deleteAll(" + dr["id"] + "," + uid + ",'multe')\" class=\"showok1\" title=\"删除\"></a>";
                info.Add("showop", opSTR);
                if (Convert.ToInt32(dr["userid"]) == uid)
                {
                    info.Add("showedit", "<a href=\"new_m" + ExName + "?mid=" + dr["id"] + "\" class=\"edit1\" title=\"编辑\"></a>");
                }
                if (isadmin > 0)
                {
                    if (Convert.ToBoolean(dr["isrec"]))
                    {
                        info.Add("showrec", "<a href=\"javascript:;\" onclick=\"RecAll(" + dr["id"] + "," + uid + ",0,'multe')\" class=\"showrec1\"></a>");
                    }
                    else
                    {
                        info.Add("showrec", "<a href=\"javascript:;\" onclick=\"RecAll(" + dr["id"] + "," + uid + ",1,'multe')\" class=\"showrec\"></a>");
                    }
                }
                else
                {
                    info.Add("showrec", string.Empty);
                }
                infolist.Add(info);
            }
            dt.Dispose();
            context.Put("infolist", infolist);
            context.Put("recordcount", ReCount);
            context.Put("pagelist", JuSNS.MVC.Pager.PagSTR(PageIndex, PgCount, ReCount, recount));
        }
    }
}
