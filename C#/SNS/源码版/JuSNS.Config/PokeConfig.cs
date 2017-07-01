using System.Collections.Generic;
using System.Xml;
using System.Web;

namespace JuSNS.Config
{
    public class PokeConfig
    {
        static private string _path
        {
            get
            {
                return HttpContext.Current.Request.ApplicationPath + "/config/base/poke.config";
            }
        }
        static private Dictionary<int, PokeActionInfo> _config = new Dictionary<int, PokeActionInfo>();

        static public Dictionary<int, PokeActionInfo> Config
        {
            get { return _config; }
        }
        /// <summary>
        /// 配置个数
        /// </summary>
        static public int Count
        {
            get
            {
                return _config.Keys.Count;
            }
        }
        static PokeConfig()
        {
            reload();
        }
        static public void reload()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(HttpContext.Current.Server.MapPath(_path));
            XmlNodeList nodelist = doc.SelectNodes("/config/add");
            foreach (XmlNode n in nodelist)
            {
                XmlAttributeCollection xac = n.Attributes;
                try
                {
                    int key = int.Parse(xac["key"].Value);
                    string value = xac["value"].Value;
                    string more = xac["more"].Value;
                    PokeActionInfo info = new PokeActionInfo();
                    info.Number = key;
                    info.Value = value;
                    info.More = more;
                    _config.Add(key, info);
                }
                catch { }
            }
        }
    }
    /// <summary>
    /// 招呼信息
    /// </summary>
    public class PokeActionInfo
    {
        private int _Number;
        private string _Value;
        private string _More;
        /// <summary>
        /// 序号
        /// </summary>
        public int Number
        {
            get { return _Number; }
            set { _Number = value; }
        }
        /// <summary>
        /// 短招呼
        /// </summary>
        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }
        /// <summary>
        /// 招呼详细
        /// </summary>
        public string More
        {
            get { return _More; }
            set { _More = value; }
        }
    }
}
