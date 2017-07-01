using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace czy.MyClass.Encrypt
{
    /// <summary>
    /// MD5加密
    /// </summary>
    public class MD5Encrypt
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str1"></param>
        /// <returns></returns>
        public static string Md5Code(string result2)
        {
            System.Security.Cryptography.MD5 md5_2 = System.Security.Cryptography.MD5.Create();
            byte[] bytes2 = md5_2.ComputeHash(System.Text.Encoding.UTF8.GetBytes(result2));
            result2 = string.Empty;
            for (int i = 0; i < bytes2.Length; i++)
            {
                result2 += string.Format("{0:X2}", bytes2[i]);
            }
            return result2;


        }
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="result3"></param>
        /// <returns></returns>
        private static string Md5Code_1(string result3)
        {
            System.Security.Cryptography.MD5 md5_3 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bytes3 = md5_3.ComputeHash(System.Text.Encoding.UTF8.GetBytes(result3));
            result3 = string.Empty;
            for (int i = 0; i < bytes3.Length; i++)
            {
                result3 += string.Format("{0:X2}", bytes3[i]);
            }
            return result3;

        }
        /// <summary>
        /// 支付宝MD5
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetMD5(string s)
        {

            /// <summary>
            /// 与ASP兼容的MD5加密算法
            /// </summary>

            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(Encoding.GetEncoding("utf-8").GetBytes(s));
            StringBuilder sb = new StringBuilder(32);
            for (int i = 0; i < t.Length; i++)
            {
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }
    }
}
