using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace czy.MyClass
{
    /// <summary>
    /// MyDateTime 的摘要说明
    /// </summary>
    public class TimeHelper
    {
        public TimeHelper()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 获取年月日
        /// <summary>
        /// 获取当前时候100年份
        /// </summary>
        /// <returns></returns>
        public static int[] GetYear()
        {
            int comYear = 100;
            int[] yearArray = new int[comYear + 1];
            DateTime dt = DateTime.Now;
            int year = dt.Year;
            for (int i = 0; i <= comYear; i++)
            {
                yearArray[i] = year - comYear + i;
            }
            return yearArray;
        }
        /// <summary>
        /// 获取月份
        /// </summary>
        /// <returns></returns>
        public static int[] GetMonth()
        {
            int[] monthArray = new int[12];
            for (int i = 0; i < 12; i++)
            {
                monthArray[i] = i + 1;
            }
            return monthArray;
        }
        /// <summary>
        /// 获取天数
        /// </summary>
        /// <param name="year">当前年份</param>
        /// <param name="month">当前月份</param>
        /// <returns></returns>
        public static int[] GetDay(int year,int month)
        {
            int[] days;
            if (month == 2) //判断是否为2月
            {
                if (year % 400 == 0 || (year % 100 != 0 && year % 4 == 0))
                {
                    days = new int[28];
                    for (int i = 1; i <= 28; i++)
                    {
                        days[i-1] = i;
                    }
                    return days;
                }
                else
                {
                    days = new int[29];
                    for (int i = 1; i <= 29; i++)
                    {
                        days[i - 1] = i;
                    }
                    return days;
                }
            }
            else if ((month <= 7 && month != 2 && month % 2 != 0) || (month > 7 && month % 2 == 0))//判断是否为31天
            {
                days = new int[31];
                for (int i = 1; i <= 31; i++)
                {
                    days[i - 1] = i;
                }
                return days;
            }
            else
            {
                days = new int[30];
                for (int i = 1; i <= 30; i++)
                {
                    days[i - 1] = i;
                }
                return days;
            }

        }
        #endregion

        #region 把秒转换成分钟
        /// <summary>
        /// 把秒转换成分钟
        /// </summary>
        /// <returns></returns>
        public static int SecondToMinute(int Second)
        {
            decimal mm = (decimal)((decimal)Second / (decimal)60);
            return Convert.ToInt32(Math.Ceiling(mm));
        }
        #endregion

        #region 返回某年某月最后一天
        /// <summary>
        /// 返回某年某月最后一天
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>日</returns>
        public static int GetMonthLastDate(int year, int month)
        {
            DateTime lastDay = new DateTime(year, month, new System.Globalization.GregorianCalendar().GetDaysInMonth(year, month));
            int Day = lastDay.Day;
            return Day;
        }
        #endregion

        #region 返回时间差
        public static string DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            string dateDiff = null;
            try
            {
    
                TimeSpan ts = DateTime2 - DateTime1;
                if (ts.Days >= 1)
                {
                    dateDiff = DateTime1.Month.ToString() + "月" + DateTime1.Day.ToString() + "日";
                }
                else
                {
                    if (ts.Hours > 1)
                    {
                        dateDiff = ts.Hours.ToString() + "小时前";
                    }
                    else
                    {
                        dateDiff = ts.Minutes.ToString() + "分钟前";
                    }
                }
            }
            catch
            { }
            return dateDiff;
        }
        #endregion

    }

}