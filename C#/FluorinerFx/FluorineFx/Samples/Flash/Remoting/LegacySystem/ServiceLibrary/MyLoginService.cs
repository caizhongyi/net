using System;
using FluorineFx;

namespace ServiceLibrary
{
    [RemotingService("LoginService")]
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
            FluorineFx.Context.FluorineContext.Current.ClearPrincipal();
            return true;
        }
    }
}
