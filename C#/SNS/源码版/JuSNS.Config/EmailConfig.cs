using System.Text.RegularExpressions;
using System.Web;
using System.Xml;

namespace JuSNS.Config
{
    /// <summary>
    /// �ʼ���ص�����
    /// </summary>
    public class EmailConfig
    {
        #region ˽�б���
        static private readonly string configpath = HttpContext.Current.Request.ApplicationPath + "/config/email/email.config";
        static private string _mode;
        static private string _host;
        static private int _port;
        static private string _from;
        static private string _username;
        static private string _password;
        static private bool _enablessl;
        #endregion
        /// <summary>
        /// ��̬���캯��
        /// </summary>
        static EmailConfig()
        {
            Reload();
        }
        /// <summary>
        /// ���¶�ȡConfig�ļ�������ȡֵ
        /// </summary>
        static public void Reload()
        {
            string filepath = HttpContext.Current.Server.MapPath(configpath);
            XmlDocument xml = new XmlDocument();
            xml.Load(filepath);
            XmlNode rootpor = xml.SelectSingleNode("email/setting");
            _mode = rootpor.Attributes["current"].Value;
            foreach (XmlNode n in rootpor.ChildNodes)
            {
                if (n.NodeType != XmlNodeType.Comment && n.Name.ToLower() == "parameter")
                {
                    _host = n.Attributes["host"].Value;
                    _port = int.Parse(n.Attributes["port"].Value);
                    _from = n.Attributes["from"].Value;
                    _username = n.Attributes["username"].Value;
                    _password = n.Attributes["password"].Value;
                    if (n.Attributes["enablessl"].Value.ToLower() == "true")
                        _enablessl = true;
                    else
                        _enablessl = false;
                }
            }
        }
        /// <summary>
        /// �ʼ������ʽ
        /// </summary>
        static public string mode
        {
            get { return _mode; }
        }
        /// <summary>
        /// ������
        /// </summary>
        static public string host
        {
            get { return _host; }
        }
        /// <summary>
        /// �˿�
        /// </summary>
        static public int port
        {
            get { return _port; }
        }
        /// <summary>
        /// �����ʼ���ַ
        /// </summary>
        static public string from
        {
            get { return _from; }
        }
        /// <summary>
        /// �û���
        /// </summary>
        static public string username
        {
            get { return _username; }
        }
        /// <summary>
        /// ����
        /// </summary>
        static public string password
        {
            get { return _password; }
        }
        /// <summary>
        /// SSL����
        /// </summary>
        static public bool enablessl
        {
            get { return _enablessl; }
        }
        
        /// <summary>
        /// �����һص��ʼ�����
        /// </summary>
        static public string retrieve
        {
            get { return BaseConfig.GetConfigValue("matter/retrievepwd", HttpContext.Current.Server.MapPath(configpath), true); }
        }
        /// <summary>
        /// ע�ᷢ���ʼ�����
        /// </summary>
        static public string register
        {
            get { return BaseConfig.GetConfigValue("matter/register", HttpContext.Current.Server.MapPath(configpath), true); }
        }

        /// <summary>
        /// ע�ἤ���ʼ�����
        /// </summary>
        static public string emailactive
        {
            get { return BaseConfig.GetConfigValue("matter/emailactive", HttpContext.Current.Server.MapPath(configpath), true); }
        }

        /// <summary>
        /// �޸ĵ����ʼ����͵��ʼ�����
        /// </summary>
        static public string modifyemail
        {
            get { return BaseConfig.GetConfigValue("matter/modifyemail", HttpContext.Current.Server.MapPath(configpath), true); }
        }

        /// <summary>
        /// ������Ѽ����ʼ�����
        /// </summary>
        static public string invite
        {
            get { return BaseConfig.GetConfigValue("matter/invite", HttpContext.Current.Server.MapPath(configpath), true); }
        }
        /// <summary>
        /// ȡ�������Ӧ�ĵ�¼��ҳ��ַ
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        static public string GetEmailLoginUrl(string emailAddress)
        {
            if (emailAddress == null)
                return string.Empty;
            string addr = Regex.Match(emailAddress, "@.+$", RegexOptions.Compiled).Value;
            if (addr == null || addr.Trim() == string.Empty)
                return string.Empty;
            string filepath = HttpContext.Current.Server.MapPath(configpath);
            XmlDocument xml = new XmlDocument();
            xml.Load(filepath);
            XmlNode rootpor = xml.SelectSingleNode("email/loginurl");
            foreach (XmlNode n in rootpor.ChildNodes)
            {
                if (n.NodeType != XmlNodeType.Comment && n.Name.ToLower() == "email")
                {
                    string s = n.Attributes["address"].Value;
                    if (s.ToLower() == addr.ToLower())
                        return n.InnerText;
                }
            }
            return string.Empty;
        }
    }
}
