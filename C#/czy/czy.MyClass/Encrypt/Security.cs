using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace czy.MyClass
{
    public class Security
    {
        public enum Type
        {
            MD5,
            SHA1
        }
        public string EncryptPassword(string PasswordString, Type EncryptType)
        {
            if (EncryptType == Type.MD5)
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(PasswordString, "MD5");
            }
            else
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(PasswordString, "SHA1");
            }

        }

    }
}
