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
    public class @new : UserPage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }


        protected void ShowInfo(ref VelocityContext context)
        {
            int uid = this.UserID;
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
            int sid = 0;
            base.Page_Load(ref context);
            string shoptitle = Public.GetXMLShopValue("shopname");
            int gid = GetInt("gid", 0);
            int scity = 0;
            int scity1 = 0;
            string pic = string.Empty;
            if (gid == 0)
            {
                context.Put("cpagetitle", "发起" + shoptitle);
            }
            else
            {
                context.Put("cpagetitle", "修改" + shoptitle);
                ShopGoodsInfo mdl = JuSNS.Home.App.Shop.Instance.GetGoodsInfo(gid);
                sid = mdl.ClassID;
                pic = mdl.Pic;
                scity1 = JuSNS.Home.Other.Area.Instance.GetAreaInfo(mdl.AreaID).ID;
                scity = JuSNS.Home.Other.Area.Instance.GetAreaInfo(mdl.AreaID).ParentID;
                context.Put("GoodsName", mdl.GoodsName);
                context.Put("Price", mdl.Price);
                context.Put("MPrice", mdl.MPrice);
                context.Put("MultePrice", mdl.MultePrice);
                context.Put("Content", mdl.Content);
                context.Put("StartTime", mdl.StartTime.ToString("yyyy-MM-dd"));
                context.Put("EndTime", mdl.EndTime.ToString("yyyy-MM-dd"));
                context.Put("Number", mdl.Number);
                context.Put("AddRess", mdl.AddRess);
                context.Put("keywords1", mdl.Keywords.Split(',')[0]);
                context.Put("keywords2", mdl.Keywords.Split(',')[1]);
                context.Put("keywords3", mdl.Keywords.Split(',')[2]);
                context.Put("Tel", mdl.Tel);
                context.Put("ExpressStyle", mdl.ExpressStyle);
                context.Put("Money", mdl.ExpressContent);
                context.Put("MulteBuy", mdl.MulteBuy);
                context.Put("MulteMinNumber", mdl.MulteMinNumber);
                context.Put("MulteMaxNumber", mdl.MulteMaxNumber);
                context.Put("GPoint", mdl.GPoint);
                if (mdl.IsOld)
                {
                    context.Put("isold", true);
                }
            }
            context.Put("shop", shoptitle);
            context.Put("sitem", scity1);
            context.Put("areaid", scity);
            context.Put("photo", pic);
            context.Put("classlist", ShowClassList(string.Empty, 0, sid));
        }

        protected string ShowClassList(string tmp, int parentid, int sid)
        {
            string listSTR = string.Empty;
            List<ShopClassInfo> infolist = JuSNS.Home.App.Shop.Instance.GetShopClass(parentid);
            foreach (ShopClassInfo info in infolist)
            {
                if (sid == info.Id)
                {
                    listSTR += "<option value=\"" + info.Id + "\" selected>" + tmp + info.ClassName + "</option>";
                }
                else
                {
                    listSTR += "<option value=\"" + info.Id + "\">" + tmp + info.ClassName + "</option>";
                }
                listSTR += ShowClassList(" -- ", info.Id, sid);
            }
            return listSTR;
        }



        public override void Page_PostBack(ref VelocityContext context)
        {
            int gid = GetInt("gid",0);
            ShopGoodsInfo mdl = new ShopGoodsInfo();
            mdl.AddRess = GetString("txtaddress");
            int provinceid = GetInt("sltareaid", 0);
            int areaid = GetInt("sltareaid1", 0);
            if (areaid != 0)
            {
                mdl.AreaID = areaid;
            }
            else
            {
                mdl.AreaID = provinceid;
            }
            mdl.ClassID = GetInt("sltclassid", 0);
            mdl.Click = 0;
            if (GetInt("isold",0) == 1)
            {
                mdl.IsOld = true;
            }
            else
            {
                mdl.IsOld = false;
            }
            mdl.Content = GetString("txtcontent");
            mdl.IsRec = false;
            mdl.DownNumber = 0;
            mdl.GPoint = GetInt("txtGPoint", 0);
            string txtStartTime = GetString("txtStartTime");
            string txtEndTime = GetString("txtEndTime");
            string errors = string.Empty;
            if (string.IsNullOrEmpty(GetString("txtGoodsName")))
            {
                errors += "填写名称；";
            }
            mdl.GoodsName = GetString("txtGoodsName");
            if (string.IsNullOrEmpty(txtEndTime))
            {
                mdl.EndTime = DateTime.Now.AddMonths(24);
            }
            else
            {
                if (Input.IsDate(txtEndTime))
                {
                    mdl.EndTime = Convert.ToDateTime(txtEndTime);
                }
                else
                {
                    errors += "结束日期必须是时间格式；";
                }
            }
            mdl.ExpressContent = GetString("txtMoney");
            mdl.ExpressStyle = Convert.ToByte(GetInt("ExpressStyle", 0));
            mdl.Id = gid;
            byte ischeck = Convert.ToByte(Public.GetXMLShopValue("shopcheck"));
            if (JuSNS.Home.User.User.Instance.GetUserInfo(this.UserID).IsAdmin > 0)
            {
                ischeck = 0;
            }
            string passSTR = string.Empty;
            if (ischeck == 1)
            {
                passSTR = "但需要审核";
            }
            mdl.IsLock = ischeck;
            mdl.Keywords = GetString("keywords1") + "," + GetString("keywords2") + "," + GetString("keywords3");
            mdl.MPrice = Convert.ToDouble(HttpContext.Current.Request["txtmPrice"]);//txtmPrice
            mdl.MultePrice = Convert.ToDouble(HttpContext.Current.Request["txtMultePrice"]);//txtMultePrice
            int MulteBuy =GetInt("radiMulteBuy",0);
            if(MulteBuy>0)
            {
                mdl.MulteBuy = 1;
            }
            else
            {
                mdl.MulteBuy = 0;
            }
            mdl.MulteMaxNumber = GetInt("txtMulteMaxNumber", 0);
            mdl.BuyNumber = 0;
            mdl.MulteMinNumber = GetInt("txtMulteMinNumber", 0);
            if (GetInt("txtNumber", 0) == 0)
            {
                mdl.Number = 1000000;
            }
            else
            {
                mdl.Number = GetInt("txtNumber", 0);
            }
            string Pic = string.Empty;
            if (HttpContext.Current.Request.Files.Count > 0)
            {
                string Path = Public.GetXMLShopValue("shopPicPath");
                HttpPostedFile hpf = HttpContext.Current.Request.Files[0];
                Pic = Public.GetFile(hpf, Public.GetXMLValue("pictype"), Path);
                if (string.IsNullOrEmpty(Pic))
                {
                    Pic = GetString("hidephoto");
                }
            }
            else
            {
                Pic = GetString("hidephoto");
            }
            mdl.Pic = Pic;
            mdl.PostIP = Public.GetClientIP();
            mdl.PostTime = DateTime.Now;
            mdl.Price = Convert.ToDouble(HttpContext.Current.Request["txtprice"]);// GetInt("txtprice", 0);
            mdl.ShopID = JuSNS.Home.App.Shop.Instance.GetShopID(this.UserID);
            if (string.IsNullOrEmpty(txtStartTime))
            {
                mdl.StartTime = DateTime.Now.AddMonths(24);
            }
            else
            {
                if (Input.IsDate(txtStartTime))
                {
                    mdl.StartTime = Convert.ToDateTime(txtStartTime);
                }
                else
                {
                    errors += "开始日期必须是时间格式；";
                }
            }
            mdl.Tel = GetString("txtTel");
            mdl.TopNumber = 0;
            mdl.UserID = this.UserID;
            if (string.IsNullOrEmpty(errors))
            {
                int n = JuSNS.Home.App.Shop.Instance.InsertShopGoods(mdl);
                if (n > 0)
                {
                    if (ischeck == 1)
                    {
                        if (gid== 0)
                        {
                            context.Put("rights", "添加成功！" + passSTR);
                        }
                        else
                        {
                            context.Put("rights", "修改成功！" + passSTR);
                        }
                    }
                    else
                    {
                        JuSNS.Home.User.User.Instance.InsertDyn(new DynInfo(0, this.GetUserID(), 0, (int)EnumDynType.CreatGoods, string.Empty, DateTime.Now, n, string.Empty));
                        //更新积分
                        JuSNS.Home.User.User.Instance.UpdateInte(this.GetUserID(), Public.JSplit(31), 0, 0, "发布商品");
                        context.Put("redirecturl", "../shop?q=my");
                    }
                }
                else
                {
                    context.Put("errors", "操作失败");
                }
            }
            else
            {
                context.Put("errors", errors);
            }
            ShowInfo(ref context);
        }
    }
}
