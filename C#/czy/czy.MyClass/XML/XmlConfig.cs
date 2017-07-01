using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Web;
using System.Collections;

namespace czy.MyClass.XML
{
    public class XmlConfig : XmlDocument
    {
        public string XmlFilePath;
        public XmlConfig() : base() { }
        public XmlConfig(XmlImplementation imp) : base(imp) { }
        public XmlConfig(XmlNameTable nt) : base(nt) { }
        public XmlConfig(string filepath)
        {
            XmlFilePath = filepath;
            if (System.IO.File.Exists(filepath))
            {
                Load(filepath);
            }
        }
        /// <summary>
        /// 读入配置信息(字符串)
        /// </summary>
        /// <param name="path">节点路径</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>节点内联文本</returns>
        public string ReadString(string Path, string defaultValue)
        {
            try
            {
                XmlNode n = SelectSingleNode(Path);
                return (n == null || n.InnerText.Trim() == "") ? defaultValue : n.InnerText.Trim();
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// 读入配置信息(32位整数)
        /// </summary>
        /// <param name="path">节点路径</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>32位整数</returns>
        public Int32 ReadInt32(string Path, string defaultValue)
        {
            try
            {
                XmlNode n = SelectSingleNode(Path);
                string rt = (n == null || n.InnerText.Trim() == "") ? defaultValue : n.InnerText.Trim();
                return Convert.ToInt32(rt);
            }
            catch
            {
                return -1;
            }
        }
        /// <summary>
        /// 读入配置信息(布尔型)
        /// </summary>
        /// <param name="path">节点路径</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>布尔值</returns>
        public bool ReadBool(string path, bool defaultValue)
        {
            return bool.Parse(ReadString(path, defaultValue.ToString()).ToLower());
        }
        public void Save()
        {
            Save(XmlFilePath);
        }

        public void WriteString(string path, string value)
        {
            XmlNode n = SelectSingleNode(path);
            n.InnerText = value;
            Save();
        }

    }
}
