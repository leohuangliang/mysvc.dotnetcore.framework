using System;
using System.Globalization;

namespace MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Helpers
{
    /// <summary>
    /// 日期相关的辅助类，通过常用的方法
    /// </summary>
    public static class DateTimeHelper
    {
        private static readonly string YMD_FORMAT = "yyyy-MM-dd";

        private static readonly string YMDHM_FORMAT = "yyyy-MM-dd HH:mm";

        private static readonly string YMDHMS_FORMAT = "yyyy-MM-dd HH:mm:ss";


        /// <summary>
        ///     计算两个日期之间的工作日，有可能会手工指定节假日
        ///     - weekends (Saturdays and Sundays)
        ///     - bank holidays in the middle of the week
        /// </summary>
        /// <param name="firstDay">First day in the time interval</param>
        /// <param name="lastDay">Last day in the time interval</param>
        /// <param name="bankHolidays">List of bank holidays excluding weekends</param>
        /// <returns>Number of business days during the 'span'</returns>
        public static int CountWorkingDay(DateTime firstDay, DateTime lastDay, params DateTime[] bankHolidays)
        {
            firstDay = firstDay.Date;
            lastDay = lastDay.Date;
            if (firstDay > lastDay)
                throw new Exception("Incorrect last day " + lastDay);

            TimeSpan span = lastDay - firstDay;
            int businessDays = span.Days + 1;
            int fullWeekCount = businessDays/7;
            // find out if there are weekends during the time exceedng the full weeks
            if (businessDays > fullWeekCount*7)
            {
                // we are here to find out if there is a 1-day or 2-days weekend
                // in the time interval remaining after subtracting the complete weeks
                var firstDayOfWeek = (int) firstDay.DayOfWeek;
                var lastDayOfWeek = (int) lastDay.DayOfWeek;
                if (lastDayOfWeek < firstDayOfWeek)
                {
                    lastDayOfWeek += 7;
                }
                if (firstDayOfWeek <= 6)
                {
                    if (lastDayOfWeek >= 7) // Both Saturday and Sunday are in the remaining time interval
                    {
                        businessDays -= 2;
                    }
                    else if (lastDayOfWeek >= 6) // Only Saturday is in the remaining time interval
                    {
                        businessDays -= 1;
                    }
                }
                else if (firstDayOfWeek <= 7 && lastDayOfWeek >= 7) // Only Sunday is in the remaining time interval
                {
                    businessDays -= 1;
                }
            }

            // subtract the weekends during the full weeks in the interval
            businessDays -= fullWeekCount + fullWeekCount;

            // subtract the number of bank holidays during the time interval
            foreach (DateTime bankHoliday in bankHolidays)
            {
                DateTime bh = bankHoliday.Date;
                if (firstDay <= bh && bh <= lastDay)
                {
                    --businessDays;
                }
            }

            return businessDays;
        }

        /// <summary>
        /// 根据工作日时效倒推统计的工作日日期
        /// </summary>
        /// <param name="statDate">统计时间</param>
        /// <param name="timeLiness">时效天数</param>
        /// <returns></returns>
        public static DateTime GetRealDay(DateTime statDate, int timeLiness)
        {
            int weeks = timeLiness/5;
            int days = timeLiness%5;
            int weekdiff = 7*weeks;
            int daydiff = days;

            DateTime fakedate = statDate.Date.AddDays(-weekdiff);
            for (int i = 0; i < daydiff; i++)
            {
                fakedate = fakedate.AddDays(-1);
                if (fakedate.DayOfWeek == DayOfWeek.Saturday || fakedate.DayOfWeek == DayOfWeek.Sunday)
                {
                    daydiff++;
                }
            }

            DateTime realdate = fakedate;
            return realdate;
        }

        public static DateTime GetDayEnd(DateTime dateTime)
        {
            return GetDayStart(dateTime).AddDays(1).AddTicks(-1);
        }

        public static DateTime GetDayEnd(String dateStr)
        {
            return GetDayStart(dateStr).AddDays(1).AddTicks(-1);
        }

        public static DateTime GetDayStart(DateTime dateTime)
        {
            return dateTime.Date;
        }

        public static DateTime GetDayStart(String dateStr)
        {
            DateTime result = DateTime.Today;
            DateTime.TryParse(dateStr, out result);
            return result.Date;
        }

        public static double CountDay(DateTime firstDay, DateTime lastDay)
        {
            firstDay = firstDay.Date;
            lastDay = lastDay.Date;
            if (firstDay > lastDay)
            {
                throw new Exception("Incorrect last day " + lastDay);
            }

            TimeSpan span = lastDay - firstDay;

            return span.TotalDays;
        }

        public static String ToYMD(this DateTime date)
        {
            return String.Format(YMD_FORMAT, date);
        }

        public static String ToYMDHM(this DateTime date)
        {
            return String.Format(YMDHM_FORMAT, date);
        }

        public static String ToYMDHMS(this DateTime date)
        {
            return String.Format(YMDHMS_FORMAT, date);
        }

        public static string PrintShortDate(DateTime date)
        {
            if (date == new DateTime(1900, 1, 1))
            {
                return string.Empty;
            }

            return date.ToShortDateString();
        }

        public static DateTime GetFirstDayOfWeek(this DateTime date)
        {
            int difference = (int) date.DayOfWeek - (int) CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
            if (difference < 0)
            {
                difference = 7 + difference;
            }

            DateTime firstDayOfWeek = date.AddDays(-difference);

            return firstDayOfWeek;
        }
    }
}