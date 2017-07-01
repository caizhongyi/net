using System.Configuration;
using System.Web.Services.Protocols;
namespace WebService1
{
    public class MySoapHeader : SoapHeader
    {
        private string _Password;
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }
    }
    public class HeaderCheck
    {
        private static string _syspass;
        static HeaderCheck()
        {
            _syspass = ConfigurationManager.AppSettings["headerpass"];
        }
        public static void check(UploadService srv)
        {
            if (srv.SoapHeader == null)
            {
                throw new SoapException("身份验证失败", SoapException.ClientFaultCode, "Security");
            }
            string md5 = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(_syspass, "MD5").ToLower();
            if (srv.SoapHeader.Password != md5)
            {
                throw new SoapException("身份验证失败", SoapException.ClientFaultCode, "Security");
            }
        }
    }
}
