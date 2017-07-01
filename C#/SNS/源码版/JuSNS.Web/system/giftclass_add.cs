using System.Web;
using System.Collections.Generic;
using System.IO;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.system
{
    public class giftclass_add : ManagePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref NVelocity.VelocityContext context)
        {
            base.Page_Loadno(ref context);
            int classid = GetInt("classid", 0);
            if (classid > 0)
            {
                context.Put("cpagetitle", "修改分类");
                GiftClassInfo info = JuSNS.Home.User.User.Instance.GetGiftClassInfo(classid);
                context.Put("ClassName", info.ClassName);
            }
            else
            {
                context.Put("cpagetitle", "添加分类");
            }
        }


        public override void Page_PostBack(ref VelocityContext context)
        {
            GiftClassInfo info = new GiftClassInfo();
            info.Id = GetInt("classid", 0);
            info.ClassName = GetString("ClassName");
            if (string.IsNullOrEmpty(GetString("ClassName")))
            {
                context.Put("errors", "填写分类名称");
            }
            else
            {
                info.ParentID = GetInt("parentid", 0);
                int n = JuSNS.Home.User.User.Instance.InsertGiftClass(info);
                if (n > 0)
                {
                    context.Put("redirecturl", "giftclass" + ExName);
                }
                else
                {
                    context.Put("errors", "操作失败");
                }
            }
            ShowInfo(ref context);
        }
    }
}
