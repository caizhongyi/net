using System.Web;
using System.Xml;

namespace JuSNS.Config
{
    public class emailsetConfig
    {
        #region ˽�б���
        static private readonly string configpath = HttpContext.Current.Request.ApplicationPath + "/config/email/emailset.config";
        static private string _letter;
        #endregion
        /// <summary>
        /// ��̬���캯��
        /// </summary>
        static emailsetConfig()
        {
            Reload();
        }
        /// <summary>
        /// ���¶�ȡConfig�ļ�������ȡֵ
        /// </summary>
        static public void Reload()
        {
            string filepath = HttpContext.Current.Server.MapPath(configpath);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filepath);//���xml�ļ�

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
        /// �ʼ�����
        /// </summary>
        static public string letter
        {
            get { return _letter; }
        }
    }
}
