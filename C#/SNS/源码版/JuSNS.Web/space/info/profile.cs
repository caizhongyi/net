using System;
using System.Web;
using System.Collections.Generic;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.space.info
{
    public class profile : UserPage
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
            string f = GetQueryString("f");
            context.Put("flag", f);
            if (!string.IsNullOrEmpty(f) && f == "1") { context.Put("startlogin", true); context.Put("cpagetitle", "完善资料"); } else { context.Put("cpagetitle", "修改资料"); }
            UserInfo mdl = JuSNS.Home.User.User.Instance.GetUserInfo(this.UserID);
            context.Put("username", "用户名：" + mdl.UserName);
            context.Put("email", "电子邮件：" + mdl.Email);
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
            context.Put("truename", mdl.TrueName);
            context.Put("sex", mdl.Sex == 0 ? "男" : "女");
            context.Put("sexs", mdl.Sex == 0 ? "1" : "0");
            #region 婚恋状态
            string[] mstr = { "保密", "单身", "恋爱中", "订婚", "已婚", "离异" };
            string marrSTR = string.Empty;
            for (int j = 0; j < 6; j++)
            {
                if (mdl.Marriage == j)
                {
                    marrSTR += "<option value=\"" + j + "\" selected>" + mstr[j] + "</option>";
                }
                else
                {
                    marrSTR += "<option value=\"" + j + "\">" + mstr[j] + "</option>";
                }
            }
            context.Put("marriage", marrSTR);
            #endregion
            UserBaseInfo basi = JuSNS.Home.User.User.Instance.GetUserBaseInfo(this.UserID);
            context.Put("birthday", basi.Birthday.ToString("yyyy-M-d"));
            #region 显示生日格式
            string[] rstr = { "显示年月日", "显示月和日", "显示年", "不显示" };
            string dstr = string.Empty;
            for (int i = 0; i < 4; i++)
            {
                if (basi.BirthidayDisplay == i)
                {
                    dstr += "<option value=\"" + i + "\" selected>" + rstr[i] + "</option>";
                }
                else
                {
                    dstr += "<option value=\"" + i + "\">" + rstr[i] + "</option>";
                }
            }
            context.Put("birthprivacy", dstr);
            #endregion

            #region 显示星座
            string constinfostr = string.Empty;
            List<ConstellationInfo> cllinfo = JuSNS.Home.Other.Constellation.Instance.GetList();
            foreach (ConstellationInfo info in cllinfo)
            {
                if (basi.Constellation == info.Id)
                {
                    constinfostr += "<option value=\"" + info.Id + "\" selected>" + info.Constellation + "</option>";
                }
                else
                {
                    constinfostr += "<option value=\"" + info.Id + "\">" + info.Constellation + "</option>";
                }
            }
            context.Put("constellation", constinfostr);
            #endregion

            #region 显示家乡
            context.Put("areaid", JuSNS.Home.Other.Area.Instance.GetAreaInfo(basi.HomeCity).ParentID);
            context.Put("sitem", basi.HomeCity);
            #endregion
            #region 加载行业
            this.Votion(ref context, basi.Vocation);
            #endregion
        }



        public void Votion(ref VelocityContext context, int sid)
        {
            string lisSTR = string.Empty;
            IList<VocationInfo> list = JuSNS.Home.Other.Area.Instance.GetVotionList();
            foreach (VocationInfo info in list)
            {
                if (sid == info.ID)
                {
                    lisSTR += "<option value=\"" + info.ID + "\" selected>" + info.VocName + "</option>";
                }
                else
                {
                    lisSTR += "<option value=\"" + info.ID + "\">" + info.VocName + "</option>";
                }
            }
            context.Put("vocation", lisSTR);
        }

        public override void Page_PostBack(ref VelocityContext context)
        {
            string truename = GetString("truename");
            int marriage = GetInt("marriage", 0);
            string err = string.Empty;
            string tmpbirth = HttpContext.Current.Request.Form["birthday"];
            if (!JuSNS.Common.Input.IsDate(tmpbirth))
            {
                err += "日志格式不正确，正确格式为：1980-1-20";
            }
            else
            {
                DateTime gTime = DateTime.Parse(tmpbirth);
                int n = gTime.Year;
                int n1 = DateTime.Now.Year;
                int rn = n1 - n;
                if (rn < 10)
                {
                    err += "您年龄也太小了吧？年龄必须大于10岁！";
                }
                if (rn > 80)
                {
                    err += "您年龄也太大了吧？年龄必须小于80岁！";
                }
            }
            if (!string.IsNullOrEmpty(err))
            {
                context.Put("errors", err);
                ShowInfo(ref context);
            }
            else
            {
                DateTime birthday = Convert.ToDateTime(tmpbirth);
                int birthprivacy = GetInt("birthprivacy", 0);
                int constellation = GetInt("constellation", 0);
                int homeprovince = GetInt("homeprovince", 0);
                int SlctCity = GetInt("SlctCity", 0);
                if (SlctCity == 0)
                {
                    SlctCity = homeprovince;
                }
                int vocation = GetInt("vocation", 0);
                UserInfo us = new UserInfo();
                us.UserID = this.UserID;
                us.TrueName = truename;
                us.Marriage = Convert.ToByte(marriage);

                UserBaseInfo basi = new UserBaseInfo();
                basi.Birthday = birthday;
                basi.BirthidayDisplay = birthprivacy;
                basi.Constellation = constellation;
                basi.HomeCity = SlctCity;
                basi.Vocation = vocation;
                int m = JuSNS.Home.User.User.Instance.UpdateUserInfo(us, basi);
                ShowInfo(ref context);
                if (m > 0)
                {
                    if (!string.IsNullOrEmpty(GetString("hideflag")) && GetString("hideflag") == "1")
                    {
                        context.Put("redirecturl", "head" + ExName + "?f=1");
                    }
                    else
                    {
                        context.Put("rights", "保存基本资料成功");
                    }
                    JuSNS.Home.User.User.Instance.UpdateInte(this.UserID, Public.JSplit(17), 0, 0, "更新个人基本资料");
                    JuSNS.Home.User.User.Instance.InsertDyn(new DynInfo(0, this.UserID, 0, (int)EnumDynType.UpdateBasic, string.Empty, DateTime.Now, this.UserID, string.Empty));
                }
                else
                {
                    context.Put("errors", "保存失败");
                }
            }
        }
    }
}