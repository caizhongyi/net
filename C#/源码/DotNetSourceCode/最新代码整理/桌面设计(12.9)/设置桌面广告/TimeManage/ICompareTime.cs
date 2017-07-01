using System;
using System.Collections.Generic;
using System.Text;

namespace WbSystem.TimeManage
{
    public interface ICompareTime
    {
        bool GetTime(DateTime oldtime,DateTime newtime);
    }
}
