using System;
using FluorineFx.Context;
using FluorineFx.Messaging.Api;
using FluorineFx.Messaging.Adapter;

namespace ServiceLibrary
{
    public class BWCheckApplication : ApplicationAdapter
    {
        public override bool AppConnect(IConnection connection, object[] parameters)
        {
            CalculateClientBw();
            return base.AppConnect(connection, parameters);
        }
    }
}
