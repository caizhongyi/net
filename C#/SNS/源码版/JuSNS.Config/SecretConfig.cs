using System;
using System.Web;
using System.Xml;

namespace JuSNS.Config
{
    /// <summary>
    /// ��ȫ��ص�����
    /// </summary>
    public class SecretConfig
    {
        #region ˽�б���
        static private readonly string configpath = HttpContext.Current.Request.ApplicationPath + "/config/sys/security.config";
        static private byte[] _userkey = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        static private byte[] _useriv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        static private byte[] _adminkey = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        static private byte[] _adminiv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        static private byte[] _inviteregkey = { 0x0A, 0x0B, 0x0C, 0x0E, 0xFF, 0x0D, 0x0B, 0x1A };
        static private byte[] _inviteregiv = { 0x22, 0x64, 0x14, 0xAB, 0x10, 0xA7, 0xC3, 0x2F };
        static private byte[] _userappkey ={ 0xF1, 0x12, 0xA3, 0xD1, 0xBA, 0x54, 0x2A, 0x88 };
        static private byte[] _userappiv ={ 0x04, 0xAE, 0x57, 0x83, 0x56, 0x28, 0x66, 0xA7 };
        static private char _userseparator = '&';
        static private char _adminseparator = '&';
        static private char _inviteregseparator = '#';
        static private char _userappseparator='|';
        #endregion
        /// <summary>
        /// ��̬���캯��
        /// </summary>
        static SecretConfig()
        {
            Reload();
        }
        /// <summary>
        /// ���¶�ȡConfig�ļ�������ȡֵ
        /// </summary>
        static public void Reload()
        {
            try
            {
                string filepath = HttpContext.Current.Server.MapPath(configpath);
                XmlDocument xml = new XmlDocument();
                xml.Load(filepath);
                XmlNode root = xml.SelectSingleNode("security");
                foreach (XmlNode n in root.ChildNodes)
                {
                    if (n.NodeType != XmlNodeType.Comment)
                    {
                        if (n.Name.ToLower() == "user")
                        {
                            XmlAttribute key = n.Attributes["key"];
                            XmlAttribute iv = n.Attributes["iv"];
                            XmlAttribute sign = n.Attributes["digisign"];
                            try
                            {
                                _userkey = GetBytes(key.Value, 8);
                                _useriv = GetBytes(iv.Value, 8);
                                _userseparator = char.Parse(sign.Value.Substring(0, 1));
                            }
                            catch
                            { }

                        }
                        else if (n.Name.ToLower() == "admin")
                        {
                            XmlAttribute akey = n.Attributes["key"];
                            XmlAttribute aiv = n.Attributes["iv"];
                            XmlAttribute asign = n.Attributes["digisign"];
                            try
                            {
                                _adminkey = GetBytes(akey.Value, 8);
                                _adminiv = GetBytes(aiv.Value, 8);
                                _adminseparator = char.Parse(asign.Value.Substring(0, 1));
                            }
                            catch
                            { }
                        }
                        else if (n.Name.ToLower() == "invitereg")
                        {
                            XmlAttribute ikey = n.Attributes["key"];
                            XmlAttribute iiv = n.Attributes["iv"];
                            XmlAttribute isign = n.Attributes["digisign"];
                            try
                            {
                                _inviteregkey = GetBytes(ikey.Value, 8);
                                _inviteregiv = GetBytes(iiv.Value, 8);
                                _inviteregseparator = char.Parse(isign.Value.Substring(0, 1));
                            }
                            catch
                            { }
                        }
                        else if (n.Name.ToLower() == "userapp")
                        {
                            XmlAttribute ikey = n.Attributes["key"];
                            XmlAttribute iiv = n.Attributes["iv"];
                            XmlAttribute isign = n.Attributes["digisign"];
                            try
                            {
                                _userappkey = GetBytes(ikey.Value, 8);
                                _userappiv = GetBytes(iiv.Value, 8);
                                _userappseparator = char.Parse(isign.Value.Substring(0, 1));
                            }
                            catch
                            { }
                        }
                    }
                }
            }
            catch
            {
                //ʹ��Ĭ��ֵ
            }
        }
        /// <summary>
        /// ��Ա������Կ
        /// </summary>
        static public byte[] UserKey
        {
            get { return _userkey; }
        }
        /// <summary>
        /// ��Ա��������
        /// </summary>
        static public byte[] UserIV
        {
            get { return _useriv; }
        }
        /// <summary>
        /// ����Ա��Կ
        /// </summary>
        static public byte[] AdminKey
        {
            get { return _adminkey; }
        }
        /// <summary>
        /// ����Ա��������
        /// </summary>
        static public byte[] AdminIV
        {
            get { return _adminiv; }
        }
        /// <summary>
        /// ��Ա����ǩ��
        /// </summary>
        static public char UserSeparator
        {
            get { return _userseparator; }
        }
        /// <summary>
        /// ����Ա����ǩ��
        /// </summary>
        static public char AdminSeparator
        {
            get { return _adminseparator; }
        }
        /// <summary>
        /// ����ע��ļ���key
        /// </summary>
        static public byte[] InviteKey
        {
            get { return _inviteregkey; }
        }
        /// <summary>
        /// ����ע��ļ���IV
        /// </summary>
        static public byte[] InviteIV
        {
            get { return _inviteregiv; }
        }
        /// <summary>
        /// ����ע�������ǩ��
        /// </summary>
        static public char InviteSeparator
        {
            get { return _inviteregseparator; }
        }
        /// <summary>
        /// �û�Ӧ�ó�������ǩ��
        /// </summary>
        static public char UserAppSeparator
        {
            get { return _userappseparator; }
        }
        /// <summary>
        /// �û�Ӧ�ó������Key
        /// </summary>
        static public byte[] UserAppKey
        {
            get { return _userappkey; }
        }
        /// <summary>
        /// �û�Ӧ�ó������IV
        /// </summary>
        static public byte[] UserAppIV
        {
            get { return _userappiv; }
        }
        static byte[] GetBytes(string input, int len)
        {
            if (input == null || input.Trim() == string.Empty)
                throw new ArgumentNullException();
            string[] s = input.Split(',');
            int n = s.Length;
            byte[] b = new byte[len];
            for (int i = 0; i < len; i++)
            {
                if (i > n)
                    b[i] = byte.Parse(" ", System.Globalization.NumberStyles.HexNumber);
                else
                    b[i] = byte.Parse(s[i], System.Globalization.NumberStyles.HexNumber);
            }
            return b;
        }
    }
}
