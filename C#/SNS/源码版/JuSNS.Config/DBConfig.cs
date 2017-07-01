using System.Configuration;

namespace JuSNS.Config
{
    public class DBConfig
    {
        public static readonly string CnString = ConfigurationManager.ConnectionStrings["snsconstr"].ConnectionString;
    }
}
