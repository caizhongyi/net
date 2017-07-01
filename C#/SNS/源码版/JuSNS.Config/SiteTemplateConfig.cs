using System.IO;
using System.Web;

namespace JuSNS.Config
{
    /// <summary>
    /// 模板配置
    /// </summary>
    public class SiteTemplateConfig
    {
        private string _TemplateName;
        private string _HTML;
        /// <summary>
        /// 模板内容
        /// </summary>
        public string HTML
        {
            get { return _HTML; }
        }
        /// <summary>
        /// 取得模板内容
        /// </summary>
        /// <param name="tempname">模板名称</param>
        public SiteTemplateConfig(string tempname)
        {
            _TemplateName = tempname;
            gethtml();
        }

        private void gethtml()
        {
            string path = HttpContext.Current.Request.ApplicationPath + "/template/site/" + _TemplateName + ".html";
            StreamReader sr = new StreamReader(HttpContext.Current.Server.MapPath(path));
            _HTML = sr.ReadToEnd();
            sr.Close();
            sr.Dispose();
        }
    }
}
