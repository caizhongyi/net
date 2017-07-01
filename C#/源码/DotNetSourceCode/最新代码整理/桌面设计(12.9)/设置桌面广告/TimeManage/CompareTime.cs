using System;
using System.Collections.Generic;
using System.Text;

namespace WbSystem.TimeManage
{
    class CompareTime:ICompareTime
    {

        public bool GetTime(DateTime oldtime, DateTime newtime)
        {
            DateTime t1 = oldtime;
            DateTime t2= newtime;
            return (DateTime.Compare(t1, t2) >= 0) ? true : false;

        }

    }
}
