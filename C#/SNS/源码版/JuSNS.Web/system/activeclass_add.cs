using System.Web;
using System.Collections.Generic;
using System.IO;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;


namespace JuSNS.Web.system
{
    public class activeclass_add : ManagePage
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
                AtiveClassInfo info = JuSNS.Home.App.Ative.Instance.GetAtiveClassInfo(classid);
                sid = info.ParentID;
                context.Put("ClassName", info.ClassName);
            }
            else
            {
                context.Put("cpagetitle", "添加分类");
            }
            context.Put("classlist", GetClassList(0, string.Empty, sid));
        }

        protected string GetClassList(int parentid, string TmpSTR, int sid)
        {
            string listSTR = string.Empty;
            List<AtiveClassInfo> infolist = JuSNS.Home.App.Ative.Instance.GetAtiveClassList(parentid);
            foreach (AtiveClassInfo info in infolist)
            {
                if (sid == info.Id)
                {
                    listSTR += "<option value=\"" + info.Id + "\" selected>" + TmpSTR + info.ClassName + "</option>";
                }
                else
                {
                    listSTR += "<option value=\"" + info.Id + "\">" + TmpSTR + info.ClassName + "</option>";
                }
                listSTR += GetClassList(info.Id, TmpSTR + "---", sid);
            }
            return listSTR;
        }

        public override void Page_PostBack(ref VelocityContext context)
        {
            AtiveClassInfo info = new AtiveClassInfo();
            info.Id = GetInt("classid", 0);
            info.ClassName = GetString("ClassName");
            if (string.IsNullOrEmpty(GetString("ClassName")))
            {
                context.Put("errors", "填写分类名称");
            }
            else
            {
                info.ParentID = GetInt("parentid", 0);
                int n = JuSNS.Home.App.Ative.Instance.InsetAtiveClass(info);
                if (n > 0)
                {
                    context.Put("redirecturl", "active" + ExName);
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
