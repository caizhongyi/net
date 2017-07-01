using System.Web;
using System.Xml;

namespace JuSNS.Config
{
    public class SysConfig
    {
        #region 私有变量
        static private readonly string configpath = HttpContext.Current.Request.ApplicationPath + "/config/sys/system.config";
        static private string _KeyWord;//关键字
        static private string _UserName;//姓名

        #endregion


         /// <summary>
        /// 静态构造函数
        /// </summary>
        static SysConfig()
        {
            Reload(true);
            Reload(false);
        }
        /// <summary>
        /// 重新读取Config文件，重新取值
        /// </summary>
        static public void Reload(bool fg)
        {
            string filepath = HttpContext.Current.Server.MapPath(configpath);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filepath);//你的xml文件
            string strNod = "forbid";
            string eque = "username";
            string reStr = "";
            if (fg == false)//关键字
            {
                strNod = "filter";
                eque = "keyword";
            }

            XmlNodeList xmlList = xmlDoc.SelectSingleNode("system/"+strNod).ChildNodes;
            bool flag = false;
            foreach (XmlNode xmlNo in xmlList)
            {
                if (xmlNo.NodeType != XmlNodeType.Comment)
                {
                    XmlElement xe = (XmlElement)xmlNo;
                    {
                        if (xe.Name == eque)
                        {
                            if (xe.InnerText != null && xe.InnerText != "")
                            {
                                if (!flag)
                                {
                                    reStr += xe.InnerText;
                                    flag = true;
                                }
                                else
                                {
                                    reStr += "|" + xe.InnerText;
                                }
                            }
                        }
                    }
                }
            }

            if (fg)
            {
                _UserName = reStr;
            }
            else
            {
                _KeyWord = reStr;
            }
        }

        /// <summary>
        /// 关键字
        /// </summary>
        static public string KeyWord
        {
            get { return _KeyWord; }
        }
        /// <summary>
        /// 姓名
        /// </summary>
        static public string UserName
        {
            get { return _UserName; }
        }
    }
}
