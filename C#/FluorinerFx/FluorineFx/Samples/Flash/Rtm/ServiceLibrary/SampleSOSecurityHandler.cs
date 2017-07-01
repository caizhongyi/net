using System;
using System.Collections;
using FluorineFx.Messaging.Api;
using FluorineFx.Messaging.Api.SO;

namespace ServiceLibrary
{
    class SampleSOSecurityHandler : ISharedObjectSecurity 
    {
        #region ISharedObjectSecurity Members

        public bool IsConnectionAllowed(ISharedObject so)
        {
            // Note: we don't check for the name here as only one SO can be
            //       created with this handler.
            return true;
        }

        public bool IsCreationAllowed(IScope scope, string name, bool persistent)
        {
            if (!"position".Equals(name) || !persistent)
                return false;
            else
                return true;
        }

        public bool IsDeleteAllowed(ISharedObject so, string key)
        {
            return false;
        }

        public bool IsSendAllowed(ISharedObject so, string message, IList arguments)
        {
            if (!"drop".Equals(message))
                return false;
            else
                return true;
        }

        public bool IsWriteAllowed(ISharedObject so, string key, object value)
        {
            return true;
        }

        #endregion
    }
}
