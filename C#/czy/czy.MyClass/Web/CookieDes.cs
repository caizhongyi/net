using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace czy.MyClass.Web
{
    /// <summary>
    /// CookieDes 的摘要描述
    /// </summary>
    public class CookieDes
    {
        #region 当前程序加密所使用的密钥
        /// <summary>
        /// 当前程序加密所使用的密钥
        /// </summary>
        public static readonly string myKey = "q0m3sd8l";
        #endregion

        public CookieDes()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        #region 创建Cookies
        /// <summary>
        /// 创建Cookies
        /// </summary>
        /// <param name="strName">Cookie 主键</param>
        /// <param name="strValue">Cookie 键值</param>
        /// <param name="strDay">Cookie 天数</param>
        /// <code>Cookie ck = new Cookie();</code>
        /// <code>ck.setCookie("主键","键值","天数");</code>
        public static void setCookie(string strName, string strValue, int strDay)
        {
            HttpCookie Cookie = new HttpCookie(strName);
            Cookie.Expires = DateTime.Now.AddDays(strDay);
            Cookie.Value = Encrypt(strValue, myKey);
            System.Web.HttpContext.Current.Response.Cookies.Add(Cookie);

        }
        #endregion

        #region 读取Cookies
        /**/
        /// <summary>
        /// 读取Cookies
        /// </summary>
        /// <param name="strName">Cookie 主键</param>
        /// <code>Cookie ck = new Cookie();</code>
        /// <code>ck.getCookie("主键");</code>
        public static string getCookie(string strName)
        {
            HttpCookie Cookie = System.Web.HttpContext.Current.Request.Cookies[strName];
            if (Cookie != null)
            {
                return Decrypt(Cookie.Value.ToString(), myKey);
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 删除Cookies
        /**/
        /// <summary>
        /// 删除Cookies
        /// </summary>
        /// <param name="strName">Cookie 主键</param>
        /// <code>Cookie ck = new Cookie();</code>
        /// <code>ck.delCookie("主键");</code>
        public static void delCookie(string strName)
        {
            HttpCookie Cookie = new HttpCookie(strName);
            Cookie.Expires = DateTime.Now.AddDays(-1);
            System.Web.HttpContext.Current.Response.Cookies.Add(Cookie);
        }
        #endregion

        #region Cookie加密方法
        /// <summary>
        /// 加密方法
        /// </summary>
        /// <param name="pToEncrypt">需要加密字符串</param>
        /// <param name="sKey">密钥</param>
        /// <returns>加密后的字符串</returns>
        public static string Encrypt(string pToEncrypt, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            //把字符串放到byte数组中


            //原来使用的UTF8编码，我改成Unicode编码了，不行
            byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);

            //建立加密对象的密钥和偏移量


            //使得输入密码必须输入英文文本
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);

            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            return ret.ToString();
        }
        #endregion

        #region Cookie加密方法
        /// <summary>
        /// 解密方法
        /// </summary>
        /// <param name="pToDecrypt">需要解密的字符串</param>
        /// <param name="sKey">密匙</param>
        /// <returns>解密后的字符串</returns>
        public static string Decrypt(string pToDecrypt, string sKey)
        {
            string reValue = "";
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
                for (int x = 0; x < pToDecrypt.Length / 2; x++)
                {
                    int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                    inputByteArray[x] = (byte)i;
                }

                //建立加密对象的密钥和偏移量，此值重要，不能修改
                des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                //建立StringBuild对象，CreateDecrypt使用的是流对象，必须把解密后的文本变成流对象
                StringBuilder ret = new StringBuilder();
                reValue = System.Text.Encoding.Default.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return reValue;

        }
        #endregion


    }
}
