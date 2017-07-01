using System.Web;
using System.Collections.Generic;
using System.IO;
using JuSNS.Model;
using JuSNS.UI.Page;
using NVelocity;

namespace JuSNS.Web.system
{
    public class createArea : BasePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            base.Page_Loadno(ref context);
            context.Put("cpagetitle", "生成地区JS - 管理中心");
        }

        public override void Page_PostBack(ref VelocityContext context)
        {
            using (StreamWriter sw = new StreamWriter(HttpContext.Current.Server.MapPath(root + "/library/js/area.js"), false))
            {
                sw.WriteLine("var Area = new Array();");
                List<DictAreaInfo> infolist = JuSNS.Home.Other.Area.Instance.GetArea();
                int i = 0;
                foreach (DictAreaInfo info in infolist)
                {
                    sw.WriteLine("Area[" + i + "] = new Array(\"" + info.ParentID + "\",\"" + info.ID + "\", \"" + info.Name + "\");");
                    i++;
                }
                if (i > 0)
                {
                    context.Put("rights", "已经生成地区JS");
                }
                else
                {
                    context.Put("errors", "生成地区JS失败");
                }
                sw.Close();
                sw.Dispose();
                base.Page_Loadno(ref context);
                context.Put("cpagetitle", "生成地区JS - 管理中心");
            }
        }
    }
}
