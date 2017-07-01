using System;
using FluorineFx;

namespace ServiceLibrary
{
	/// <summary>
	/// Fluorine sample service.
	/// </summary>
	[RemotingService("FluorineFx sample service")]
	public class Sample
	{
		public Sample()
		{
		}

        [Role("admin")]
        public string GetSecureData()
        {
            return "Secure data sent from server.";
        }
	}
}
