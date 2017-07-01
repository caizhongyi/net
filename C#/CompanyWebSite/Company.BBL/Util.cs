using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Company.BBL
{
    public class Util
    {
        private static string connectString = System.Configuration.ConfigurationSettings.AppSettings["constr"].ToString();

        public static string ConnectString
        {
            get { return Util.connectString; }
            set { Util.connectString = value; }
        }
    }
}
