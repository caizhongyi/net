using System.Web;
using System.Collections.Generic;
using System.IO;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;


namespace JuSNS.Web.system
{
    public class blogclass_add : ManagePage
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
                context.Put("cpagetitle", "修改博客分类");
                BlogClassInfo info = JuSNS.Home.App.Blog.Instance.GetBlogClassInfo(classid);
                sid = info.ParentID;
                context.Put("className", info.CName);
            }
            else
            {
                context.Put("cpagetitle", "添加博客分类");
            }
        }

        public override void Page_PostBack(ref VelocityContext context)
        {
            BlogClassInfo info = new BlogClassInfo();
            info.Id = GetInt("classid", 0);
            info.CName = GetString("className");
            info.OrderID = 0;
            if (string.IsNullOrEmpty(GetString("className")))
            {
                context.Put("errors", "填写分类名称");
            }
            else
            {
                info.ParentID = 0;
                info.UserID = 0;
                int n = JuSNS.Home.App.Blog.Instance.InsertBlogClass(info);
                if (n > 0)
                {
                    context.Put("redirecturl", "blogclass" + ExName);
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
