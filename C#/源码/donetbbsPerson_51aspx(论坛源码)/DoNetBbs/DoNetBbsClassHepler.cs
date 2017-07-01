//======================================================
//������������������  ��\\\|///                      
//�������������������� \\��- -��//                   
//��   www.donetbbs.com  ( @ @ ) www.csharpbbs.com                    
//��������������������oOOo-(_)-oOOo��������          
//������������������������������������������
//������������������ �� ԭ �� ��������������
//�������������˲���Ʒ���뱣������Ϣ��������
//��������** csharpbbs@hotmail.com **��������
//��������** donetbbs@hotmail.com **��������
//��������**    QICQ 351310701    **��������
//����������������������������Dooo��     ��
//�������������������� oooD��-(�� )��������
//�������������������� (  )��  ) /
//����������������������\ (�� (_/
//���������������������� \_)
//===========�Ϻ��ж��������������޹�˾=============
using System;
using System.Text;
using System.Web;
namespace DoNetBbs
{
    public abstract class DoNetBbsClassHepler
    {
        private static DoNetBbsClassHepler _defaultInstance = null;
        static DoNetBbsClassHepler()
        {
            CreateDefaultHepler();
        }
        /// <summary>
        /// ʵ��DoNetBbsClassHepler�ӿ�.
        /// </summary>
        /// <returns></returns>
        public static DoNetBbsClassHepler Instance()
        {
            return _defaultInstance;
        }
        private static void CreateDefaultHepler()
        {
            Type type = Type.GetType("DoNetBbs.DoNetBbsClass");
            object newObject = null;
            if (type != null)
            {
                newObject = Activator.CreateInstance(type);
            }
            _defaultInstance = newObject as DoNetBbsClassHepler;
        }
        /// <summary>
        /// ȡ�ַ���������
        /// </summary>
        /// <param name="StringChars">������ַ���</param>
        /// <param name="CharNub">�����ַ����ĸ���</param>
        /// <returns>�����ַ���</returns>
        public abstract string GetFewChars(string StringChars, int CharNub);
        /// <summary>
        /// ����session���ƣ����ظ�session��ֵ
        /// </summary>
        /// <param name="sessionName">session����</param>
        /// <returns>����null�������ַ���</returns>
        public abstract string GetSession(string sessionName);
        /// <summary>
        /// �ж������ַ����Ƿ������ͬ��ֵ
        /// </summary>
        /// <param name="chars">��һ���ַ�������abcd,cad</param>
        /// <param name="filterchars">��һ���ַ�������cad,abcd</param>
        /// <returns>���ؽ����True����False</returns>
        public abstract bool GetComparison(string chars, string filterchars);
        /// <summary>
        /// �����
        /// </summary>
        /// <param name="Number">�������λ��</param>
        /// <returns>����һ��Numberλ������</returns>
        public abstract int GetRandom(int Number);
        public abstract string GetPassword(string password);
        /// <summary>
        /// ���ܺ���
        /// </summary>
        /// <param name="chars">Ҫ���ܵ��ַ���</param>
        /// <returns>���ؼ��ܺ�Ľ��</returns>
        public abstract string GetEncryptChar(string chars);
        /// <summary>
        /// ���ܺ���
        /// </summary>
        /// <param name="chars">Ҫ���ܵ��ַ���</param>
        /// <returns>���ؽ��ܺ�Ľ��</returns>
        public abstract string GetDecrypt(string chars);

        /// <summary>
        /// web.config ����
        /// </summary>
        /// <param name="appSettingskey">web.config �ַ�����</param>
        /// <returns>���ظ� web.config ��ֵ</returns>
        public abstract string GetConfiguration(string appSettingskey);


        /// <summary>
        /// ����Ӧ���ַ�����ʽΪ��Ҫ����Ľ��
        /// </summary>
        /// <param name="html">��Ҫ��ʽ����html</param>
        /// <param name="chars">��Ҫ��ʽ�����ַ���</param>
        /// <param name="returns">��ʽ����Ľ��</param>
        /// <returns>���ظ�ʽ���html</returns>
        public abstract string GetFormat(string html, string chars, string returns);

        /// <summary>
        /// �ȸ�ʽ�����ٰ��Ѿ���ʽ�����ַ����ӻ���
        /// </summary>
        /// <param name="html">��Ҫ��ʽ����html</param>
        /// <param name="chars">��Ҫ��ʽ�����ַ�</param>
        /// <param name="returns">��ʽ���Ľ��</param>
        /// <returns></returns>
        public abstract string GetFormatRepeat(string html, string chars, string returns);


        public abstract bool GetComparison(string chars, bool Stirnumber, string filterchars, bool filternumber);

        public abstract string GetUniqueArraylist(string chars, bool Stirnumber, string addchars);

        public abstract int GetMod(int divisor, int dividend);
        /// <summary>
        /// ʱ����
        /// </summary>
        /// <param name="time">�����ʱ��</param>
        /// <param name="type">ʱ��������d�죬hСʱ��t���ӣ�s�룬����Ϊ����</param>
        /// <returns>��������ʱ����</returns>
        public abstract string GetTimeSpan(DateTime time, string type);

        /// <summary>
        /// ��һҳ��url
        /// </summary>
        /// <returns>������һҳ��url</returns>
        public abstract string GetUrlReferrer();

        /// <summary>
        /// HttpContext
        /// </summary>
        /// <returns>����HttpContext</returns>
        public abstract HttpContext GetHttpContext();

        /// <summary>
        /// int�͵�Requestֵ
        /// </summary>
        /// <param name="query">Request ����</param>
        /// <returns>����int�͵�Requestֵ,���ΪNull���򷵻�0</returns>
        public abstract int GetQueryInt(string query);
        /// <summary>
        /// �ַ����͵�Requestֵ
        /// </summary>
        /// <param name="query">Request ����</param>
        /// <returns>�����ַ����͵�Requestֵ,���ΪNull���򷵻ؿ�</returns>
        public abstract string GetQueryString(string query);

        public abstract int GetFormInt(string name);
        public abstract string GetFormString(string name, bool encode);
        public abstract bool GetDisableForm();
        /// <summary>
        /// ����Sessionֵ
        /// </summary>
        /// <param name="sessionName">Session����</param>
        /// <param name="sessionVlue">Sessionֵ</param>
        public abstract void SetSession(string sessionName, string sessionVlue);

        public abstract void WriteAlert(string alert, bool ajax);
        public abstract void WriteFocus(string name, bool ajax);
        public abstract void WriteRedirect(string url, bool parent, bool ajax);

        public abstract void WriteReload(bool parent, bool ajax);
        public abstract void WriteWindowClose(bool ajax);
        public abstract void WriteJavaScript(string jsname, bool ajax);

        public abstract string GetJavaScriptInput(string id, string value, bool ajax);

        public abstract string GetJavaScriptSelect(string id, string value, bool ajax);

        public abstract string GetJavaScriptDsplay(string id, bool display, bool ajax);
        public abstract string GetJavaScriptDsplay(string id, string groupID, bool ajax);

        public abstract string GetJavaScriptidValue(string id, string title,string value, bool ajax);

        public abstract string GetJavaScriptDisabled(string name, bool disabled, bool ajax);


        public abstract string GetJavaScriptCopyInnerHTML(string indexid, string secondid, bool ajax);
        public abstract string GetJavaScriptInnerHTML(string id, string value, bool ajax);

        public abstract string GetJavaScriptCheckBox(string id, bool check, bool ajax);

        public abstract string GetJavaScriptCheckBoxGroup(string id, string value, bool ajax);

        public abstract string GetJavaScriptCheckRadio(string name, string value, bool ajax);

        //public abstract string GetWriteDisplay(string name, bool display);
        public abstract bool GetEmailFormat(string email);

        public abstract void PostEmail(System.Net.Mail.MailMessage mail);

        public abstract string GetAccessDate(string date);


        public abstract void WrietRefreshInfo(string menuTitle, string title, string content, string name, DateTime time);

    }
}
