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
    public class new_m : UserPage
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
            base.Page_Load(ref context);
            int mid = GetInt("mid", 0);
            int gid = GetInt("gid", 0);
            int scity = 0;
            int scity1 = 0;
            context.Put("cpagetitle", "发起团购");
            string pic = string.Empty;
            if (mid > 0)
            {
                ShopMulteBuyInfo mdl = JuSNS.Home.App.Shop.Instance.GetMulteBuyInfo(mid);
                if (mdl.UserID != uid)
                {
                    context.Put("errors", "不是您的信息，你不能编辑");
                }
                else
                {
                    pic = mdl.Pic;
                    gid = mdl.GoodsID;
                    scity1 = mdl.CityID;
                    scity = mdl.ProvinceID;
                    context.Put("Title", mdl.Title);
                    context.Put("MinMember", mdl.MinMember);
                    context.Put("MaxMember", mdl.MaxMember);
                    context.Put("Content", mdl.Content);
                    context.Put("StartTime", mdl.StartTime.ToString("yyyy-MM-dd"));
                    context.Put("EndTime", mdl.EndTime.ToString("yyyy-MM-dd"));
                    context.Put("Price", mdl.Price.ToString("0.00"));
                    context.Put("AddRess", mdl.AddRess);
                    context.Put("keywords1", mdl.Keywords.Split(',')[0]);
                    context.Put("keywords2", mdl.Keywords.Split(',')[1]);
                    context.Put("keywords3", mdl.Keywords.Split(',')[2]);
                    context.Put("LinkStyle", mdl.LinkStyle);
                    context.Put("userid", mdl.UserID);
                }
            }
            if (gid > 0)
            {
                ShopGoodsInfo sgoods = JuSNS.Home.App.Shop.Instance.GetGoodsInfo(gid);
                context.Put("goodsname", sgoods.GoodsName);
                context.Put("MinMember", sgoods.MulteMinNumber);
                context.Put("MaxMember", sgoods.MulteMaxNumber);
            }
            context.Put("gid", gid);
            string shoptitle = Public.GetXMLShopValue("shopname");
            context.Put("shop", shoptitle);
            context.Put("sitem", scity1);
            context.Put("areaid", scity);
            context.Put("photo", pic);
        }



        public override void Page_PostBack(ref VelocityContext context)
        {
            int gid = GetInt("gid", 0);
            ShopMulteBuyInfo mdl = new ShopMulteBuyInfo();
            mdl.AddRess = GetString("txtaddress");
            int provinceID = GetInt("provinceID", 0);
            int CityID = GetInt("CityID", 0);
            mdl.ProvinceID = provinceID;
            mdl.CityID = CityID;
            mdl.AttMember = 0;
            mdl.Content = GetString("txtcontent");
            string txtStartTime = GetString("txtStartTime");
            string txtEndTime = GetString("txtEndTime");
            string errors = string.Empty;
            if (string.IsNullOrEmpty(GetString("txtTitle")))
            {
                errors += "填写名称；";
            }
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
            mdl.GoodsID = gid;
            mdl.Id = GetInt("mid", 0);
            byte ischeck = Convert.ToByte(Public.GetXMLShopValue("MulteBuyCheck"));
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
            mdl.JoinMember = 0;
            mdl.Keywords = GetString("keywords1") + "," + GetString("keywords2") + "," + GetString("keywords3");
            mdl.LinkStyle = GetString("txtLinkStyle");
            mdl.MaxMember = GetInt("txtMaxMember", 0);
            mdl.MinMember = GetInt("txtMinMember", 0);
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
            mdl.Price = Convert.ToDecimal(GetString("txtPrice"));
            mdl.Title = GetString("txtTitle");
            mdl.UserID = this.UserID;

            if (string.IsNullOrEmpty(errors))
            {
                int n = JuSNS.Home.App.Shop.Instance.InsertMulteBuy(mdl);
                if (n > 0)
                {
                    if (ischeck == 1)
                    {
                        if (gid == 0)
                        {
                            context.Put("rights", "成功发起了团购！" + passSTR);
                        }
                        else
                        {
                            context.Put("rights", "修改团购成功！" + passSTR);
                        }
                    }
                    else
                    {
                        JuSNS.Home.User.User.Instance.InsertDyn(new DynInfo(0, this.GetUserID(), 0, (int)EnumDynType.CreatMulte, string.Empty, DateTime.Now, n, string.Empty));
                        //更新积分
                        JuSNS.Home.User.User.Instance.UpdateInte(this.GetUserID(), Public.JSplit(33), 0, 0, "发起团购");
                        context.Put("redirecturl", "../shop/multebuy" + ExName);
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
