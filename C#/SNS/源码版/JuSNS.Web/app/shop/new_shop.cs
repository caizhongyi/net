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
    public class new_shop : UserPage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
            int sid = GetInt("sid", 0);
            if (sid == 0)
            {
                ShopInfo mdl = JuSNS.Home.App.Shop.Instance.GetShopForUserID(this.UserID);
                if (mdl != null)
                {
                    PageError("您已经申请过店铺了，不能再申请！", root + "/app/shop/list" + ExName + "");
                }
            }
        }


        protected void ShowInfo(ref VelocityContext context)
        {
            int uid = this.UserID;
            int isOpen = Convert.ToInt16(Public.GetXMLShopValue("isopen"));
            if (isOpen == 0)
            {
                context.Put("redirecturl", root + "/library/page/open" + ExName + "?q=false&app=shop");
            }
            base.Page_Load(ref context);
            string shoptitle = Public.GetXMLShopValue("shopname");
            context.Put("shop", shoptitle);
            context.Put("photo", string.Empty);
            GetInfo(ref context, GetInt("sid", 0));
        }

        protected void GetInfo(ref VelocityContext context, int sid)
        {
            int classid = 0;
            int areadid = 0;
            int apid = 0;
            if (sid > 0)
            {
                byte ischeck = Convert.ToByte(Public.GetXMLShopValue("shopincheck"));
                if (ischeck == 1)
                {
                    context.Put("ischeck", true);
                }
                context.Put("cpagetitle", "修改店铺");
                ShopInfo mdl = JuSNS.Home.App.Shop.Instance.GetShopForID(sid);
                if (this.UserID != mdl.UserID)
                {
                    context.Put("errors", "错误的参数");
                }
                else
                {
                    classid = mdl.ClassID;
                    areadid = mdl.AreaID;
                    DictAreaInfo ainfo = JuSNS.Home.Other.Area.Instance.GetAreaInfo(mdl.AreaID);
                    //if (ainfo.ParentID > 0)
                    //{
                    //    areadid = JuSNS.Home.Other.Area.Instance.Info(ainfo.ParentID).ID;
                    //}
                    apid = ainfo.ParentID;
                    context.Put("AddRess", mdl.AddRess);
                    context.Put("CompanyName", mdl.CompanyName);
                    context.Put("content", mdl.Content);
                    context.Put("Faren", mdl.Faren);
                    context.Put("FarenMobile", mdl.FarenMobile);
                    context.Put("Fax", mdl.Fax);
                    context.Put("HasSerive", mdl.HasSerive);
                    context.Put("JoinCase", mdl.JoinCase);
                    context.Put("keywords1", mdl.Keywords.Split(',')[0]);
                    context.Put("keywords2", mdl.Keywords.Split(',')[1]);
                    context.Put("keywords3", mdl.Keywords.Split(',')[2]);
                    context.Put("LinkMan", mdl.LinkMan);
                    context.Put("Mobile", mdl.Mobile);
                    context.Put("photo", mdl.Pic);
                    context.Put("PostCode", mdl.PostCode);
                    context.Put("ShopAddress", mdl.ShopAddress);
                    context.Put("ShopName", mdl.ShopName);
                    context.Put("ShopRName", mdl.ShopRName);
                    context.Put("Tel", mdl.Tel);
                }
            }
            else
            {
                context.Put("cpagetitle", "店铺入驻");
            }
            context.Put("sitem", areadid);
            context.Put("areaid", apid);
            context.Put("classlist", ShowClassList(string.Empty, 0, classid));
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
            int sid = GetInt("sid", 0);
            int errornumber = 0;
            ShopInfo mdl = new ShopInfo();
            mdl.AddRess = GetString("AddRess");
            if (string.IsNullOrEmpty(GetString("AddRess")))
            {
                errornumber++;
            }
            int sltareaid = GetInt("sltareaid", 0);
            int sltareaid1 = GetInt("sltareaid1", 0);
            if (sltareaid == 0)
            {
                errornumber++;
            }
            if (sltareaid1 > 0)
            {
                mdl.AreaID = sltareaid1;
            }
            else
            {
                mdl.AreaID = sltareaid;
            }
            mdl.ClassID = GetInt("sltclassid", 0);
            mdl.Click = 0;
            if (string.IsNullOrEmpty(GetString("CompanyName")))
            {
                errornumber++;
            }
            mdl.CompanyName = GetString("CompanyName");
            mdl.Content = GetString("txtcontent");
            if (string.IsNullOrEmpty(GetString("txtcontent")))
            {
                errornumber++;
            }
            mdl.DownNumber = 0;
            mdl.Faren = GetString("Faren");
            if (string.IsNullOrEmpty(GetString("Faren")))
            {
                errornumber++;
            }
            mdl.FarenMobile = GetString("FarenMobile");
            mdl.Fax = GetString("Fax");
            mdl.Tel = GetString("Tel");
            if (string.IsNullOrEmpty(GetString("FarenMobile")) && string.IsNullOrEmpty(GetString("Tel")))
            {
                errornumber++;
            }
            mdl.HasSerive = Convert.ToByte(GetInt("HasSerive", 0));
            mdl.Id = sid;
            string passSTR = string.Empty;
            byte ischeck = Convert.ToByte(Public.GetXMLShopValue("shopincheck"));
            if (JuSNS.Home.User.User.Instance.GetUserInfo(this.UserID).IsAdmin > 0)
            {
                ischeck = 0;
            }
            if (ischeck == 1)
            {
                passSTR = "，需要审核";
            }
            mdl.IsLock = ischeck;
            mdl.JoinCase = GetString("JoinCase");
            mdl.Keywords = GetString("keywords1") + "," + GetString("keywords2") + "," + GetString("keywords3");
            mdl.LinkMan = GetString("linkMan");
            if (string.IsNullOrEmpty(GetString("linkMan")))
            {
                errornumber++;
            }
            mdl.Mobile = GetString("Mobile");
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
            mdl.PostCode = GetString("PostCode");
            mdl.PostIP = Public.GetClientIP();
            mdl.PostTime = DateTime.Now;
            mdl.IsRec = false;
            mdl.ShopAddress = GetString("ShopAddress");
            mdl.ShopName = GetString("ShopName");
            if (string.IsNullOrEmpty(GetString("ShopName")))
            {
                errornumber++;
            }
            mdl.ShopRName = GetString("ShopRName");
            mdl.TopNumber = 0;
            mdl.UserID = this.UserID;
            if (errornumber > 0)
            {
                context.Put("errors", "带*的必须填写");
            }
            else
            {
                int n = JuSNS.Home.App.Shop.Instance.InsertShop(mdl);
                if (n > 0)
                {
                    if (ischeck == 1)
                    {
                        if (sid == 0)
                        {
                            context.Put("rights", "申请店铺入驻成功" + passSTR);
                        }
                        else
                        {
                            context.Put("rights", "修改店铺成功！" + passSTR);
                        }
                    }
                    else
                    {
                        JuSNS.Home.User.User.Instance.InsertDyn(new DynInfo(0, this.GetUserID(), 0, (int)EnumDynType.CreatShop, string.Empty, DateTime.Now, n, string.Empty));
                        //更新积分
                        JuSNS.Home.User.User.Instance.UpdateInte(this.GetUserID(), Public.JSplit(35), 0, 0, "店铺入住");
                        context.Put("redirecturl", "../shop/list"+ExName+"?q=my");
                    }
                }
                else
                {
                    context.Put("errors", "操作失败");
                }
            }
            ShowInfo(ref context);
        }
    }
}
