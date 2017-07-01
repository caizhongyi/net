using System.Collections.Generic;
using System.Xml;
using System.Web;

namespace JuSNS.Config
{
    /// <summary>
    /// 用户API文件配置读取
    /// </summary>
    static public class UserApiConfig
    {
        static private readonly string path = HttpContext.Current.Request.ApplicationPath + "/config/api/UserApi.config";
        static private Dictionary<string, string> _SysConfig = new Dictionary<string, string>();
        static private Dictionary<int, string> _AppType = new Dictionary<int, string>();
        /// <summary>
        /// 取得用户API系统配置
        /// </summary>
        static public Dictionary<string, string> SysConfig
        {
            get { return _SysConfig; }
        }
        /// <summary>
        /// 应用类别
        /// </summary>
        static public Dictionary<int, string> AppType
        {
            get { return _AppType; }
        }
        /// <summary>
        /// 取得应用类别
        /// </summary>
        /// <param name="type">类别编号</param>
        /// <returns></returns>
        static public string GetType(int type)
        {
            return _AppType[type];
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        static UserApiConfig()
        {
            Reload();
        }
        /// <summary>
        /// 重新加载配置文件
        /// </summary>
        static public void Reload()
        {
            _SysConfig.Clear();
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.Load(HttpContext.Current.Request.MapPath(path));
            XmlNodeList Nodes = XmlDoc.SelectNodes("/config/sysparam/add");
            foreach (XmlNode node in Nodes)
            {
                XmlAttributeCollection xac = node.Attributes;
                string key = xac["key"].InnerText;
                string value = xac["value"].InnerText;
                _SysConfig.Add(key, value);
            }
            Nodes = XmlDoc.SelectNodes("/config/apptype/add");
            foreach (XmlNode node in Nodes)
            {
                XmlAttributeCollection xac = node.Attributes;
                int key = int.Parse(xac["key"].InnerText);
                string value = xac["value"].InnerText;
                _AppType.Add(key, value);
            }
        }
    }
}
