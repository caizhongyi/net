//======================================================
//　　　　　　　　　  　\\\|///                      
//　　　　　　　　　　 \\　- -　//                   
//　   www.donetbbs.com  ( @ @ ) www.csharpbbs.com                    
//┏━━━━━━━━━oOOo-(_)-oOOo━━━┓          
//┃　　　　　　　　　　　　　　　　　　　┃
//┃　　　　　　　东 网 原 创 　　　　　　┃
//┃　　　　美人草作品，请保留此信息！　　┃
//┃　　　** csharpbbs@hotmail.com **　　　┃
//┃　　　** donetbbs@hotmail.com **　　　┃
//┃　　　**    QICQ 351310701    **　　　┃
//┃　　　　　　　　　　　　　Dooo　     ┃
//┗━━━━━━━━━ oooD━-(　 )━━━┛
//　　　　　　　　　　 (  )　  ) /
//　　　　　　　　　　　\ (　 (_/
//　　　　　　　　　　　 \_)
//===========上海市东网技术服务有限公司=============
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
        /// 实现DoNetBbsClassHepler接口.
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
        /// 取字符串个个数
        /// </summary>
        /// <param name="StringChars">输入的字符串</param>
        /// <param name="CharNub">返回字符串的个数</param>
        /// <returns>返回字符串</returns>
        public abstract string GetFewChars(string StringChars, int CharNub);
        /// <summary>
        /// 输入session名称，返回该session的值
        /// </summary>
        /// <param name="sessionName">session名称</param>
        /// <returns>返回null，或者字符串</returns>
        public abstract string GetSession(string sessionName);
        /// <summary>
        /// 判断两个字符串是否存在相同的值
        /// </summary>
        /// <param name="chars">第一个字符串，如abcd,cad</param>
        /// <param name="filterchars">第一个字符串，如cad,abcd</param>
        /// <returns>返回结果是True或者False</returns>
        public abstract bool GetComparison(string chars, string filterchars);
        /// <summary>
        /// 随机数
        /// </summary>
        /// <param name="Number">随机数的位数</param>
        /// <returns>返回一个Number位的数字</returns>
        public abstract int GetRandom(int Number);
        public abstract string GetPassword(string password);
        /// <summary>
        /// 加密函数
        /// </summary>
        /// <param name="chars">要加密的字符串</param>
        /// <returns>返回加密后的结果</returns>
        public abstract string GetEncryptChar(string chars);
        /// <summary>
        /// 解密函数
        /// </summary>
        /// <param name="chars">要解密的字符串</param>
        /// <returns>返回解密后的结果</returns>
        public abstract string GetDecrypt(string chars);

        /// <summary>
        /// web.config 配置
        /// </summary>
        /// <param name="appSettingskey">web.config 字符名称</param>
        /// <returns>返回该 web.config 的值</returns>
        public abstract string GetConfiguration(string appSettingskey);


        /// <summary>
        /// 把相应的字符串格式为需要输出的结果
        /// </summary>
        /// <param name="html">需要格式化的html</param>
        /// <param name="chars">需要格式化的字符串</param>
        /// <param name="returns">格式化后的结果</param>
        /// <returns>返回格式后的html</returns>
        public abstract string GetFormat(string html, string chars, string returns);

        /// <summary>
        /// 先格式化，再把已经格式化的字符串加回来
        /// </summary>
        /// <param name="html">需要格式化的html</param>
        /// <param name="chars">需要格式化的字符</param>
        /// <param name="returns">格式化的结果</param>
        /// <returns></returns>
        public abstract string GetFormatRepeat(string html, string chars, string returns);


        public abstract bool GetComparison(string chars, bool Stirnumber, string filterchars, bool filternumber);

        public abstract string GetUniqueArraylist(string chars, bool Stirnumber, string addchars);

        public abstract int GetMod(int divisor, int dividend);
        /// <summary>
        /// 时间间隔
        /// </summary>
        /// <param name="time">输入的时间</param>
        /// <param name="type">时间间隔类型d天，h小时，t分钟，s秒，其他为毫秒</param>
        /// <returns>返回相差的时间间隔</returns>
        public abstract string GetTimeSpan(DateTime time, string type);

        /// <summary>
        /// 上一页的url
        /// </summary>
        /// <returns>返回上一页的url</returns>
        public abstract string GetUrlReferrer();

        /// <summary>
        /// HttpContext
        /// </summary>
        /// <returns>返回HttpContext</returns>
        public abstract HttpContext GetHttpContext();

        /// <summary>
        /// int型的Request值
        /// </summary>
        /// <param name="query">Request 名称</param>
        /// <returns>返回int型的Request值,如果为Null，则返回0</returns>
        public abstract int GetQueryInt(string query);
        /// <summary>
        /// 字符串型的Request值
        /// </summary>
        /// <param name="query">Request 名称</param>
        /// <returns>返回字符串型的Request值,如果为Null，则返回空</returns>
        public abstract string GetQueryString(string query);

        public abstract int GetFormInt(string name);
        public abstract string GetFormString(string name, bool encode);
        public abstract bool GetDisableForm();
        /// <summary>
        /// 设置Session值
        /// </summary>
        /// <param name="sessionName">Session名称</param>
        /// <param name="sessionVlue">Session值</param>
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
