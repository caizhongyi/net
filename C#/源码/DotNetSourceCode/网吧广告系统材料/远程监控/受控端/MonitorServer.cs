/************************************************************/
//【项目】：远程监控
//【创建】：2005年10月
//【作者】：SmartKernel
//【邮箱】：smartkernel@126.com
//【QQ  】：120018689
//【MSN 】：smartkernel@hotmail.com
//【网站】：www.SmartKernel.com
/************************************************************/

using System;
using System.Runtime.Remoting;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace SmartKernel.Net
{
    class MonitorServer
    {
        static void Main(string[] args)
        {
            try
            {
                TcpServerChannel channel = new TcpServerChannel(8888);
                ChannelServices.RegisterChannel(channel, false);
                RemotingConfiguration.RegisterWellKnownServiceType(typeof(Monitor), "MonitorServerUrl", WellKnownObjectMode.SingleCall);
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message );
                Console.ReadLine();
            }
        }
    }
}
