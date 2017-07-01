using System;

using FluorineFx;

namespace ServiceLibrary
{
    /// <summary>
    /// Summary description for SecureService
    /// </summary>
    [RemotingService()]
    public class SecureService
    {
        public SecureService()
        {
        }

        [FluorineFx.Json.JsonRpcMethod]
        public string GetSecureData()
        {
            return "Secure data sent from server.";
        }
    }
}