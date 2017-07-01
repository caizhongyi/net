using System;
using System.Collections.Generic;
using System.Web;
using System.Xml;

namespace JuSNS.Common
{
    public class HttpModule : IHttpModule
    {
        /// <summary>
        /// 实现接口的 Init 方法
        /// </summary>
        /// <param name="app"></param>
        public void Init(HttpApplication app)
        {
            app.BeginRequest += new EventHandler(this.ModuleRewriter_BeginRequest);
        }

        /// <summary>
        /// 实现接口的 Dispose 方法
        /// </summary>
        public void Dispose() { }

        /// <summary>
        /// BeginRequest响应事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ModuleRewriter_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication app = (HttpApplication)sender;
            Rewrite(app.Request.Path, app);
        }

        /// <summary>
        /// URL重写
        /// </summary>
        /// <param name="requestedPath"></param>
        /// <param name="app"></param>
        protected void Rewrite(string requestedPath, HttpApplication app)
        {
            string urlstr = requestedPath.ToLower();
            string reqstr = string.Empty;
            HttpContext Context = app.Context;
            int pos = requestedPath.IndexOf('?');
            if (pos > 0)
            {
                urlstr = urlstr.Substring(0, pos);
                reqstr = requestedPath.Substring(pos);
            }
            SiteUrls site = SiteUrls.GetInstance();
            string newurl = site.GetValue(urlstr);
            if (newurl == null)
                return;
            else
                Context.RewritePath(newurl + reqstr);
        }
    }


    /// <summary>
    /// 站点伪Url信息类
    /// </summary>
    public class SiteUrls
    {
        #region 内部属性和方法

        private static object lockPad = new object();
        private static volatile SiteUrls instance = null;
        private Dictionary<string, string> _UrlList;

        public Dictionary<string, string> UrlList
        {
            get
            {
                return _UrlList;
            }
            set
            {
                _UrlList = value;
            }
        }
        public string GetValue(string key)
        {
            string value = null;
            if (_UrlList.TryGetValue(key, out value))
                return value;
            else
                return null;
        }
        private SiteUrls()
        {
            string SiteUrlsFile = HttpContext.Current.Server.MapPath("~/config/url.config");
            _UrlList = new Dictionary<string, string>();
            XmlDocument xml = new XmlDocument();
            xml.Load(SiteUrlsFile);
            XmlNode root = xml.SelectSingleNode("urls");
            foreach (XmlNode n in root.ChildNodes)
            {
                if (n.NodeType != XmlNodeType.Comment && n.Name.ToLower() == "rewrite")
                {
                    XmlAttribute pseudo = n.Attributes["pseudourl"];
                    XmlAttribute real = n.Attributes["realurl"];

                    if (pseudo != null && real != null)
                    {
                        if (!_UrlList.ContainsKey(pseudo.Value))
                            _UrlList.Add(pseudo.Value.ToLower(), real.Value);
                    }
                }
            }
        }
        #endregion

        public static SiteUrls GetInstance()
        {
            if (instance == null)
            {
                lock (lockPad)
                {
                    if (instance == null)
                    {
                        instance = new SiteUrls();
                    }
                }
            }
            return instance;
        }

        public static void SetInstance(SiteUrls anInstance)
        {
            if (anInstance != null)
                instance = anInstance;
        }

        public static void SetInstance()
        {
            SetInstance(new SiteUrls());
        }
    }
}
