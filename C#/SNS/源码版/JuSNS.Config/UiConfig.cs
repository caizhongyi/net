using System;
using System.Configuration;

namespace JuSNS.Config
{
    public class UiConfig
    {
        public static readonly string WebDAL = ConfigurationManager.AppSettings["WebDAL"];
        public static readonly string PageTitle = BaseConfig.GetConfigValue("webName");
        public static readonly string ExName = BaseConfig.GetConfigValue("siteExName");
        public static readonly string SiteName = BaseConfig.GetConfigValue("SiteName");
        public static readonly string Domain = BaseConfig.GetConfigValue("Domain");
        public static readonly string RootUrl = BaseConfig.GetConfigValue("Url");
        public static readonly string Keywords = BaseConfig.GetConfigValue("keywords");
        public static readonly string Description = BaseConfig.GetConfigValue("description");
        public static readonly string CopyRight = BaseConfig.GetConfigValue("copyright");
        public static readonly string SkinStyle = BaseConfig.GetConfigValue("SkinStyle");
        public static readonly string gName = BaseConfig.GetConfigValue("gName");
        public static readonly string MiniType = BaseConfig.GetConfigValue("MiniType");
        public static readonly string Openurl = BaseConfig.GetConfigValue("openurl");
        public static readonly string PVersion = "<a href=\"http://www.julaa.com\" target=\"_blank\" title=\"官方演示站\">JuSNS</a> <a href=\"http://www.jusns.com/download.html\" title=\"jusns下载中心\" target=\"_blank\">2.0.5</a>";
        public static readonly string Version = PVersion;
        public static readonly string RegVer = BaseConfig.GetConfigValue("RegVer");
        public static readonly string isRealName = BaseConfig.GetConfigValue("isRealName");
        public static readonly string URL = BaseConfig.GetConfigValue("Url");
        public static readonly string CookieDomain = BaseConfig.GetConfigValue("CookieDomain");
        public static readonly string isUpHeadPic = BaseConfig.GetConfigValue("isUpHeadPic");
        public static readonly string isShortmenu = BaseConfig.GetConfigValue("isShortmenu");
        public static readonly string ValidateIP = BaseConfig.GetConfigValue("ValidateIP");
        public static readonly string SpaceUrl = BaseConfig.GetConfigValue("Spaceurl");
        public static readonly string CookieVerifyCode = BaseConfig.GetConfigValue("CookieVerifyCode");
        public static readonly string siteExName = BaseConfig.GetConfigValue("siteExName");
        public static readonly string PicSize = BaseConfig.GetConfigValue("picsize");
        public static readonly string PicType = BaseConfig.GetConfigValue("pictype");
        public static readonly int CityNumber = Convert.ToInt32(BaseConfig.GetBaseConfigValue("cityNumber"));
        public static readonly int ATTNumber = Convert.ToInt32(BaseConfig.GetBaseConfigValue("attNumber"));
        public static readonly int FNumber = Convert.ToInt32(BaseConfig.GetBaseConfigValue("fNumber"));
        public static readonly int VNumber = Convert.ToInt32(BaseConfig.GetBaseConfigValue("vNumber"));
        public static readonly int PNumber = Convert.ToInt32(BaseConfig.GetBaseConfigValue("pNumber"));
        public static readonly int SearchNumber = Convert.ToInt32(BaseConfig.GetBaseConfigValue("SearchNumber"));
        public static readonly int Magic = Convert.ToInt32(BaseConfig.GetBaseConfigValue("Magic"));
        public static readonly string memberleves = BaseConfig.GetConfigValue("memberleves");
    }
}
