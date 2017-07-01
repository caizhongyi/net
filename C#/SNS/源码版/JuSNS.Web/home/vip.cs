using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.home
{
    public class vip:UserPage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        public void ShowInfo(ref VelocityContext context)
        {
            base.Page_Load(ref context);
            context.Put("cpagetitle", "VIP会员");
            UserInfo mdl = JuSNS.Home.User.User.Instance.GetUserInfo(this.UserID);
            JoinVipInfo jmdl = JuSNS.Home.User.User.Instance.GetVipInfo(this.UserID);
            if (mdl.IsVip)
            {
                context.Put("isvip", true);
                context.Put("today", jmdl.EndTime.ToString("yyyy-MM-dd"));
            }
            else
            {
                if (jmdl == null)
                {
                    context.Put("button", "<input type=\"button\" value=\"申请VIP会员\" class=\"btn_blue10\" onclick=\"JoinVip(" + this.UserID + ",'"+DateTime.Now.AddMonths(12).ToString("yyyy-MM-dd")+"')\" />");
                }
                else
                {
                    switch (jmdl.IsLock)
                    {
                        case (byte)EnumCusState.ForLock:
                            context.Put("button", "VIP会员审核中...");
                            break;
                        case (byte)EnumCusState.ForStop:
                            context.Put("button", "VIP会员已停止");
                            break;
                        case (byte)EnumCusState.ForUnPass:
                            context.Put("button", "VIP会员未通过审核");
                            break;
                    }
                }
            }
        }
    }
}
