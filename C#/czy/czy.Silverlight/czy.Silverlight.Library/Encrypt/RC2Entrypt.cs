using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace czy.MyClass.Encrypt
{
    /// <summary>
    /// RC2加密与解密
    /// </summary>
    public class RC2Entrypt
    {
        #region 成员
        /// <summary>
        /// 加密密钥
        /// </summary>
        private const string strKEY = "TaSkManAger=";
        /// <summary>
        /// 初始化向量
        /// </summary>
        private const string strIV = "tAsKmANaGER=";
        #endregion

        #region 采用RC2方式执行对称加密
        /// <summary>
        /// 采用RC2方式执行对称加密
        /// </summary>
        /// <param name="strValue">未加密的数据</param>
        public static string Encrypt(string strValue)
        {
            SymmetricAlgorithm objRC2 = new RC2CryptoServiceProvider();

            ICryptoTransform objICT;
            MemoryStream objMS;
            CryptoStream objCS;
            byte[] byteValue;

            objICT = objRC2.CreateEncryptor(Convert.FromBase64String(strKEY), Convert.FromBase64String(strIV));

            byteValue = Encoding.UTF8.GetBytes(strValue);

            objMS = new MemoryStream();
            objCS = new CryptoStream(objMS, objICT, CryptoStreamMode.Write);
            objCS.Write(byteValue, 0, byteValue.Length);
            objCS.FlushFinalBlock();

            objCS.Close();

            strValue = Convert.ToBase64String(objMS.ToArray());
            return strValue;
        }
        #endregion

        #region 采用RC2方式执行对称解密
        /// <summary>
        /// 采用RC2方式执行对称解密
        /// </summary>
        /// <param name="strValue">已加密的数据</param>
        public static string Decrypt( string strValue)
        {
            SymmetricAlgorithm objRC2 = new RC2CryptoServiceProvider();

            ICryptoTransform objICT;
            MemoryStream objMS;
            CryptoStream objCS;
            byte[] byteValue;

            objICT = objRC2.CreateDecryptor(Convert.FromBase64String(strKEY), Convert.FromBase64String(strIV));

            byteValue = Convert.FromBase64String(strValue);

            objMS = new MemoryStream();
            objCS = new CryptoStream(objMS, objICT, CryptoStreamMode.Write);
            objCS.Write(byteValue, 0, byteValue.Length);
            objCS.FlushFinalBlock();

            objCS.Close();

            strValue = Encoding.UTF8.GetString(objMS.ToArray());
            return strValue;
        }
        #endregion
    }
}
