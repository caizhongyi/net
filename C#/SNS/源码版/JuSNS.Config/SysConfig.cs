using System.Web;
using System.Xml;

namespace JuSNS.Config
{
    public class SysConfig
    {
        #region ˽�б���
        static private readonly string configpath = HttpContext.Current.Request.ApplicationPath + "/config/sys/system.config";
        static private string _KeyWord;//�ؼ���
        static private string _UserName;//����

        #endregion


         /// <summary>
        /// ��̬���캯��
        /// </summary>
        static SysConfig()
        {
            Reload(true);
            Reload(false);
        }
        /// <summary>
        /// ���¶�ȡConfig�ļ�������ȡֵ
        /// </summary>
        static public void Reload(bool fg)
        {
            string filepath = HttpContext.Current.Server.MapPath(configpath);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filepath);//���xml�ļ�
            string strNod = "forbid";
            string eque = "username";
            string reStr = "";
            if (fg == false)//�ؼ���
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
        /// �ؼ���
        /// </summary>
        static public string KeyWord
        {
            get { return _KeyWord; }
        }
        /// <summary>
        /// ����
        /// </summary>
        static public string UserName
        {
            get { return _UserName; }
        }
    }
}
