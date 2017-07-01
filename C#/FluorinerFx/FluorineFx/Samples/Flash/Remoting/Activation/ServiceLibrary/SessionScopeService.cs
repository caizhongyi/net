using System;
using FluorineFx;

namespace ServiceLibrary
{
    /// <summary>
    /// Fluorine sample service.
    /// </summary>
    [RemotingService("Fluorine sample service")]
    class SessionScopeService
    {
        int _counter;

        public SessionScopeService()
		{
		}

        public int GetGlobalCounter()
        {
            return ++_counter;
        }
    }
}
