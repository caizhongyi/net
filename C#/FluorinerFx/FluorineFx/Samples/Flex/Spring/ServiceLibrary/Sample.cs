using System;
using FluorineFx;

namespace ServiceLibrary
{
	/// <summary>
	/// Fluorine sample service.
	/// </summary>
	[RemotingService("Fluorine sample service")]
	public class Sample
	{
		public Sample()
		{
		}

		public string Echo(string text)
		{
			return "Gateway echo: " + text;
		}
	}
}
