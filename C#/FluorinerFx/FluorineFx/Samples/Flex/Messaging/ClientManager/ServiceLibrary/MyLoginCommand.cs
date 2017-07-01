using System;
using System.Collections;
using System.Security;
using System.Security.Principal;

using FluorineFx.Security;

namespace ServiceLibrary
{
    /// <summary>
    /// Summary description for MyLoginCommand
    /// </summary>
    public class MyLoginCommand : GenericLoginCommand
    {
        public MyLoginCommand()
        {
        }

        public override IPrincipal DoAuthentication(string username, Hashtable credentials)
        {
            string clientId = FluorineFx.Context.FluorineContext.Current.ClientId;
            string password = credentials["password"] as string;
            if (username == "admin" && password == "admin")
            {
                GenericIdentity identity = new GenericIdentity(username);
                GenericPrincipal principal = new GenericPrincipal(identity, new string[] { "admin", "privilegeduser" });
                return principal;
            }
            else
                return null;
        }
    }
}