using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace JuSNS.Common
{
    /// <summary> 
    /// DES 加解密
    /// </summary> 
    public class DES
    {
        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="input">待加密的字符串</param>
        /// <param name="key">加密密钥</param>
        /// <returns></returns>
        public static string Encrypt(string EncryptString, byte[] Key, byte[] IV)
        {
            //byte[] rgbKey = Encoding.UTF8.GetBytes(key.Substring(0, 8));
            byte[] inputByteArray = Encoding.UTF8.GetBytes(EncryptString);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, des.CreateEncryptor(Key, IV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Convert.ToBase64String(mStream.ToArray());
        }
        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="input">待解密的字符串</param>
        /// <param name="key">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串,失败返源串</returns>
        public static string Decrypt(string DecryptString, byte[] Key, byte[] IV)
        {
            try
            {
                //byte[] rgbKey = Encoding.UTF8.GetBytes(Key);
                byte[] inputByteArray = Convert.FromBase64String(DecryptString);
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();

                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, des.CreateDecryptor(Key, IV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return "";
            }
        }
    }
    /// <summary>
    /// RSA加解密算法
    /// </summary>
    public class RSA
    {
        /// <summary>
        /// RSA加密函数
        /// </summary>
        /// <param name="xmlPublicKey">说明KEY必须是XML的行式,返回的是字符串</param>
        /// <param name="EncryptString"></param>
        /// <returns></returns>
        public string Encrypt(string xmlPublicKey, string EncryptString)
        {
            byte[] PlainTextBArray;
            byte[] CypherTextBArray;
            string Result;
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlPublicKey);
            PlainTextBArray = (new UnicodeEncoding()).GetBytes(EncryptString);
            CypherTextBArray = rsa.Encrypt(PlainTextBArray, false);
            Result = Convert.ToBase64String(CypherTextBArray);
            return Result;
        }
        /// <summary>
        /// RSA解密函数
        /// </summary>
        /// <param name="xmlPrivateKey"></param>
        /// <param name="DecryptString"></param>
        /// <returns></returns>
        public string Decrypt(string xmlPrivateKey, string DecryptString)
        {
            byte[] PlainTextBArray;
            byte[] DypherTextBArray;
            string Result;
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlPrivateKey);
            PlainTextBArray = Convert.FromBase64String(DecryptString);
            DypherTextBArray = rsa.Decrypt(PlainTextBArray, false);
            Result = (new UnicodeEncoding()).GetString(DypherTextBArray);
            return Result;
        }

        /// <summary>
        /// 产生RSA的密钥
        /// </summary>
        /// <param name="xmlKeys">私钥</param>
        /// <param name="xmlPublicKey">公钥</param>
        public void RSAKey(out string xmlKeys, out string xmlPublicKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            xmlKeys = rsa.ToXmlString(true);
            xmlPublicKey = rsa.ToXmlString(false);
        }
    }
}
