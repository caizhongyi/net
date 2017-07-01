using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace czy.MyClass.Encrypt
{
    /// <summary>
    /// AES加密
    /// </summary>
    class AESEncryption
    {
        //默认密钥向量    
        private static byte[] _key1 = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF, 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        /// <summary>   
        /// AES加密算法   
        /// </summary>  
        /// <param name="cipherText">明文字节数组</param>   
        /// <param name="strKey">密钥</param>   
        /// <returns>返回加密后的字符串</returns>   
        public static byte[] AESEncrypt(byte[] inputByteArray, string strKey)
        {
            //分组加密算法   
            SymmetricAlgorithm des = Rijndael.Create();
            //设置密钥及密钥向量   
            des.Key = Encoding.UTF8.GetBytes(strKey);
            des.IV = _key1;
            des.BlockSize = 128;
            des.Mode = CipherMode.CBC;
            des.Padding = PaddingMode.Zeros;
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            byte[] cipherBytes = ms.ToArray();//得到加密后的字节数组   
            cs.Close();
            ms.Close();
            return cipherBytes;
        }

        /// <summary>   
        /// AES解密   
        /// </summary>   
        /// <param name="cipherText">密文字节数组</param>   
        /// <param name="strKey">密钥</param>   
        /// <returns>返回解密后的字符串</returns>   
        public static byte[] AESDecrypt(byte[] cipherText, string strKey)
        {
            SymmetricAlgorithm des = Rijndael.Create();
            des.Key = Encoding.UTF8.GetBytes(strKey);
            des.IV = _key1;
            des.BlockSize = 128;
            des.Padding = PaddingMode.Zeros;
            des.Mode = CipherMode.CBC;
            byte[] decryptBytes = new byte[cipherText.Length];
            MemoryStream ms = new MemoryStream(cipherText);
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Read);
            cs.Read(decryptBytes, 0, decryptBytes.Length);
            cs.Close();
            ms.Close();
            return decryptBytes;
        }
    }
}
