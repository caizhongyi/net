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
    public class area : ManagePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref NVelocity.VelocityContext context)
        {
            base.Page_Loadno(ref context);
            context.Put("cpagetitle", "地区管理 - 管理中心");
            context.Put("classlist", GetClassList(0, string.Empty));
        }

        protected string GetClassList(int parentid, string TmpSTR)
        {
            string listSTR = string.Empty;
            List<DictAreaInfo> Infolist = JuSNS.Home.Other.Area.Instance.CityList(parentid);
            foreach (DictAreaInfo info in Infolist)
            {
                listSTR += "<tr id=\"param_" + info.ID + "\">";
                listSTR += "<td>" + TmpSTR + " <a href=\"area_new" + ExName + "?aid=" + info.ID + "\" title=\"点击修改\">" + info.Name + "</a></td>";
                string LockSTR=string.Empty;
                if(info.IsLock)
                {
                    LockSTR="锁定";
                }
                else
                {
                    LockSTR="正常";
                }
                listSTR += "<td style=\"text-align:center;\">" + LockSTR + "</td>";
                listSTR += "<td><input  type=\"button\" value=\"修改\" onclick=\"window.location.href='area_new" + ExName + "?aid=" + info.ID + "'\" class=\"btn_blue2\" /> <input  type=\"button\" value=\"删除\" onclick=\"deleteAll(" + info.ID + "," + this.UserID + ",'area');\" class=\"btn_blue2\" /></td>";
                listSTR += "</tr>";
                listSTR += GetClassList(info.ID, TmpSTR + "---");
            }
            return listSTR;
        }
    }
}
