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
    public class newsclass : ManagePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref NVelocity.VelocityContext context)
        {
            base.Page_Loadno(ref context);
            context.Put("cpagetitle", "资讯分类管理 - 管理中心");
            context.Put("classlist", GetClassList(0, string.Empty));
        }

        protected string GetClassList(int parentid, string TmpSTR)
        {
            string listSTR = string.Empty;
            List<NewsChannelInfo> Infolist = JuSNS.Home.App.News.Instance.GetNewsChannel(parentid, 0);
            foreach (NewsChannelInfo info in Infolist)
            {
                listSTR += "<tr id=\"param_" + info.Id + "\">";
                listSTR += "<td>" + TmpSTR + " <a href=\"newsclass_new"+ExName+"?classid=" + info.Id + "\" title=\"点击修改\">" + info.ChannelName + "</a></td>";
                listSTR += "<td style=\"text-align:center;\">"+info.OrderID+"</td>";
                listSTR += "<td><input  type=\"button\" value=\"修改\" onclick=\"window.location.href='newsclass_new" + ExName + "?classid=" + info.Id + "'\" class=\"btn_blue2\" /> <input  type=\"button\" value=\"删除\" onclick=\"deleteAll(" + info.Id + "," + this.UserID + ",'newsclass');\" class=\"btn_blue2\" /></td>";
                listSTR += "</tr>";
                listSTR += GetClassList(info.Id, TmpSTR + "---");
            }
            return listSTR;
        }
    }
}
