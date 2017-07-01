using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using JuSNS.Common;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.system
{
    public class giftclass : ManagePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref NVelocity.VelocityContext context)
        {
            base.Page_Loadno(ref context);
            context.Put("cpagetitle", "礼物分类 - 管理中心");
            context.Put("classlist", ShowClassList());
        }

        protected string ShowClassList()
        {
            string listSTR = string.Empty;
            List<GiftClassInfo> infolist = JuSNS.Home.User.User.Instance.GetGiftClassList();
            foreach (GiftClassInfo info in infolist)
            {
                listSTR += "<li id=\"param_" + info.Id + "\"><input  type=\"button\" value=\"修改\" onclick=\"window.location.href='giftclass_add" + ExName + "?classid=" + info.Id + "'\" class=\"btn_blue2\" /><input  type=\"button\" value=\"删除\" onclick=\"deleteAll(" + info.Id + "," + this.UserID + ",'giftclass');\" class=\"btn_blue2\" /> <a href=\"giftclass_add" + ExName + "?classid=" + info.Id + "\" title=\"点击修改\">" + info.ClassName + "</a></li>";
            }
            return listSTR;
        }
    }
}
