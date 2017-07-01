using System;
using System.Collections;

namespace ServiceLibrary
{
    [FluorineFx.RemotingService]
    public class ComputerInfoService
    {

        public Hashtable GetComputerStats()
        {
            Hashtable result = new Hashtable();
            result["os"] = Environment.OSVersion.ToString();
            result["clr"] = Environment.Version.ToString();
            double uptime = (Environment.TickCount / 3600000);
            result["uptime"] = uptime.ToString() + " :Hours";
            return result;
        }
    }
}
