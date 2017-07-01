using System;
using FluorineFx;

namespace ServiceLibrary
{
	/// <summary>
	/// Fluorine sample service.
	/// </summary>
	[RemotingService("Fluorine sample service")]
	public class ApplicationScopeService
	{
        int _counter;

        public ApplicationScopeService()
		{
		}

		public int GetGlobalCounter()
		{
            return ++_counter;
		}
	}
}
