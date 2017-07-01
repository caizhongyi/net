using System;
using System.ServiceModel;
using System.Windows;

namespace DeepEarth.Provider.VirtualEarth
{
    public class DynamicTokenServiceBinding
    {
        public DynamicTokenServiceBinding()
        {
            var serviceUrl = new Uri(Application.Current.Host.Source , "../Services/VETokenService.svc");

            var _Binding = new BasicHttpBinding();
            _Binding.Security.Mode = BasicHttpSecurityMode.None;
            _Binding.MaxReceivedMessageSize = 2147483647;
            _Binding.MaxBufferSize = 2147483647;

            var _EndPoint = new EndpointAddress(serviceUrl);

            Token.SetBinding(_Binding, _EndPoint);
        }
    }
}