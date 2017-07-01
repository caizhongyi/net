using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// 时间
/// </summary>
public static class DataTimeExtender
{
    #region 星期转为中文
    /// <summary>
    /// 星期转为中文
    /// </summary>
    /// <returns>中文的星期</returns>
    public static string GetDayOfWeekCN(this DateTime dt)
    {
        switch (dt.DayOfWeek.ToString())
        {
            case "Monday": return "星期一";

            case "Tuesday": return "星期二";

            case "Wednesday": return "星期三";

            case "Thursday ": return "星期四";

            case "Friday ": return "星期五";

            case "Saturday": return "星期六";

            case "Sunday": return "星期日";

            default: return "";
        }

    }
    #endregion


    
}


