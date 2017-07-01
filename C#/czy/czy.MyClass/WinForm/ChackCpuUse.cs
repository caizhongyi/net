using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace czy.MyClass.WinForm
{
    class ChackCpuUse
    {
        [STAThread]
        public static string main()
        {
            //Say("$Id: CpuLoadInfo.cs,v 1.2 2002/08/17 17:45:48 rz65 Exp $");
            //Say();

            //Say("Attempt to create a PerformanceCounter instance:");
            //Say("Category name = " + CategoryName);
            //Say("Counter name = " + CounterName);
            //Say("Instance name = " + InstanceName);
            PerformanceCounter pc = new PerformanceCounter(CategoryName, CounterName, InstanceName);
            //Say("Performance counter was created.");
            //Say("Property CounterType: " + pc.CounterType);
            //Say();

            //Say("Property CounterHelp: " + pc.CounterHelp);
            //Say();
            //Say("Entering measurement loop.");

            while (true)
            {
                Thread.Sleep(1000); // wait for 1 second
                float cpuLoad = pc.NextValue();
                return "CPU load = " + cpuLoad + " %.";
            }
        }
        private const string CategoryName = "Processor";
        private const string CounterName = "% Processor Time";
        private const string InstanceName = "_Total";
    }
}
