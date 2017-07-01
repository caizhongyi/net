using System;
using System.Collections.Generic;
using System.Web;
using System.Xml;
//using JuSNS.Common;

namespace JuSNS.Config
{
    /// <summary>
    /// 图片缩略图相关的配置
    /// </summary>
    public class PicConfig
    {
        #region 私有变量
        static private readonly string configpath = HttpContext.Current.Request.ApplicationPath + "/config/photo/photo.config";
        static private string _protraitserv;
        static private string _photoserv;
        static private string _protraitdir;
        static private string _photodir;
        static private Dictionary<string, PicConfigInfo> _portrait = new Dictionary<string, PicConfigInfo>();
        static private Dictionary<string, PicConfigInfo> _photo = new Dictionary<string, PicConfigInfo>();
        static private string _blogserv;
        static private string _blogorigdir;
        static private string _blogbrevdir;
        static private Dictionary<string, PicConfigInfo> _GroupHead = new Dictionary<string, PicConfigInfo>();
        static private string _GroupServer;
        static private string _GroupDir;
        static private string _SoapPass = "JuSNS";
        static private string _AppServer;
        static private string _AppDir;
        /// <summary>
        /// 应用图片服务器
        /// </summary>
        static public string AppServer
        {
            get { return _AppServer; }
        }
        /// <summary>
        /// 应用目录
        /// </summary>
        static public string AppDir
        {
            get { return _AppDir; }
        }
        #endregion
        /// <summary>
        /// 静态构造函数
        /// </summary>
        static PicConfig()
        {
            Reload();
        }
        /// <summary>
        /// 重新读取Config文件，重新取值
        /// </summary>
        static public void Reload()
        {
            string filepath = HttpContext.Current.Server.MapPath(configpath);
            XmlDocument xml = new XmlDocument();
            xml.Load(filepath);
            #region 头象
            XmlNode rootpor = xml.SelectSingleNode("picture/portrait");
            _protraitserv = rootpor.Attributes["server"].Value;
            _protraitdir = rootpor.Attributes["originaldir"].Value;
            foreach (XmlNode n in rootpor.ChildNodes)
            {
                if (n.NodeType != XmlNodeType.Comment)
                {
                    if (n.Name.ToLower() == "breviary")
                    {
                        XmlAttribute name = n.Attributes["name"];
                        XmlAttribute dir = n.Attributes["directory"];
                        XmlAttribute wid = n.Attributes["width"];
                        XmlAttribute hei = n.Attributes["height"];
                        XmlAttribute pri = n.Attributes["priority"];
                        PicConfigInfo info = new PicConfigInfo();
                        info.Directory = dir.Value;
                        if (string.IsNullOrEmpty(wid.Value))
                        {
                            info.X = 1;
                        }
                        else
                        {
                            info.X = Convert.ToInt32(wid.Value);
                        }
                        if (string.IsNullOrEmpty(hei.Value))
                        {
                            info.Y = 1;
                        }
                        else
                        {
                            info.Y = Convert.ToInt32(hei.Value);
                        }
                        if (string.IsNullOrEmpty(pri.Value))
                        {
                            info.Priority = 1;
                        }
                        else
                        {
                            info.Priority = Convert.ToInt32(pri.Value);
                        }
                        try
                        {
                            _portrait.Add(name.Value, info);
                        }
                        catch
                        {
                        }
                    }
                }
            }
            #endregion
            #region 照片

            XmlNode rootpho = xml.SelectSingleNode("picture/photo");
            _photoserv = rootpho.Attributes["server"].Value;
            _photodir = rootpho.Attributes["originaldir"].Value;

            XmlNode soapNode = xml.SelectSingleNode("picture/soappass");
            if (soapNode != null)
            {
                _SoapPass = soapNode.InnerText;
            }
            foreach (XmlNode n in rootpho.ChildNodes)
            {
                if (n.NodeType != XmlNodeType.Comment)
                {
                    if (n.Name.ToLower() == "breviary")
                    {
                        XmlAttribute name = n.Attributes["name"];
                        XmlAttribute dir = n.Attributes["directory"];
                        XmlAttribute wid = n.Attributes["width"];
                        XmlAttribute hei = n.Attributes["height"];
                        XmlAttribute pri = n.Attributes["priority"];
                        PicConfigInfo info = new PicConfigInfo();
                        info.Directory = dir.Value;
                        if (string.IsNullOrEmpty(wid.Value))
                        {
                            info.X = 1;
                        }
                        else
                        {
                            info.X = Convert.ToInt32(wid.Value);
                        }
                        if (string.IsNullOrEmpty(hei.Value))
                        {
                            info.Y = 1;
                        }
                        else
                        {
                            info.Y = Convert.ToInt32(hei.Value);
                        }
                        if (string.IsNullOrEmpty(pri.Value))
                        {
                            info.Priority = 1;
                        }
                        else
                        {
                            info.Priority = Convert.ToInt32(pri.Value);
                        }
                        try
                        {
                            _photo.Add(name.Value, info);
                        }
                        catch
                        {
                        }
                    }
                }
            }
            #endregion
            #region 群组头像
            XmlNode rootgroup = xml.SelectSingleNode("picture/group");
            _GroupServer = rootgroup.Attributes["server"].Value;
            _GroupDir = rootgroup.Attributes["originaldir"].Value;
            foreach (XmlNode n in rootgroup.ChildNodes)
            {
                if (n.NodeType != XmlNodeType.Comment)
                {
                    if (n.Name.ToLower() == "breviary")
                    {
                        XmlAttribute name = n.Attributes["name"];
                        XmlAttribute dir = n.Attributes["directory"];
                        XmlAttribute wid = n.Attributes["width"];
                        XmlAttribute hei = n.Attributes["height"];
                        XmlAttribute pri = n.Attributes["priority"];
                        PicConfigInfo info = new PicConfigInfo();
                        info.Directory = dir.Value;
                        if (string.IsNullOrEmpty(wid.Value))
                        {
                            info.X = 1;
                        }
                        else
                        {
                            info.X = Convert.ToInt32(wid.Value);
                        }
                        if (string.IsNullOrEmpty(hei.Value))
                        {
                            info.Y = 1;
                        }
                        else
                        {
                            info.Y = Convert.ToInt32(hei.Value);
                        }
                        if (string.IsNullOrEmpty(pri.Value))
                        {
                            info.Priority = 1;
                        }
                        else
                        {
                            info.Priority = Convert.ToInt32(pri.Value);
                        }
                        try
                        {
                            _GroupHead.Add(name.Value, info);
                        }
                        catch
                        {
                        }
                    }
                }
            }
            #endregion

            #region 日志
            XmlNode rootblog = xml.SelectSingleNode("picture/blogimg");
            _blogserv = rootblog.Attributes["server"].Value;
            _blogorigdir = rootblog.Attributes["originaldir"].Value;
            _blogbrevdir = rootblog.Attributes["breviary"].Value;
            #endregion
            #region app
            XmlNode appNode = xml.SelectSingleNode("picture/app");
            _AppDir = appNode.Attributes["originaldir"].Value;
            _AppServer = appNode.Attributes["server"].Value;
            #endregion
        }

        /// <summary>
        /// 获取原始图片根目录
        /// </summary>
        static public string PhotoRoot
        {
            get { return _photodir; }
        }
        /// <summary>
        /// 获取原始头象根目录
        /// </summary>
        static public string ProtraitRoot
        {
            get { return _protraitdir; }
        }
        /// <summary>
        /// 获取头象服务器
        /// </summary>
        static public string ProtraitServer
        {
            get { return _protraitserv; }
        }
        /// <summary>
        /// 获取照片服务器
        /// </summary>
        static public string PhotoServer
        {
            get { return _photoserv; }
        }
        /// <summary>
        /// 头象缩略图配置信息
        /// </summary>
        static public Dictionary<string, PicConfigInfo> Portrait
        {
            get { return _portrait; }
        }
        /// <summary>
        /// 照片缩略图配置信息
        /// </summary>
        static public Dictionary<string, PicConfigInfo> Photo
        {
            get { return _photo; }
        }
        /// <summary>
        /// 日志图片服务器
        /// </summary>
        static public string BlogServer
        {
            get { return _blogserv; }
        }
        /// <summary>
        /// 原始日志图片根目录
        /// </summary>
        static public string BlogOrigDir
        {
            get { return _blogorigdir; }
        }
        /// <summary>
        /// 缩略日志图片根目录
        /// </summary>
        static public string BlogBrevDir
        {
            get { return _blogbrevdir; }
        }
        /// <summary>
        /// 头像配置
        /// </summary>
        static public Dictionary<string, PicConfigInfo> GroupHead
        {
            get { return _GroupHead; }
        }
        /// <summary>
        /// 群组头像目录
        /// </summary>
        static public string GroupDir
        {
            get { return _GroupDir; }
        }
        /// <summary>
        /// 群组头像服务器
        /// </summary>
        static public string GroupServer
        {
            get { return _GroupServer; }
        }
        /// <summary>
        /// 取得头象的相关的缩略图信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        static public PicConfigInfo GetPortrtBrevi(string name)
        {
            PicConfigInfo value;
            if (_portrait.TryGetValue(name, out value))
                return value;
            else
                return null;
        }
        /// <summary>
        /// 获取照片相关的缩略图信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        static public PicConfigInfo GetPhotoBrevi(string name)
        {
            PicConfigInfo value;
            if (_photo.TryGetValue(name, out value))
                return value;
            else
                return null;
        }
        /// <summary>
        /// 取得soappass
        /// </summary>
        static public string SoapPass
        {
            get { return _SoapPass; }
        }
    }
    /// <summary>
    /// 照片,头像相关的配置信息
    /// </summary>
    public class PicConfigInfo
    {
        private string _directory;
        private int _x;
        private int _y;
        private int _priority;
        /// <summary>
        /// 宽度
        /// </summary>
        public int X
        {
            get { return _x; }
            set { _x = value; }
        }
        /// <summary>
        /// 保存目标
        /// </summary>
        public string Directory
        {
            get { return _directory; }
            set { _directory = value; }
        }
        /// <summary>
        /// 高度
        /// </summary>
        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }
        /// <summary>
        /// 优先,0表示宽度优先,否则表示高度优先
        /// </summary>
        public int Priority
        {
            get { return _priority; }
            set { _priority = value; }
        }

    }
}
