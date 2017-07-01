using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting;
using SmartKernel.Net;

namespace MonitorClient
{
    class Client
    {
        PortInfo pi = new PortInfo();
        void MonitorControled()
        {
            try
            {
                TcpServerChannel channel = new TcpServerChannel(pi.GetPort());
                ChannelServices.RegisterChannel(channel,false);
                RemotingConfiguration.RegisterWellKnownServiceType(typeof(Monitor), "MonitorServerUrl", WellKnownObjectMode.SingleCall);
            }
            catch(Exception ex) 
            {
                throw ex;
            }
        }
    }
}
