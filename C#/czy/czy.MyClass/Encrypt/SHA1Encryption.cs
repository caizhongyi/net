using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Web.Security;
using System.Web.Configuration;

namespace czy.MyClass.Encrypt
{
    /// <summary>
    /// 类据加密(SHA1,RC2加密码)
    /// </summary>
    public class SHA1Encryption
    {
        #region  SHA1
        /// <summary>
        /// 加密 (为默认 SHA1)
        /// </summary>
        /// <param name="strValue">未加密的数据</param>
        public  static string GetHashValue( string strValue)
        {
            strValue = FormsAuthentication.HashPasswordForStoringInConfigFile(strValue, FormsAuthPasswordFormat.SHA1.ToString());
            return strValue;
        }

        #endregion
   

    

    }
}
