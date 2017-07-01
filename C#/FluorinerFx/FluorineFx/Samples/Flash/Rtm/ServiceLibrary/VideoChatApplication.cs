using System;
using FluorineFx.Context;
using FluorineFx.Messaging.Api;
using FluorineFx.Messaging.Adapter;
using FluorineFx.Messaging.Api.SO;
using FluorineFx.Messaging.Api.Service;
using FluorineFx.Messaging.Api.Stream;

namespace ServiceLibrary
{
    public class VideoChatApplication : ApplicationAdapter
    {
        public override bool AppStart(IScope application)
        {
            RegisterStreamPublishSecurity(new AuthNamePublishSecurity());
            return base.AppStart(application);
        }

        public override bool AppConnect(IConnection connection, object[] parameters)
        {
            //"authenticated" user...
            connection.SetAttribute("UserType", "authenticated");
            return base.AppConnect(connection, parameters);
        }
    }
}
