using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace CpuLoad
{
    public class LoadCpu:ILoadCpu
    {   
        private const string CategoryName = "Processor";
        private const string CounterName = "% Processor Time";
        private const string InstanceName = "_Total";
        [STAThread]
        public bool IsZero()
        {
            PerformanceCounter pc = new PerformanceCounter(CategoryName, CounterName, InstanceName);
            while (true)
            {
                Thread.Sleep(1500);
                float cpuLoad = pc.NextValue();
                if (cpuLoad < 0.2)
                {
                    return true;
                }
            }
        }
    }
}
