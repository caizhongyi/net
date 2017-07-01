using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace JuSNS.Common
{
    /// <summary> 
    /// DES �ӽ���
    /// </summary> 
    public class DES
    {
        /// <summary>
        /// DES����
        /// </summary>
        /// <param name="input">�����ܵ��ַ���</param>
        /// <param name="key">������Կ</param>
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
        /// DES����
        /// </summary>
        /// <param name="input">�����ܵ��ַ���</param>
        /// <param name="key">������Կ,Ҫ��Ϊ8λ,�ͼ�����Կ��ͬ</param>
        /// <returns>���ܳɹ����ؽ��ܺ���ַ���,ʧ�ܷ�Դ��</returns>
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
    /// RSA�ӽ����㷨
    /// </summary>
    public class RSA
    {
        /// <summary>
        /// RSA���ܺ���
        /// </summary>
        /// <param name="xmlPublicKey">˵��KEY������XML����ʽ,���ص����ַ���</param>
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
        /// RSA���ܺ���
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
        /// ����RSA����Կ
        /// </summary>
        /// <param name="xmlKeys">˽Կ</param>
        /// <param name="xmlPublicKey">��Կ</param>
        public void RSAKey(out string xmlKeys, out string xmlPublicKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            xmlKeys = rsa.ToXmlString(true);
            xmlPublicKey = rsa.ToXmlString(false);
        }
    }
}
