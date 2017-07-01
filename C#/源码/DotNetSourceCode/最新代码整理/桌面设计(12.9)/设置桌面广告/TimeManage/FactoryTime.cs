using System;
using System.Collections.Generic;
using System.Text;

namespace WbSystem.TimeManage
{
    public class FactoryTime
    {
        public static ICompareTime GetTimeInfo()
        {
            return new CompareTime();
        }
    }
}
