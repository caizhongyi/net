using System;
using FluorineFx.Context;
using FluorineFx.Messaging.Api;
using FluorineFx.Messaging.Api.Stream;

namespace ServiceLibrary
{
    class AuthNamePublishSecurity : IStreamPublishSecurity
    {
        #region IStreamPublishSecurity Members

        public bool IsPublishAllowed(IScope scope, string name, string mode)
        {
            if (!"live".Equals(mode))
            {
                // Not a live stream
                return false;
            }

            IConnection connection = FluorineContext.Current.Connection;
            if (!"authenticated".Equals(connection.GetAttribute("UserType")))
            {
                // User was not authenticated
                return false;
            }

            if (!name.StartsWith("testing"))
                return false;
            else
                return true;
        }

        #endregion
    }
}
