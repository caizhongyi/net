using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace YZWBSM
{
    class ClsUtility
    {

        [DllImport("kernel32")]
        public static extern bool RemoveDirectory(string lpPathName);
        [DllImport("kernel32.dll")]
        public static extern bool Beep(int frequency, int duration); 

    }
}
