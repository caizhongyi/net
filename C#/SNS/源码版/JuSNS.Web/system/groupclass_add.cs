using System.Web;
using System.Collections.Generic;
using System.IO;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;


namespace JuSNS.Web.system
{
    public class groupclass_add : ManagePage
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
                context.Put("cpagetitle", "修改群组分类");
                GroupClassInfo info = JuSNS.Home.App.Group.Instance.GetGroupClassInfo(classid);
                sid = info.Parentid;
                context.Put("className", info.ClassName);
            }
            else
            {
                context.Put("cpagetitle", "添加群组分类");
            }
            context.Put("classlist", GetClassList(0, string.Empty, sid));
        }

        protected string GetClassList(int parentid, string TmpSTR, int sid)
        {
            string listSTR = string.Empty;
            List<GroupClassInfo> infolist = JuSNS.Home.App.Group.Instance.GetClassList(parentid);
            foreach (GroupClassInfo info in infolist)
            {
                if (sid == info.ID)
                {
                    listSTR += "<option value=\"" + info.ID + "\" selected>" + TmpSTR + info.ClassName + "</option>";
                }
                else
                {
                    listSTR += "<option value=\"" + info.ID + "\">" + TmpSTR + info.ClassName + "</option>";
                }
                listSTR += GetClassList(info.ID, TmpSTR + "---", sid);
            }
            return listSTR;
        }

        public override void Page_PostBack(ref VelocityContext context)
        {
            GroupClassInfo info = new GroupClassInfo();
            info.ID = GetInt("classid", 0);
            info.ClassName = GetString("className");
            info.IsCreat = true;
            if (string.IsNullOrEmpty(GetString("className")))
            {
                context.Put("errors", "填写分类名称");
            }
            else
            {
                info.Parentid = GetInt("parentid", 0);
                int n = JuSNS.Home.App.Group.Instance.InsertGroupClass(info);
                if (n > 0)
                {
                    context.Put("redirecturl", "groupclass" + ExName);
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
