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
    public class groupclass : ManagePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref NVelocity.VelocityContext context)
        {
            base.Page_Loadno(ref context);
            context.Put("cpagetitle", "社群分类管理 - 管理中心");
            context.Put("classlist", ShowClassList(0, string.Empty));
        }

        protected string ShowClassList(int parentid, string tmpstr)
        {
            string listSTR = string.Empty;
            List<GroupClassInfo> infolist = JuSNS.Home.App.Group.Instance.GetClassList(parentid);
            foreach (GroupClassInfo info in infolist)
            {
                listSTR += "<li id=\"param_" + info.ID + "\"><input  type=\"button\" value=\"修改\" onclick=\"window.location.href='groupclass_add"+ExName+"?classid="+info.ID+"'\" class=\"btn_blue2\" /><input  type=\"button\" value=\"删除\" onclick=\"deleteAll(" + info.ID + "," + this.UserID + ",'groupclass');\" class=\"btn_blue2\" /> <a href=\"groupclass_add" + ExName + "?classid=" + info.ID + "\" title=\"点击修改\">" + tmpstr + info.ClassName + "</a></li>";
                listSTR += ShowClassList(info.ID, tmpstr + " -- ");
            }
            return listSTR;
        }
    }
}
