using System;
using System.Collections.Generic;
using System.Text;

namespace NetFiles.Common
{
    public class Config
    {
        public static System.Net.IPAddress IPAddress
        {
            get
            {
                return System.Net.IPAddress.Parse(System.Configuration.ConfigurationSettings.AppSettings["ipaddress"]);
            }
        }
        public static int IPPort
        {
            get
            {
                return int.Parse(System.Configuration.ConfigurationSettings.AppSettings["port"]);
            }
        }
        private static HFSoft.Net.NetServer mServer;
        public static HFSoft.Net.NetServer Server
        {
            get
            {
                if (mServer == null || mServer.Listener == null)
                {
                    mServer = new HFSoft.Net.NetServer(new System.Net.IPEndPoint(IPAddress,IPPort),typeof(HFSoft.Net.ReceiveHandler));
                }
                return mServer;
            }
        }
        public static void ControlEnable(bool enable, params System.Windows.Forms.Control[] controls)
        {
            foreach (System.Windows.Forms.Control item in controls)
            {
                item.Enabled = enable;
            }
        }
        //ToolStripItem 
        public static void ControlEnable(bool enable, params System.Windows.Forms.ToolStripItem[] controls)
        {
            foreach (System.Windows.Forms.ToolStripItem item in controls)
            {
                item.Enabled = enable;
            }
        }
        public static HFSoft.Net.IChannel ClientChannel(System.Threading.ManualResetEvent mr)
        {
            return HFSoft.Net.NetServer.CreateChannel(new System.Net.IPEndPoint(IPAddress, IPPort), new HFSoft.Net.ReceiveHandler(),
                mr);
        }
        public static HFSoft.Net.IChannel ClientChannel(System.Threading.ManualResetEvent mr,HFSoft.Net.IReceiveHandler hander)
        {
            return HFSoft.Net.NetServer.CreateChannel(new System.Net.IPEndPoint(IPAddress, IPPort), hander,
                mr);
        }
    
    
    }
}
