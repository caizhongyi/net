using System;
using System.ServiceModel;

namespace DeepEarth.Provider.VirtualEarth.Services
{
	[ServiceContract(
		Name = "ITokenService",
        ConfigurationName = "ITokenService",
		Namespace = "http://codeplex.com/deepearth")]
	public interface ITokenService
	{
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginGetToken(AsyncCallback callback, object asyncState);
        
		string EndGetToken(IAsyncResult asyncResult);
	}
}
