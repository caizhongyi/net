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
            string password = credentials["password"] as string;
            if (username == "admin" && password == "admin")
            {
                GenericIdentity identity = new GenericIdentity(username);
                GenericPrincipal principal = new GenericPrincipal(identity, new string[] { "admin", "privilegeduser" });
                return principal;
            }
            else
            {
                //To get this error message as "faultString" thow a SecurityException with the message
                //However the first CommandMessage will mess up the result (faultString>faultDetail), and only for subsequent RemotingMessages will work.
                //throw new SecurityException("Not a valid username or password");
                return null;
            }
        }
    }
}