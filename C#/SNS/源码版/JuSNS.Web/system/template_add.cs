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
    public class template_add : ManagePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        public void ShowInfo(ref VelocityContext context)
        {
            base.Page_Loadno(ref context);
            int tid = GetInt("tid", 0);
            if (tid > 0)
            {
                context.Put("cpagetitle", "修改空间模板");
                SpaceTemplateInfo info = JuSNS.Home.User.User.Instance.GetSpaceTemplate(tid);
                context.Put("TName", info.TName);
                context.Put("TEName", info.TEName);
                context.Put("IPoint", info.IPoint);
                context.Put("GPoint", info.GPoint);
                context.Put("UseNumber", info.UseNumber);
                context.Put("IsLock", info.IsLock);
            }
            else
            {
                context.Put("cpagetitle", "添加空间模板");
            }
            
        }

        public override void Page_PostBack(ref VelocityContext context)
        {
            string TName = GetString("TName");
            string TEName = GetString("TEName");
            if (string.IsNullOrEmpty(TName) || string.IsNullOrEmpty(TEName))
            {
                context.Put("errors", "模板名称和CSS目录必须填写");
            }
            else
            {
                SpaceTemplateInfo info = new SpaceTemplateInfo();
                info.GPoint = GetInt("GPoints", 0);
                info.ID = GetInt("tid", 0);
                info.IPoint = GetInt("IPoint", 0);
                info.IsLock = Convert.ToByte(GetInt("islock", 0));
                info.PostTime = DateTime.Now;
                info.TEName = TEName;
                info.TName = TName;
                info.UseNumber = GetInt("UseNumber", 0);
                int n = JuSNS.Home.User.User.Instance.InsertSpaceTemplate(info);
                if (n > 0)
                {
                    context.Put("redirecturl", "template" + ExName);
                }
                else
                {
                    context.Put("errors", "发生错误");
                }
            }
        }
    }
}
