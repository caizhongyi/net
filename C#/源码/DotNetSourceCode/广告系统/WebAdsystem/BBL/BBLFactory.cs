using System;
using System.Collections.Generic;
using System.Text;

namespace BBL
{
    public class BBLFactory
    {
        //public static IBBLOPertion getAdType()
        //{
        //    return new BBLOperation();
        //}

        public static IAdInfo GetAdInfo()
        {
            return new AdInfo();
        }

        public static IWbInfo GetWbInfo()
        {
            return new WbInfo();
        }
        public static IUserInfo GetUserInfo()
        {
            return new UserInfo();
        }
     
    }
}
