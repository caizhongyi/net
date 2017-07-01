using System;

namespace JuSNS.Web
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }


        protected void Application_End(object sender, EventArgs e)
        {

        }
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception x = Server.GetLastError().GetBaseException();
            Response.Write(x.Message);
            Response.End();
        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {

        }
    }
}