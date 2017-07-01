using System;
using System.Web;
using System.Collections.Generic;
using JuSNS.Common;
using JuSNS.Config;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.space.info
{
    public class links : UserPage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            HttpContext.Current.Response.CacheControl = "no-cache";
            HttpContext.Current.Response.Expires = 0;
            ShowInfo(ref context);
        }

        public void ShowInfo(ref VelocityContext context)
        {
            HttpContext.Current.Response.Expires = 0;
            base.Page_Loadno(ref context);
            context.Put("cpagetitle", "修改联系资料");
            UserInfo mdl = JuSNS.Home.User.User.Instance.GetUserInfo(this.UserID);
            context.Put("username", "用户名：" + mdl.UserName);
            context.Put("email", "电子邮件：" + mdl.Email);
            context.Put("emails", mdl.Email);
            context.Put("mobilesendcontent", "编写【" + Public.GetXMLValue("sendmobileNumber") + "】发送到 " + Public.GetXMLValue("sendMobileBind") + "，根据提示完成绑定。");
            if (!string.IsNullOrEmpty(mdl.Mobile))
            {
                context.Put("mobile", "手机：" + mdl.Mobile);
                string bindMobile = string.Empty;
                if (mdl.BindMoblie)
                {
                    bindMobile = "<a href=\"javascript:void(0);\" onclick=\"bindmobile('" + mdl.Mobile + "',0);\" title=\"取消绑定\">已绑定</a>";
                }
                else
                {
                    bindMobile = "<a href=\"javascript:void(0);\" onclick=\"bindmobile('" + mdl.Mobile + "',1);\" title=\"绑定手机\">未绑定</a>";
                }
                context.Put("bindmobile", "(" + bindMobile + ")");
            }
            else
            {
                context.Put("mobile", "手机：<a href=\"javascript:void(0);\" onclick=\"jQuery('#hideMobile').toggle();\" title=\"填写手机\">未填写</a>");
                context.Put("bindmobile", string.Empty);
            }
            this.GetBaseInfo(ref context,mdl);
        }

        public void GetBaseInfo(ref VelocityContext context, UserInfo mdl)
        {
            UserBaseInfo baseinfo = JuSNS.Home.User.User.Instance.GetUserBaseInfo(this.UserID);
            context.Put("emailpri", Public.GetPrivacy(baseinfo.EmailPrivacy));
            context.Put("msn", baseinfo.MSN);
            context.Put("msnpri", Public.GetPrivacy(baseinfo.MSNPrivacy));
            context.Put("qq", baseinfo.QQ);
            context.Put("qqpri", Public.GetPrivacy(baseinfo.QQPrivacy));
            context.Put("tel", baseinfo.Tel);
            context.Put("telpri", Public.GetPrivacy(baseinfo.TelPrivacy));
            context.Put("address", baseinfo.Addr);
            context.Put("addresspri", Public.GetPrivacy(baseinfo.AddrPrivacy));
            context.Put("website", baseinfo.WebSite);
            context.Put("websitepri", Public.GetPrivacy(baseinfo.WebSitePrivacy));
            #region 显示居住地
            context.Put("sitem", mdl.City);
            context.Put("areaid", mdl.ProvinceID);
            #endregion
        }


        public override void Page_PostBack(ref VelocityContext context)
        {
            string msn = GetString("txtmsn");
            string qq = GetString("txtqq");
            string tel = GetString("txttel");
            int homeprovince = GetInt("homeprovince", 0);
            int SlctCity = GetInt("SlctCity", 0);
            string address = GetString("txtaddress");
            string website = GetString("txtwebsite");
            int EmailPrivacy = GetInt("EmailPrivacy", 0);
            int MSNPrivacy = GetInt("MSNPrivacy", 0);
            int QQPrivacy = GetInt("QQPrivacy", 0);
            int TelPrivacy = GetInt("TelPrivacy", 0);
            int AddrPrivacy = GetInt("AddrPrivacy", 0);
            int WebSitePrivacy = GetInt("WebSitePrivacy", 0);
            UserInfo us = new UserInfo();
            us.UserID = this.UserID;
            us.ProvinceID = homeprovince;
            us.City = SlctCity;
            UserBaseInfo basi = new UserBaseInfo();
            basi.MSN = msn;
            basi.QQ = qq;
            basi.Tel = tel;
            basi.Addr = address;
            basi.WebSite = website;
            basi.EmailPrivacy = Convert.ToByte(EmailPrivacy);
            basi.MSNPrivacy = MSNPrivacy;
            basi.QQPrivacy = QQPrivacy;
            basi.TelPrivacy = TelPrivacy;
            basi.AddrPrivacy = AddrPrivacy;
            basi.WebSitePrivacy = WebSitePrivacy;
            if (JuSNS.Home.User.User.Instance.UpdateUserBasicInfo(us, basi) > 0)
            {
                context.Put("rights", "修改联系方式成功");
            }
            else
            {
                context.Put("errors", "保存失败");
            }
            ShowInfo(ref context);
        }
    }
}
