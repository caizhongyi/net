using System;
using System.Net;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Threading;
using System.Web;
using DeepEarth.Provider.VirtualEarth.Services.TokenWebReference;

namespace DeepEarth.Provider.VirtualEarth.Services
{
	[ServiceBehavior(
		Name = "TokenService",
        ConfigurationName = "TokenService",
		Namespace = "http://codeplex.com/deepearth")]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public class TokenService : ITokenService
	{
		private const string cacheKey = "VEToken";
		private readonly CommonService commonservice;

        /// <summary>
        /// In Order to use the Virtual Earth services from Silverlight you need a token.
        /// We have this async service on the server side to request on demand
        /// We cache the token for the session period to reduce transactions if the user refreshes the page.
        /// For a more a basic blocking version see the end of this file for example
        /// </summary>
		public TokenService()
		{
			try {
				commonservice = new CommonService {
					Credentials =
						new NetworkCredential(ConfigurationManager.AppSettings["VETokenUserName"],
							ConfigurationManager.AppSettings["VETokenPassword"])
				};

				var serviceUrl = ConfigurationManager.AppSettings["VETokenServiceUrl"];
				if (!string.IsNullOrEmpty(serviceUrl))
				{
					commonservice.Url = serviceUrl;
				}
            } catch (Exception e)
                {
                    //You need to get a Virtual Earth Web Service (VEWS) ID and password 
                    //by setting up a developer account at https://mappoint-css.live.com/mwssignup 
                    //in order to use VE tiles and services. A free 90 day account takes about 2 hours
                    //to be provisioned.

                    //if (e.Message == "The request failed with HTTP status 401: Unauthorized.")
                    //Error Getting Token for Virtual Earth Service, have you set your ID and Password in the web.config file?
                    throw;
                }
		}

		public IAsyncResult BeginGetToken(AsyncCallback callback, object asyncState) {
			if (HttpContext.Current.Session[cacheKey] == null) {
				// The maximum allowable token duration is 480 minutes (8 hours).
				// The minimum allowable duration is 15 minutes.
				var tokenSpec = new TokenSpecification {
					ClientIPAddress = HttpContext.Current.Request.UserHostAddress,
					TokenValidityDurationMinutes = 60
				};
                //IPv6 fix for local dev
                if (tokenSpec.ClientIPAddress == "::1")
                {
                    tokenSpec.ClientIPAddress = "127.0.0.1";
                }
				return commonservice.BeginGetClientToken(tokenSpec, callback, asyncState);
			}
			//complete the Async call immediately with the cached value
			var asyncResult = new CompletedAsyncResult((string)HttpContext.Current.Session[cacheKey], callback, asyncState);
			ThreadPool.QueueUserWorkItem(Callback, asyncResult);
			return asyncResult;
		}

		public string EndGetToken(IAsyncResult asyncResult) {
			string result;
			if (asyncResult.CompletedSynchronously) {
				result = ((CompletedAsyncResult)asyncResult).Result;
			} else {
				try {
					result = commonservice.EndGetClientToken(asyncResult);
					HttpContext.Current.Session[cacheKey] = result;
				} catch (Exception e) {
					//You need to get a Virtual Earth Web Service (VEWS) ID and password 
					//by setting up a developer account at https://mappoint-css.live.com/mwssignup 
					//in order to use VE tiles and services. A free 90 day account takes about 2 hours
					//to be provisioned.

                    //if (e.Message == "The request failed with HTTP status 401: Unauthorized.")
                    //Error Getting Token for Virtual Earth Service, have you set your ID and Password in the web.config file?
					throw;
				}
			}
			// Return the asynchronous result from the other Web service.
			return result;
		}

		private static void Callback(object state) {
			var asyncResult = state as CompletedAsyncResult;
			if (asyncResult != null)
				asyncResult.Complete();
		}

		#region CompletedAsyncResult

		sealed class CompletedAsyncResult : IAsyncResult, IDisposable
		{
			readonly AsyncCallback callback;
			readonly ManualResetEvent manualResentEvent;
			readonly object state;

			public CompletedAsyncResult(string result, AsyncCallback callback, object state) {
				this.callback = callback;
				this.state = state;
				manualResentEvent = new ManualResetEvent(false);
				Result = result;
			}

			public string Result {
				get;
				set;
			}

			public ManualResetEvent AsyncWait {
				get {
					return manualResentEvent;
				}
			}

			#region IAsyncResult Members

			object IAsyncResult.AsyncState {
				get {
					return state;
				}
			}

			WaitHandle IAsyncResult.AsyncWaitHandle {
				get {
					return AsyncWait;
				}
			}

			bool IAsyncResult.CompletedSynchronously {
				get {
					return true;
				}
			}

			bool IAsyncResult.IsCompleted {
				get {
					return manualResentEvent.WaitOne(0, false);
				}
			}

			#endregion

			#region IDisposable Members

			public void Dispose() {
				manualResentEvent.Close();
			}

			#endregion

			public void Complete() {
				manualResentEvent.Set();
				if (callback != null) {
					callback(this);
				}
			}
		}

		#endregion

        //Simple no async version example you would see in simple SDK docs:

        //public string GetToken() {
        //    var token = HttpContext.Current.Session[cacheKey] as string;
        //    if (string.IsNullOrEmpty(token))
        //    {
        //        // The maximum allowable token duration is 480 minutes (8 hours).
        //        // The minimum allowable duration is 15 minutes.
        //        var tokenSpec = new TokenSpecification {
        //            ClientIPAddress = HttpContext.Current.Request.UserHostAddress,
        //            TokenValidityDurationMinutes = 60
        //        };
        //        token = commonservice.GetClientToken(tokenSpec);
        //        HttpContext.Current.Session[cacheKey] = token;
        //    }

        //    return token;
        //}
	}
}
