using System.Web;
using JuSNS.UI.Page;
using NVelocity;
using System.IO;
using System.Text;

namespace JuSNS.Web.system
{
    public class sysconfig : ManagePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            ShowInfo(ref context);
        }

        protected void ShowInfo(ref NVelocity.VelocityContext context)
        {
            base.Page_Loadno(ref context);
            string path = GetString("path");
            byte isadmin = JuSNS.Home.User.User.Instance.GetUserInfo(this.UserID).IsAdmin;
            if (isadmin < 2)
            {
                context.Put("errors", "您权限不足！超级管理员才能操作。");
            }
            else
            {
                if (string.IsNullOrEmpty(path) || path.IndexOf("~/config") == -1)
                {
                    context.Put("errors", "错误的参数");
                }
                else
                {
                    context.Put("path", path);
                    context.Put("cpagetitle", "修改配置文件" + path + " - 管理中心");
                    StreamReader sr = new StreamReader(HttpContext.Current.Server.MapPath(path));
                    string s = string.Empty;
                    string ss = string.Empty;
                    s = sr.ReadLine();
                    string sss = sr.ReadToEnd(); ;
                    ss = s + "\r\n" + sss;
                    sr.Dispose();
                    context.Put("configcontent", ss);
                    if (path.ToLower() == "~/config/base/page.config")
                    {
                        context.Put("current2", " class=\"current\"");
                        context.Put("current1", string.Empty);
                        context.Put("current3", string.Empty);
                        context.Put("isindexconfig", true);
                    }
                    else if (path.ToLower() == "~/config/photo/photo.config")
                    {
                        context.Put("current1", string.Empty);
                        context.Put("current2", string.Empty);
                        context.Put("current3",  " class=\"current\"");
                    }
                    else
                    {
                        context.Put("current1", " class=\"current\"");
                        context.Put("current2", string.Empty);
                        context.Put("current3", string.Empty);
                    }
                }
            }
        }

        public override void Page_PostBack(ref VelocityContext context)
        {
            string path = GetString("path");
            string syscontent = GetString("configcontent");
            StreamWriter sw = new StreamWriter(new FileStream(HttpContext.Current.Server.MapPath(path), FileMode.Open), Encoding.UTF8);
            sw.WriteLine(syscontent);
            //sw.Write(sss);
            sw.Close();
            sw.Dispose();
            context.Put("rights", "保存成功！");
            ShowInfo(ref context);
        }
    }
}
