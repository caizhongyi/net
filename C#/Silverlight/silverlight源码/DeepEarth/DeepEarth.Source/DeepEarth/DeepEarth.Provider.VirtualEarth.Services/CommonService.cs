using System;

namespace DeepEarth.Provider.VirtualEarth.Services.TokenWebReference
{
	partial class CommonService
	{
		public IAsyncResult BeginGetClientToken(TokenSpecification tokenSpec, AsyncCallback callback, object asyncState)
		{
			return this.BeginInvoke("GetClientToken", new object[] { tokenSpec }, callback, asyncState);
		}

		public string EndGetClientToken(IAsyncResult asyncResult)
		{
			object[] results = this.EndInvoke(asyncResult);
			return ((string)(results[0]));
		}
	}
}
