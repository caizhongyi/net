using System;
using System.Collections.Generic;
using System.Text;
using BBL.Inface;
using BBL.Opeation;
namespace BBL
{
    public class BBLFactory
    {
        public static IAdvInfo GetAdvInfo()
        {
            return new  BBL.Opeation.AdvInfo();
        }
        public static IAdvIssueInfo GetAdvIssueInfo()
        {
            return new AdvIssueInfo();
        }
        public static  IAdvTypeInfo GetAdvTypeInfo()
        {
            return new AdvTypeInfo();
        }
        public static  IAdvUnitPriceInfo GetAdvUnitPriceInfo()
        {
            return new AdvUnitPriceInfo();

        }     
        public static IAreaInfo  GetAreaInfo()
        {
            return new AreaInfo();
        }
         public static IProvinceInfo GetProvinceInfo()
        {
            return new ProvinceInfo();
        }
          public static IUserInfo  GetUserInfo()
        {
            return new UserInfoService();
        }
        public static IUserTypeInfo GetUserTypeInfo()
        {
            return new UserTypeInfo();
        }
        public static IWbInfo GetWbInfo()
        {
            return new WbInfo();
        }
           public static IWbRankInfo GetWbRankInfo ()
        {
            return new WbRankInfo();
        }
        
    }
}
