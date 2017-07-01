using System.Collections.Generic;
using System.Xml;
using System.Web;

namespace JuSNS.Config
{
    /// <summary>
    /// 用户动态的配置文件
    /// </summary>
    public class DynConfig
    {
        static private string _path
        {
            get
            {
                return HttpContext.Current.Request.ApplicationPath + "/config/base/dyn.config";
            }
        }
        static private Dictionary<string, string> _config = new Dictionary<string, string>();
        /// <summary>
        /// 返回配置
        /// </summary>
        static public Dictionary<string, string> Config
        {
            get { return _config; }
        }
        static DynConfig()
        {
            reload();
        }
        static public void reload()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(HttpContext.Current.Server.MapPath(_path));
            XmlNode node = doc.SelectSingleNode("configuration");
            XmlNodeList nodelist = node.ChildNodes;
            foreach (XmlNode n in nodelist)
            {
                _config.Add(n.Name, n.InnerText);
            }
        }
    }
}
