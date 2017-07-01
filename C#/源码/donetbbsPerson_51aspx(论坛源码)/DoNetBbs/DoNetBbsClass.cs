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
using System.Web;
using System.Text;
using System.IO;
using System.Security.Cryptography;
namespace DoNetBbs
{

    public class DoNetBbsClass : DoNetBbsClassHepler
    {
        public DoNetBbsClass()
        {
			if (VersionKey == string.Empty)
			{

			    VersionKey = GetConfiguration("WebSite_Version");
                HttpContext.Current.Response.Write(Decrypt(VersionKey));
			    WebSite = Decrypt(VersionKey).Split('|')[0].ToString();
			    Version = Decrypt(VersionKey).Split('|')[1].ToString();

			    bool boolWebSite = false;
			    if (GetConfiguration("WebSite_WebSite").Replace(WebSite, "") != GetConfiguration("WebSite_WebSite"))
			    {
			        boolWebSite = true;
			    }
			    bool boolTimeOut = false;
			    if ((double.Parse(GetTimeSpan(System.Convert.ToDateTime(Decrypt(VersionKey).Split('|')[2].ToString()), "d")) <= 0))
			    {
			        boolTimeOut = true;
			    }
			    if (boolTimeOut)
			    {
			        if (!boolWebSite)
			        {
			            HttpContext.Current.Response.Write("<script>alert('Version Time Out');</script>");
			            Version = "Time Out<script>alert('Version Time Out');</script>";
			        }
			    }
			    else
			    {
			        HttpContext.Current.Response.Write("<script>alert('Version Time Out');</script>");
			        Version = "Time Out<script>alert('Version Time Out');</script>";
			    }

			    HttpContext.Current.Response.Write(Decrypt(VersionKey));
                HttpContext.Current.Response.Write(EncryptChar("baiqiansheng.com|BQSBbs v1.0.0|2010-01-01 12:59:59"));
			}
        }
        private static string  _Version = string.Empty;
        public static string Version
        {
            get { return _Version; }
            set { _Version = value; }
        }
        private static string _WebSite = string.Empty;
        public static string WebSite
        {
            get { return _WebSite; }
            set { _WebSite = value; }
        }


        private string MyKey
        {
            get {return "12345876";}
        }
        private string EncryptChar(string chars)
        {
            string strKey = "12345678";
            string strIV = "12345678";
            Byte[] Key = Encoding.UTF8.GetBytes(MyKey.Substring(0, 8));
            Byte[] IV = Encoding.UTF8.GetBytes(strIV.Substring(0, 8));
            Byte[] inputByteArray = Encoding.UTF8.GetBytes(chars);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(Key, Key), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }//

        /// <summary>
        /// 解密函数
        /// </summary>
        /// <param name="chars">要解密的字符串</param>
        /// <returns>返回解密后的结果</returns>
        private string Decrypt(string chars)
        {
            string strKey = "12345678";
            string strIV = "12345678";
            Byte[] Key = Encoding.UTF8.GetBytes(MyKey.Substring(0, 8));
            Byte[] IV = Encoding.UTF8.GetBytes(strIV.Substring(0, 8));
            byte[] inputByteArray = new Byte[chars.Length];
            inputByteArray = Convert.FromBase64String(chars);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(Key, Key), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            Encoding encoding = Encoding.UTF8;
            return encoding.GetString(ms.ToArray());
        }//


        private static string VersionKey=string.Empty;
        /// <summary>
        /// 取字符串个个数
        /// </summary>
        /// <param name="StringChars">输入的字符串</param>
        /// <param name="CharNub">返回字符串的个数</param>
        /// <returns>返回字符串</returns>
        public override string GetFewChars(string StringChars, int CharNub)
        {
            string StringChar;
            Byte[] MyByte = System.Text.Encoding.Default.GetBytes(StringChars);
            if (MyByte.Length <= CharNub)
            {
                StringChar = StringChars;
            }
            else
            {
                StringChar = System.Text.Encoding.Default.GetString(MyByte, 0, CharNub);
                StringChar += "...";
            }
            return (StringChar);
        }
        /// <summary>
        /// 输入session名称，返回该session的值
        /// </summary>
        /// <param name="sessionName">session名称</param>
        /// <returns>返回null，或者字符串</returns>
        public override string GetSession(string sessionName)
        {
            if (HttpContext.Current.Session[sessionName] == null)
            {
                return null;
            }
            else
            {
                return HttpContext.Current.Session[sessionName].ToString();
            }
        }
        /// <summary>
        /// 判断两个字符串是否存在相同的值
        /// </summary>
        /// <param name="chars">第一个字符串，如abcd,cad</param>
        /// <param name="filterchars">第一个字符串，如cad,abcd</param>
        /// <returns>返回结果是True或者False</returns>
        public override bool GetComparison(string chars, string filterchars)
        {
            if (chars == string.Empty || filterchars==string.Empty)
            {
                return false;
            }
            string[] ArrayString;
            ArrayString = chars.Split(',');
            string[] ArrayFilter;
            ArrayFilter = filterchars.Split(',');
            for (int i = 0; i < ArrayString.Length; i++)
            {
                if (ArrayString[i].ToString() != string.Empty)
                {
                    for (int j = 0; j < ArrayFilter.Length; j++)
                    {
                        if (ArrayFilter[j].ToString() != string.Empty)
                        {
                            if (ArrayString[i].ToString() == ArrayFilter[j])
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// 随机数
        /// </summary>
        /// <param name="Number">随机数的位数</param>
        /// <returns>返回一个Number位的数字</returns>
        public override int GetRandom(int Number)
        {
            Random Rand = new Random();
            if (Number >= 10)
            {
                Number = 9;
            }
            int DoNetBbsNumber = int.Parse(Math.Pow(10, Number).ToString());
            return Rand.Next(DoNetBbsNumber / 10, DoNetBbsNumber - 1);
        }
        public override string GetPassword(string password)
        {
            if (GetConfiguration("WebSite_PassWordType").ToString() == "Md5")
            {
                return GetPasswordMd5(password);
            }
            else if (GetConfiguration("WebSite_PassWordType").ToString() == "Dec")
            {
                return GetEncryptChar(password);
            }
            else
            {
                return password;
            }

        }//
        /// <summary>
        /// 加密和解密的Key
        /// </summary>
        private string strKey
        {
            get { return "32145876"; }
        }
        /// <summary>
        /// 加密函数
        /// </summary>
        /// <param name="chars">要加密的字符串</param>
        /// <returns>返回加密后的结果</returns>
        public override string GetEncryptChar(string chars)
        {
            string strKey = "12345678";
            string strIV = "12345678";
            Byte[] Key = Encoding.UTF8.GetBytes(strKey.Substring(0, 8));
            Byte[] IV = Encoding.UTF8.GetBytes(strIV.Substring(0, 8));
            Byte[] inputByteArray = Encoding.UTF8.GetBytes(chars);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(Key, Key), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }//

        /// <summary>
        /// 解密函数
        /// </summary>
        /// <param name="chars">要解密的字符串</param>
        /// <returns>返回解密后的结果</returns>
        public override string GetDecrypt(string chars)
        {
            string strKey = "12345678";
            string strIV = "12345678";
            Byte[] Key = Encoding.UTF8.GetBytes(strKey.Substring(0, 8));
            Byte[] IV = Encoding.UTF8.GetBytes(strIV.Substring(0, 8));
            byte[] inputByteArray = new Byte[chars.Length];
            inputByteArray = Convert.FromBase64String(chars);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(Key, Key), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            Encoding encoding = Encoding.UTF8;
            return encoding.GetString(ms.ToArray());
        }//
        
        /// <summary>
        /// Md5加密
        /// </summary>
        /// <param textChar="password">输入要加密的字符串</param>
        /// <returns>返回Md5加密后的结果</returns>
        private string GetPasswordMd5(string textChar)
        {
            textChar = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(textChar, "md5").ToLower().Substring(8, 16);
            return textChar;
        }//

        /// <summary>
        /// web.config 配置
        /// </summary>
        /// <param name="appSettingskey">web.config 字符名称</param>
        /// <returns>返回该 web.config 的值</returns>
        public override string GetConfiguration(string appSettingskey)
        {
            return System.Configuration.ConfigurationManager.AppSettings[appSettingskey].ToString();
        }

        /// <summary>
        /// 把相应的字符串格式为需要输出的结果
        /// </summary>
        /// <param name="html">需要格式化的html</param>
        /// <param name="chars">需要格式化的字符串</param>
        /// <param name="returns">格式化后的结果</param>
        /// <returns>返回格式后的html</returns>
        public override string GetFormat(string html, string chars, string returns)
        {
            return html.Replace("{$" + chars + "}", returns);
        }

        /// <summary>
        /// 先格式化，再把已经格式化的字符串加回来
        /// </summary>
        /// <param name="html">需要格式化的html</param>
        /// <param name="chars">需要格式化的字符</param>
        /// <param name="returns">格式化的结果</param>
        /// <returns></returns>
        public override string GetFormatRepeat(string html, string chars, string returns)
        {
            return html.Replace("{$" + chars + "}", returns + "{$" + chars + "}");
        }


        public override bool GetComparison(string chars, bool Stirnumber, string filterchars, bool filternumber)
        {
            if (chars == string.Empty || filterchars==string.Empty)
            {
                return false;
            }
            string[] ArrayString;
            if (Stirnumber)
            {
                ArrayString = chars.Split(',');
            }
            else
            {
                ArrayString = chars.Split('|');
            }
            string[] ArrayFilter;
            if (filternumber)
            {
                ArrayFilter = filterchars.Split(',');
            }
            else
            {
                ArrayFilter = filterchars.Split('|');
            }
            for (int i = 0; i < ArrayString.Length; i++)
            {
                if (ArrayString[i].ToString() != string.Empty)
                {
                    for (int j = 0; j < ArrayFilter.Length; j++)
                    {
                        if (ArrayFilter[j].ToString() != string.Empty)
                        {
                            if (ArrayString[i].ToString().ToLower() == ArrayFilter[j].ToLower())
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
        public override string GetUniqueArraylist(string chars, bool Stirnumber, string addchars)
        {
            if (addchars != null)
            {
                if (chars != string.Empty)
                {
                    if (Stirnumber)
                    {
                        chars += "," + addchars;
                    }
                    else
                    {
                        chars += "|" + addchars;
                    }
                }
                else
                {
                    chars = addchars;
                }
            }
            if (chars == string.Empty)
            {
                return null;
            }
            string newchars = null;
            string[] ArrayString;
            if (Stirnumber)
            {
                ArrayString = chars.Split(',');
            }
            else
            {
                ArrayString = chars.Split('|');
            }
            for (int i = 0; i < ArrayString.Length; i++)
            {
                if (ArrayString[i].ToString() != string.Empty)
                {
                    if (newchars == null)
                    {
                        newchars = ArrayString[i].ToString();
                    }
                    else
                    {
                        if (!GetComparison(newchars, Stirnumber, ArrayString[i].ToString(), true))
                        {
                            newchars += "," + ArrayString[i].ToString();
                        }
                    }//
                }//
            }
            return newchars;
        }//



        public override bool GetDisableForm()
        {
            if (GetConfiguration("WebSite_DisableFormUrl") == string.Empty)
            {
                return true;
            }
            else
            {
                string UrlReferrer = string.Empty;
                if (HttpContext.Current.Request.UrlReferrer == null)
                {
                    return false;
                }
                else
                {
                    UrlReferrer = HttpContext.Current.Request.UrlReferrer.ToString();
                    if (UrlReferrer.Split('/').Length >= 2)
                    {
                        if (GetComparison(UrlReferrer.Split('/')[2].ToString(), false, GetConfiguration("WebSite_DisableFormUrl"), false))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public override int GetMod(int divisor, int dividend)
        {
            if ((divisor == 0) || (dividend == 0))
                return 0;

            int c = divisor / dividend;
            if (c * dividend != divisor)
            {
                c = divisor - (divisor / dividend) * dividend;
            }
            else
            {
                c = 0;
            }//
            return c;
        }
        public override int GetFormInt(string name)
        {
            if (HttpContext.Current.Request.Form[name] == null)
            {
                return 0;
            }
            else
            {
                return int.Parse(HttpContext.Current.Request.Form[name].ToString());
            }
        }
        public override string GetFormString(string name, bool encode)
        {
            if (HttpContext.Current.Request.Form[name] == null)
            {
                return string.Empty;
            }
            else
            {
                if (HttpContext.Current.Request.Form[name].Replace(" ", "") == "")
                {
                    return string.Empty;
                }
                else
                {
                    if (encode)
                    {
                        return System.Web.HttpUtility.HtmlEncode(HttpContext.Current.Request.Form[name].ToString().Replace("'", "’").Replace("{$", "{￥"));
                    }
                    else
                    {
                        return HttpContext.Current.Request.Form[name].ToString().Replace("'", "’").Replace("{$", "{￥");
                    }
                }
            }
        }
        /// <summary>
        /// 时间间隔
        /// </summary>
        /// <param name="time">输入的时间</param>
        /// <param name="type">时间间隔类型d天，h小时，t分钟，s秒，其他为毫秒</param>
        /// <returns>返回相差的时间间隔</returns>
        public override string GetTimeSpan(DateTime time, string type)
        {
            TimeSpan ThisDateTime = (DateTime.Now - time);
            if (type == "d")
            {
                return ThisDateTime.TotalDays.ToString();
            }
            else if (type == "h")
            {
                return ThisDateTime.TotalHours.ToString();
            }
            else if (type == "t")
            {
                return ThisDateTime.TotalMinutes.ToString();
            }
            else if (type == "s")
            {
                return ThisDateTime.TotalSeconds.ToString();
            }
            else
            {
                //GetTimeSpan(time,"s")
                return ThisDateTime.TotalMilliseconds.ToString();
            }
        }

        /// <summary>
        /// 上一页的url
        /// </summary>
        /// <returns>返回上一页的url</returns>
        public override string GetUrlReferrer()
        {
            if (HttpContext.Current.Request.UrlReferrer == null)
            {
                return string.Empty;
            }
            else
            {
                return HttpContext.Current.Request.UrlReferrer.ToString();
            }
        }

        /// <summary>
        /// HttpContext
        /// </summary>
        /// <returns>返回HttpContext</returns>
        public override HttpContext GetHttpContext()
        {
            //HttpContext context = new HttpContext();
            return HttpContext.Current;
        }

        /// <summary>
        /// int型的Request值
        /// </summary>
        /// <param name="query">Request 名称</param>
        /// <returns>返回int型的Request值,如果为Null，则返回0</returns>
        public override int GetQueryInt(string query)
        {
            if (HttpContext.Current.Request[query] == null)
            {
                return 0;
            }
            else
            {
                return int.Parse(HttpContext.Current.Request[query].ToString());
            }
        }
        /// <summary>
        /// 字符串型的Request值
        /// </summary>
        /// <param name="query">Request 名称</param>
        /// <returns>返回字符串型的Request值,如果为Null，则返回空</returns>
        public override string GetQueryString(string query)
        {
            if (HttpContext.Current.Request[query] == null)
            {
                return string.Empty;
            }
            else
            {
                return HttpContext.Current.Request[query].ToString();
            }
        }

        /// <summary>
        /// 设置Session值
        /// </summary>
        /// <param name="sessionName">Session名称</param>
        /// <param name="sessionVlue">Session值</param>
        public override void SetSession(string sessionName,string sessionVlue)
        {
            if (sessionVlue == string.Empty || sessionVlue == null)
            {
                HttpContext.Current.Session.Remove(sessionName);
            }
            else
            {
                HttpContext.Current.Session[sessionName] = sessionVlue;
            }
        }

        public override void WriteAlert(string alert, bool ajax)
        {
            if (ajax)
            {
                HttpContext.Current.Response.Write("alert('" + alert.Replace("\r\n", "") + "');");
            }
            else
            {
                HttpContext.Current.Response.Write("<script>alert('" + alert.Replace("\r\n", "") + "');</script>");
            }
        }
        public override void WriteFocus(string name, bool ajax)
        {
            if (ajax)
            {
                    HttpContext.Current.Response.Write("document.all."+name+".focus();");
            }
            else
            {
                HttpContext.Current.Response.Write("document.all."+name+".focus();");
            }
        }
        public override void WriteRedirect(string url, bool parent, bool ajax)
        {
            if (url == null)
            {
                url = "about:blank";
            }
            string parentPath;
            if (parent)
            {
                parentPath = "parent.window";
            }
            else
            {
                parentPath = "window";
            }
            if (ajax)
            {
                HttpContext.Current.Response.Write("" + parentPath + ".location='" + url + "';");
            }
            else
            {
                HttpContext.Current.Response.Write("<script>" + parentPath + ".location='" + url + "';</script>");
            }
        }

        public override void WriteReload(bool parent,bool ajax)
        {
            if (ajax)
            {
                if (parent)
                {
                    HttpContext.Current.Response.Write("parent.window.location.reload();");
                }
                else
                {
                    HttpContext.Current.Response.Write("window.location.reload();");
                }
            }
            else
            {
                if (parent)
                {
                    HttpContext.Current.Response.Write("<script>parent.window.location.reload();</script>");
                }
                else
                {
                    HttpContext.Current.Response.Write("<script>window.location.reload();</script>");
                }
            }
        }
        public override void WriteWindowClose(bool ajax)
        {
            if (ajax)
            {
                HttpContext.Current.Response.Write("parent.window.opener=null;parent.window.close();");
            }
            else
            {
                HttpContext.Current.Response.Write("<script>parent.window.opener=null;parent.window.close();</script>");
            }
        }
        public override void WriteJavaScript(string jsname, bool ajax)
        {
            if (ajax)
            {
                HttpContext.Current.Response.Write("" + jsname + ";");
            }
            else
            {
                HttpContext.Current.Response.Write("<script>" + jsname + ";</script>");
            }
        }

        //public override string GetWriteDisplay(string name, bool display)
        //{
        //    if (display)
        //    {
        //        return "<script>parent.document.all." + name + ".style.display='';</script>";
        //    }
        //    else
        //    {
        //        return "<script>parent.document.all." + name + ".style.display='none';</script>";
        //    }
        //}
        public override string GetJavaScriptInput(string id, string value, bool ajax)
        {
            if (ajax)
            {
                HttpContext.Current.Response.Write("parent.document.all." + id + ".value='" + value + "';");
                return null;
            }
            else
            {
                return "<script>parent.document.all." + id + ".value='" + value + "';</script>";            
            }
        }//

        public override string GetJavaScriptSelect(string id, string value,bool ajax)
        {
            if (ajax)
            {
                HttpContext.Current.Response.Write("for (var d=0;d<parent.document.all." + id + ".length;d++){if (parent.document.all." + id + ".options[d].value=='" + value + "'){parent.document.all." + id + ".options[d].selected='true';d=parent.document.all." + id + ".length;}};");
                return null;
            }
            else
            {
                return "<script>for (var d=0;d<parent.document.all." + id + ".length;d++){if (parent.document.all." + id + ".options[d].value=='" + value + "'){parent.document.all." + id + ".options[d].selected='true';d=parent.document.all." + id + ".length;}}</script>";
            }
        }//

        public override string GetJavaScriptDsplay(string id, bool display,bool ajax)
        {
            if (ajax)
            {
                if (display == false)
                {
                    HttpContext.Current.Response.Write("parent.document.all." + id + ".style.display='none';");
                }
                else
                {
                    HttpContext.Current.Response.Write("parent.document.all." + id + ".style.display='';");
                }
                return null;
            }
            else
            {
                if (display == false)
                {
                    return "<script>parent.document.all." + id + ".style.display='none';</script>";
                }
                else
                {
                    return "<script>parent.document.all." + id + ".style.display='';</script>";
                }
            }
        }//
        public override string GetJavaScriptDsplay(string id,string groupID,bool ajax)
        {
            if (ajax)
            {
                HttpContext.Current.Response.Write("var group='" + groupID + "';for (var d=0;d<group.split(',').length;d++){eval(\"document.all.\"+group.split(',')[d]).style.display='none';}document.all." + id + ".style.display='';");
                return null;
            }
            else
            {
                return "<script>var group='" + groupID + "';for (var d=0;d<group.split(',').length;d++){eval(\"document.all.\"+group.split(',')[d]).style.display='none';}document.all." + id + ".style.display='';</script>";
            }
        }//



        public override string GetJavaScriptDisabled(string name, bool disabled, bool ajax)
        {
            if (ajax)
            {
                if (disabled)
                {
                    HttpContext.Current.Response.Write("parent.document.all." + name + ".disabled='1';");
                }
                else
                {
                    HttpContext.Current.Response.Write("parent.document.all." + name + ".disabled='0';");
                }
                return null;
            }
            else
            {
                if (disabled)
                {
                    return "<script>parent.document.all." + name + ".disabled='1';</script>";
                }
                else
                {
                    return "<script>parent.document.all." + name + ".disabled='0';</script>";
                }
            }
        }
        public override string GetJavaScriptCopyInnerHTML(string indexid, string secondid, bool ajax)
        {
            if (ajax)
            {
                HttpContext.Current.Response.Write("parent.document.all." + indexid + ".innerHTML=parent.document.all." + secondid + ".innerHTML;");
                return null;
            }
            else
            {
                return "<script>parent.document.all." + indexid + ".innerHTML=parent.document.all." + secondid + ".innerHTML;</script>";
            }
        }//
        public override string GetJavaScriptInnerHTML(string id, string value, bool ajax)
        {
            if (ajax)
            {
                HttpContext.Current.Response.Write("parent.document.all." + id + ".innerHTML='" + value.Replace("\r\n", "") + "';");
                return null;
            }
            else
            {
                return "<script>parent.document.all." + id + ".innerHTML='" + value.Replace("\r\n", "") + "';</script>";
            }
        }//

        public override string GetJavaScriptidValue(string id, string title, string value, bool ajax)
        {
            if (ajax)
            {
                HttpContext.Current.Response.Write("parent.document.all." + id + "." + title + "='" + value + "';");
                return null;
            }
            else
            {
                return "<script>parent.document.all." + id + "." + title + "='" + value + "';</script>";
            }
        }

        public override string GetJavaScriptCheckBox(string id, bool check, bool ajax)
        {
            if (ajax)
            {
                if (check)
                {
                    HttpContext.Current.Response.Write("parent.document.all." + id + ".checked=1;");
                }
                else
                {
                    HttpContext.Current.Response.Write("parent.document.all." + id + ".checked=0;");
                }
                return null;
            }
            else
            {
                if (check)
                {
                    return "<script>parent.document.all." + id + ".checked=1;</script>";
                }
                else
                {
                    return "<script>parent.document.all." + id + ".checked=0;</script>";
                }
            }
        }//
        public override string GetJavaScriptCheckBoxGroup(string id, string value, bool ajax)
        {
            value = value.Replace("\r\n", "").Replace("'", "‘");           

            if (ajax)
            {
                HttpContext.Current.Response.Write("for (var d=0;d<parent.document.all." + id + ".length;d++){for (var j=0;j<\"" + value + "\".split(',').length;j++){if (\"" + value + "\".split(',')[j]==parent.document.all." + id + "[d].value){parent.document.all." + id + "[d].checked=1;j=\"" + value + "\".split(',').length;}}};");
                return null;
            }
            else
            {
                return "<script>for (var d=0;d<parent.document.all." + id + ".length;d++){for (var j=0;j<\"" + value + "\".split(',').length;j++){if (\"" + value + "\".split(',')[j]==parent.document.all." + id + "[d].value){parent.document.all." + id + "[d].checked=1;j=\"" + value + "\".split(',').length;}}}</script>";
            }
        }//

        public override string GetJavaScriptCheckRadio(string name, string value, bool ajax)
        {
            if (ajax)
            {
                HttpContext.Current.Response.Write("for (var d=0;d<parent.document.all." + name + ".length;d++){if (parent.document.all." + name + "[d].value=='" + value + "'){parent.document.all." + name + "[d].checked='true';d=parent.document.all." + name + ".length;}};");
                return null;
            }
            else
            {
                return "<script>for (var d=0;d<parent.document.all." + name + ".length;d++){if (parent.document.all." + name + "[d].value=='" + value + "'){parent.document.all." + name + "[d].checked='true';d=parent.document.all." + name + ".length;}}</script>";
            }//
        }
        public override bool GetEmailFormat(string email)
        {
            if (email == string.Empty)
            {
                return false;
            }
            else
            {
                if (email.Length < 6)
                {
                    return false;
                }
                else
                {
                    if (email.Replace("@","").ToString()==email)
                    {
                        return false;
                    }
                    if (email.Replace(".","").ToString() == email)
                    {
                        return false;
                    }
                    return true;
                }
            }
        }

        public override void PostEmail(System.Net.Mail.MailMessage mail)
        {
            System.Net.Mail.SmtpClient ISmtpClient;
            if (GetConfiguration("WebSite_EmailHost") == string.Empty || GetConfiguration("WebSite_EmailUserName") == string.Empty || GetConfiguration("WebSite_EmailPassWord") == string.Empty)
            {
                ISmtpClient = new System.Net.Mail.SmtpClient("61.152.108.187");
                ISmtpClient.Credentials = new System.Net.NetworkCredential("smtpserver", "Len1long");
            }
            else
            {
                ISmtpClient = new System.Net.Mail.SmtpClient("" + GetConfiguration("WebSite_EmailHost") + "");
                ISmtpClient.Credentials = new System.Net.NetworkCredential("" + GetConfiguration("WebSite_EmailUserName") + "", "" + GetConfiguration("WebSite_EmailPassWord") + "");
            }

            mail.Body += "<br><br><br><br>更多服务、更多精彩，请到 <a href=http://www.donetbbs.com target=_blank>东网论坛</a><br><br><a href=http://www.donetbbs.com target=_blank><img src=\"http://www.donetbbs.com/Images/donetbbs_ad/banner468x60.gif\" /></a>";
            try
            {
                ISmtpClient.Send(mail);
            }
            catch { }
        }


        public override string GetAccessDate(string date)
        {
            if (GetConfiguration("ConnectionType").ToString() == "Access")
            {
                return "\"" + date + "\"";
            }
            else
            {
                return date;
            }
        }

        public override void WrietRefreshInfo(string menuTitle,string title, string content, string name, DateTime time)
        {
            string write = string.Empty;

            write = "var shimName=\"Dos_RefreshDiv\";";
            write += "var divObj=document.getElementById(shimName);";
            write += "if (divObj==null)";
            write += "{";
            write += "divObj = document.createElement(\"<div></div>\"); ";
            write += "divObj.name = shimName;";
            write += "divObj.id = shimName;";
            write += "divObj.openID=\"0\";";

            write += "divObj.style.zIndex=100000;";
            write += "divObj.style.width='200';";
            write += "divObj.style.height='150';";
            write += "divObj.style.position='absolute';";
            write += "divObj.innerHTML='<div style=\"background-color : #336699;width:100%;height:22px;color:ffffff;padding-left : 4px;padding-top: 4px;\"><div style=\"width:80px;float:left\">";
            write += "" + menuTitle + "</div><div style=\"width:50px;float:right;text-align : right;padding-right : 4px;\"><a href=\"javascript:Dos_DeleteMessages()\"><font color=ffffff>关闭</font></a> </div></div>';";
            write += "window.document.body.appendChild(divObj);";
            write += "divObj=document.getElementById(shimName);";
            write += "}";
            write += "divObj.style.display='';";
            write += "divObj.style.backgroundColor='f0f0f0';";
            write += "var iframeName=\"Dos_RefreshIframe\";";
            write += "var iframeObj=document.getElementById(iframeName);";
            write += "if (iframeObj==null)";
            write += "{";
            write += "var iframeObj = document.createElement(\"<iframe scrolling='no' frameborder='1'\"+";
            write += "\"style='position:absolute; top:0px;\"+";
            write += "\"left:0px; display:none;width:0;height:0'></iframe>\"); ";
            write += "iframeObj.name = iframeName;";
            write += "iframeObj.id = iframeName;";
            write += "window.document.body.appendChild(iframeObj);";
            write += "iframeObj=document.getElementById(iframeName);";
            write += "iframeObj.style.width='200';";
            write += "iframeObj.style.height='150';";
            write += "}";

            write += "iframeObj.style.display='';";

            write += "if (parseInt(divObj.openID)!=0)";
            write += "{";
            write += "var thisShimName=\"Dos_RefreshDiv\"+divObj.openID;";
            write += "var thisDivObj=document.getElementById(thisShimName);";
            write += "thisDivObj.style.display='none';";
            write += "}";

            write += "divObj.openID=parseInt(divObj.openID)+1;";

            write += "var shimNames=\"Dos_RefreshDiv\"+divObj.openID;";

            write += "var shimDiv=\"<div id=\"+shimNames+\" style='width:100%;height:26px;'><div";

            write += " style='width:100%;height:22px;padding-left:4px;padding-top:4px;text-align :left'>" + title + "</div><div";

            write += " style='width:100%;height:80px;overflow:hidden;padding-left:4px;text-align :left'>" + content + "</div><div";

            write += " style='width:100%;height:22px;text-align :right;padding-right:4px;background-color : #336699;padding-top:4px;'>" + name + " ";

            write += "" + time + "</div></div>\";";

            write += "divObj.innerHTML+=shimDiv;";
            write += "Dos_RefreshMessagesIDV();";
            HttpContext.Current.Response.Write(write);


        }
    }
}
