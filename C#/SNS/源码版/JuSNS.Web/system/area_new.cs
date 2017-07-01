using System.Web;
using System.Collections.Generic;
using System.IO;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.system
{
    public class area_new : ManagePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref NVelocity.VelocityContext context)
        {
            base.Page_Loadno(ref context);
            int aid = GetInt("aid", 0);
            int sid = 0;
            if (aid > 0)
            {
                context.Put("cpagetitle", "修改地区");
                DictAreaInfo info = JuSNS.Home.Other.Area.Instance.GetAreaInfo(aid);
                sid = info.ParentID;
                context.Put("name", info.Name);
            }
            else
            {
                context.Put("cpagetitle", "添加地区");
            }
            context.Put("classlist", GetClassList(0, string.Empty, sid));
        }

        protected string GetClassList(int parentid, string TmpSTR, int sid)
        {
            string listSTR = string.Empty;
            List<DictAreaInfo> Infolist = JuSNS.Home.Other.Area.Instance.CityList(parentid);
            foreach (DictAreaInfo info in Infolist)
            {
                if (sid == info.ID)
                {
                    listSTR += "<option value=\"" + info.ID + "\" selected>" + TmpSTR + info.Name + "</option>";
                }
                else
                {
                    listSTR += "<option value=\"" + info.ID + "\">" + TmpSTR + info.Name + "</option>";
                }
                //listSTR += GetClassList(info.ID, TmpSTR + "---", sid);
            }
            return listSTR;
        }

        public override void Page_PostBack(ref VelocityContext context)
        {
            DictAreaInfo info = new DictAreaInfo();
            info.ID = GetInt("aid", 0);
            info.IsLock = false;
            info.Name = GetString("aname");
            if (string.IsNullOrEmpty(GetString("aname")))
            {
                context.Put("errors", "填写地区名称");
            }
            else
            {
                info.ParentID = GetInt("parentid", 0);
            }
            int n = JuSNS.Home.Other.Area.Instance.InsertArea(info);
            if (n > 0)
            {
                context.Put("redirecturl", "area" + ExName);
            }
            else
            {
                context.Put("errors", "操作失败");
            }
            ShowInfo(ref context);
        }

        protected void createareajs()
        {
            using (StreamWriter sw = new StreamWriter(HttpContext.Current.Server.MapPath(root + "/library/js/area.js"), true))
            {
                List<DictAreaInfo> infolist = JuSNS.Home.Other.Area.Instance.GetArea();
                int i = 0;
                foreach (DictAreaInfo info in infolist)
                {
                    sw.WriteLine("Area[" + i + "] = new Array(\"" + info.ParentID + "\",\"" + info.ID + "\", \"" + info.Name + "\");");
                    i++;
                }
            }
        }
    }
}
