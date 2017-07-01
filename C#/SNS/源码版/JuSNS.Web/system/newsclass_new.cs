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
    public class newsclass_new : ManagePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref NVelocity.VelocityContext context)
        {
            base.Page_Loadno(ref context);
            int classid = GetInt("classid", 0);
            int sid = 0;
            if (classid > 0)
            {
                context.Put("cpagetitle", "修改分类");
                NewsChannelInfo info = JuSNS.Home.App.News.Instance.GetNewsChannelInfo(classid);
                context.Put("ChannelName", info.ChannelName);
                context.Put("OrderID", info.OrderID);
                sid = info.ParentID;
            }
            else
            {
                context.Put("cpagetitle", "添加分类");
            }
            context.Put("classlist", GetClassList(0, string.Empty, sid));
        }

        protected string GetClassList(int parentid, string TmpSTR,int sid)
        {
            string listSTR = string.Empty;
            List<NewsChannelInfo> Infolist = JuSNS.Home.App.News.Instance.GetNewsChannel(parentid, 0);
            foreach (NewsChannelInfo info in Infolist)
            {
                if (sid == info.Id)
                {
                    listSTR += "<option value=\"" + info.Id + "\" selected>" + TmpSTR + "" + info.ChannelName + "</option>";
                }
                else
                {
                    listSTR += "<option value=\"" + info.Id + "\">" + TmpSTR + "" + info.ChannelName + "</option>";
                }
                listSTR += GetClassList(info.Id, TmpSTR + "---", sid);
            }
            return listSTR;
        }

        public override void Page_PostBack(ref VelocityContext context)
        {
            string channelName = GetString("channelName");
            if (string.IsNullOrEmpty(channelName))
            {
                context.Put("errors", "请填写分类标题");
            }
            else
            {
                NewsChannelInfo info = new NewsChannelInfo();
                info.ChannelName = channelName;
                info.ChannelType = 0;
                info.Id = GetInt("classid", 0);
                info.OrderID = GetInt("OrderID", 0);
                info.ParentID = GetInt("parentid", 0);
                info.PerPageNumber = 20;
                info.Pic = string.Empty;
                int n = JuSNS.Home.App.News.Instance.InsertUpdateNewsClass(info);
                if (n > 0)
                {
                    context.Put("redirecturl", "newsclass" + ExName);
                }
                else
                {
                    context.Put("errors", "发生错误");
                }
                ShowInfo(ref context);
            }
        }
    }
}

