using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Web;

namespace JuSNS.Config
{
    public class BaseConfig
    {
        /// <summary>
        /// 读JuSite.config取配置文件
        /// </summary>
        /// <param name="Target"></param>
        /// <returns></returns>
        static internal string GetConfigValue(string Target)
        {
            string path = HttpContext.Current.Server.MapPath("~/config/base/JuSite.config");
            return GetConfigValue(Target, path, false);
        }

        /// <summary>
        /// 读base.config取配置文件
        /// </summary>
        /// <param name="Target"></param>
        /// <returns></returns>
        static internal string GetBaseConfigValue(string Target)
        {
            string path = HttpContext.Current.Server.MapPath("~/config/base/base.config");
            return GetConfigValue(Target, path, false);
        }

        /// <summary>
        /// 读JuSite.config取配置文件
        /// </summary>
        /// <param name="Target"></param>
        /// <param name="ConfigPathName"></param>
        /// <returns></returns>
        static internal string GetConfigValue(string Target, string XmlPath, params bool[] cdata)
        {
            System.Xml.XmlDocument xdoc = new XmlDocument();
            xdoc.Load(XmlPath);
            XmlElement root = xdoc.DocumentElement;
            XmlNode node = root.SelectSingleNode(Target);
            if (node != null)
            {
                if (cdata != null && cdata[0])
                    return node.InnerText;
                else
                    return node.InnerXml;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
