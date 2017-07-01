using System.Web;
using System.Xml;

namespace JuSNS.Config
{
    public class emailsetConfig
    {
        #region 私有变量
        static private readonly string configpath = HttpContext.Current.Request.ApplicationPath + "/config/email/emailset.config";
        static private string _letter;
        #endregion
        /// <summary>
        /// 静态构造函数
        /// </summary>
        static emailsetConfig()
        {
            Reload();
        }
        /// <summary>
        /// 重新读取Config文件，重新取值
        /// </summary>
        static public void Reload()
        {
            string filepath = HttpContext.Current.Server.MapPath(configpath);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filepath);//你的xml文件

            XmlNodeList xmlList = xmlDoc.SelectSingleNode("emailset").ChildNodes;
            foreach (XmlNode xmlNo in xmlList)
            {
                if (xmlNo.NodeType != XmlNodeType.Comment)
                {
                    XmlElement xe = (XmlElement)xmlNo;
                    {
                        if (xe.Name == "letter")
                        {
                            if (xe.InnerText != null && xe.InnerText != "")
                            {
                                _letter = xe.InnerText;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 邮件内容
        /// </summary>
        static public string letter
        {
            get { return _letter; }
        }
    }
}
