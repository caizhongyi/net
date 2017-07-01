using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.app.shop
{
    public class buy : UserPage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            int uid = this.UserID;
            int gid = GetInt("gid", 0);
            string shoptitle = Public.GetXMLShopValue("shopname");
            context.Put("shop", shoptitle);
            ShopInfo sinfo = JuSNS.Home.App.Shop.Instance.GetShopForUserID(uid);
            if (sinfo != null)
            {
                context.Put("isshop", true);
            }
            int isOpen = Convert.ToInt16(Public.GetXMLShopValue("isopen"));
            if (isOpen == 0)
            {
                context.Put("redirecturl", root + "/library/page/open" + ExName + "?q=false&app=shop");
            }
            string OpenTrade = Public.GetXMLShopValue("OpenTrade");
            if (OpenTrade != "1")
            {
                PageError("未开启在线交易", root + "/app/shop/goods" + ExName + "?gid=" + gid);
            }
            string gName = Public.GetXMLValue("gName");
            //判断金币够不
            ShopGoodsInfo mdl = JuSNS.Home.App.Shop.Instance.GetGoodsInfo(gid);
            double money = mdl.MPrice;
            int GPoint = mdl.GPoint;
            UserInfo uinfo = JuSNS.Home.User.User.Instance.GetUserInfo(uid);
            if (mdl.Number == 0)
            {
                PageError("商品无货,无法购买", root + "/app/shop/goods" + ExName + "?gid=" + gid);
            }
            if (uinfo.Inteyb < GPoint)
            {
                PageError("您的" + gName + "(" + uinfo.Inteyb + ")不足" + GPoint + ",无法购买", root + "/app/shop/goods" + ExName + "?gid=" + gid);
            }
            if (uinfo.Money < money)
            {
                PageError("您的帐户不足 " + money + "，无法购买。请<a href=\"" + root + "/home/charge\">充值</a>后购买", root + "/app/shop/goods" + ExName + "?gid=" + gid);
            }
            string ordernumber=GetString("ordernumber");
            int oid = GetInt("orderid", 0);
            if (string.IsNullOrEmpty(ordernumber) && ordernumber.Length < 10&&oid==0)
            {
                ordernumber = DateTime.Now.ToString("MMddhh-") + Rand.Str(9).ToUpper();
                ShopOrderInfo info = new ShopOrderInfo();
                info.GoodsID = gid;
                info.GPoint = mdl.GPoint;
                info.Id = 0;
                info.IsLock = (byte)EnumCusState.ForLock;
                info.Money = Convert.ToDecimal(mdl.MPrice);
                info.OrderNumber = ordernumber;
                info.PostIP = Public.GetClientIP();
                info.PostTime = DateTime.Now;
                info.UserID = uid;
                oid = JuSNS.Home.App.Shop.Instance.InserShopOrder(info);
            }
            if (oid > 0)
            {
                context.Put("oid", oid);
                context.Put("cpagetitle", "购买" + shoptitle);
                context.Put("paymoney", mdl.MPrice.ToString("0.00"));
                context.Put("gspoint", mdl.GPoint);
                context.Put("mymoney", uinfo.Money.ToString("0.00"));
                context.Put("mygpoint", uinfo.Inteyb);
                context.Put("ordernumber", ordernumber);
                context.Put("goodsname", mdl.GoodsName);
                context.Put("gid", gid);
            }
            else
            {
                PageError("异常错误", root + "/app/shop/goods" + ExName + "?gid=" + gid);
            }
        }

        public override void Page_PostBack(ref VelocityContext context)
        {
            int gid = GetInt("gid", 0);
            int oid = GetInt("oid", 0);
            int uid = this.UserID;
            //扣除金钱，金币
            ShopGoodsInfo mdl = JuSNS.Home.App.Shop.Instance.GetGoodsInfo(gid);
            double money = mdl.MPrice;
            int gpoint = mdl.GPoint;
            string ordernumbers = GetString("ordernumbers");
            if (gpoint > 0)
            {
                JuSNS.Home.User.User.Instance.UpdateInte(uid, gpoint, 1, 1, "购买商品扣除金币，订单号：" + ordernumbers + "");
            }
            JuSNS.Home.User.User.Instance.UpdateInte(uid, money, 2, 1, "购买商品扣除帐户资金，订单号：" + ordernumbers + "");
            int n = JuSNS.Home.App.Shop.Instance.UpdateShopOrder(oid);
            context.Put("redirecturl", "s");
            PageRight("商品购买成功！", root + "/app/shop/order" + ExName, true);
        }
    }
}
