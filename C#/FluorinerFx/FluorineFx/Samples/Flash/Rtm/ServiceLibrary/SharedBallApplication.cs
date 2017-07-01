using System;
using FluorineFx.Messaging.Api;
using FluorineFx.Messaging.Adapter;
using FluorineFx.Messaging.Api.SO;
using FluorineFx.Messaging.Api.Service;
using FluorineFx.Messaging.Api.Stream;

namespace ServiceLibrary
{
	/// <summary>
	/// Summary description for AppHandler.
	/// </summary>
    public class SharedBallApplication : ApplicationAdapter
	{
        public SharedBallApplication()
		{
		}

        public override bool AppStart(IScope application)
        {
            RegisterSharedObjectSecurity(new SampleSOSecurityHandler());
            return base.AppStart(application);
        }
    }
}
