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
    public class shopnews : UserPage
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
                ShowInfo(ref context);
            }
        }


        protected void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            string shoptitle = Public.GetXMLShopValue("shopname");
            context.Put("shop", shoptitle);
            int nid = GetInt("nid", 0);
            if (nid > 0)
            {
                context.Put("cpagetitle", "修改公告");
                ShopNewsInfo info = JuSNS.Home.App.Shop.Instance.GetShopNewsInfo(nid);
                context.Put("title", info.Title);
                context.Put("content", info.Content);
            }
            else
            {
                context.Put("cpagetitle", "添加公告");
            }
        }

        public override void Page_PostBack(ref VelocityContext context)
        {
            int nid = GetInt("nid", 0);
            ShopNewsInfo info = new ShopNewsInfo();
            info.Click = 0; 
            info.Content = GetString("content");
            info.CreatTime = DateTime.Now;
            info.Id = nid;
            info.Islock = false;
            info.ShopID = JuSNS.Home.App.Shop.Instance.GetShopForUserID(this.GetUserID()).Id;
            info.Title = GetString("title");
            int n = JuSNS.Home.App.Shop.Instance.InsertShopNews(info);
            if (n > 0)
            {
                context.Put("redirecturl", "news" + ExName);
            }
            else
            {
                context.Put("errors", "发生错误");
            }
        }
    }
}
