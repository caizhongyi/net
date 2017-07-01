using System;
using System.Data;
using System.Configuration;

namespace ServiceLibrary
{
    /// <summary>
    /// MyLoginService is used to force setCredentials sending out credentials
    /// For Flash this is also used to log out.
    /// </summary>
    [FluorineFx.RemotingService]
    public class MyLoginService
    {
        public MyLoginService()
        {
        }

        public bool Login()
        {
            return true;
        }

        public bool Logout()
        {
            //FormsAuthentication.SignOut();
            new MyLoginCommand().Logout(null);
            return true;
        }
    }
}