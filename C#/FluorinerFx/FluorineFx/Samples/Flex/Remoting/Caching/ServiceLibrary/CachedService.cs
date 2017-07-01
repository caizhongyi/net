using System;
using FluorineFx;

namespace ServiceLibrary
{
	/// <summary>
	/// FluorineFx sample service.
	/// </summary>
    [RemotingService("FluorineFx CachedService")]
	public class CachedService
	{
        public CachedService()
		{
		}

        public string Echo()
        {
            return "Gateway echo.";
        }

		public string Echo1(string text)
		{
			return "Gateway echo: " + text;
		}

        public string Echo2(int val, DateTime date)
        {
            return "Gateway echo: " + val.ToString() + ", " + date.ToString();
        }
	}
}
