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
    public class activeclass : ManagePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref NVelocity.VelocityContext context)
        {
            base.Page_Loadno(ref context);
            context.Put("cpagetitle", "活动分类管理 - 管理中心");
            context.Put("classlist", ShowClassList(0, string.Empty));
        }

        protected string ShowClassList(int parentid, string tmpstr)
        {
            string listSTR = string.Empty;
            List<AtiveClassInfo> infolist = JuSNS.Home.App.Ative.Instance.GetAtiveClassList(parentid);
            foreach (AtiveClassInfo info in infolist)
            {
                listSTR += "<li id=\"param_" + info.Id + "\"><input  type=\"button\" value=\"修改\" onclick=\"window.location.href='activeclass_add" + ExName + "?classid=" + info.Id + "'\" class=\"btn_blue2\" /><input  type=\"button\" value=\"删除\" onclick=\"deleteAll(" + info.Id + "," + this.UserID + ",'activeclass');\" class=\"btn_blue2\" /> <a href=\"activeclass_add" + ExName + "?classid=" + info.Id + "\" title=\"点击修改\">" + tmpstr + info.ClassName + "</a></li>";
                listSTR += ShowClassList(info.Id, tmpstr + " -- ");
            }
            return listSTR;
        }
    }
}
