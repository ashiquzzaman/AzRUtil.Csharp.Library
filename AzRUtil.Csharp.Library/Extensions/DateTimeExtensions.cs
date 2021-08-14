using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace AzRUtil.Csharp.Library.Extensions
{
    //ALL Time are UTC
    public static class DateTimeExtensions
    {
        public static DateTime ToLocalNow(string timeZoneId = "Bangladesh Standard Time")
        {
            return DateTime.UtcNow.ConvertLocalTime(timeZoneId);
        }

        public static DateTime ConvertLocalTime(this DateTime dateTime, string timeZoneId = "Bangladesh Standard Time")
        {
            //TimeZoneInfo z in TimeZoneInfo.GetSystemTimeZones();//z.Id
            //timeZoneId = AppIdentity.CurrentApp.TimeZoneId;
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            var result = TimeZoneInfo.ConvertTime(dateTime, timeZone);//dateTime.LocalTime();
            return result;
        }

        private static long lastTimeStamp = DateTime.UtcNow.Ticks;

        public static long UtcNowTicks
        {
            get
            {
                long original, newValue;
                do
                {
                    original = lastTimeStamp;
                    long now = DateTime.UtcNow.Ticks;
                    newValue = Math.Max(now, original + 1);
                } while (Interlocked.CompareExchange
                    (ref lastTimeStamp, newValue, original) != original);

                return newValue;
            }
        }

        #region LONG & STRING DATETIME


        public static DateTime ToUtcDateTime(this string value)
        {
            var time = new DateTime();
            var matchingCulture =
                CultureInfo.GetCultures(CultureTypes.AllCultures)
                    .FirstOrDefault(ci => DateTime.TryParse(value, ci, DateTimeStyles.None, out time));
            return time;
        }
        public static DateTime ToDateTimeFromUniversal(this string value)
        {

            var dateTime = new DateTime();
            var matchingCulture =
                CultureInfo.GetCultures(CultureTypes.AllCultures)
                    .FirstOrDefault(ci => DateTime.TryParse(value, ci, DateTimeStyles.None, out dateTime));
            var localDateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc).ConvertLocalTime();

            return localDateTime;
        }





        #endregion



        #region Julian

        public static string ToJulian(this DateTime dateTime)
        {
            var result = $"{dateTime.Year}{dateTime.DayOfYear}";
            return result;
        }
        public static string ToInvoiceDate(this DateTime dateTime)
        {
            var result = dateTime.ToString("yy") + dateTime.ToString("MM") + dateTime.ToString("dd");
            return result;
        }

        public static DateTime FromJulian(this int dayOfYear, int year)
        {
            var theDate = new DateTime(year, 1, 1).AddDays(dayOfYear - 1);

            return theDate;
        }

        #endregion

        public static int TimeDifference(this DateTime startTime)
        {
            TimeSpan span = DateTimeExtensions.ToLocalNow() - startTime;
            return (int)span.TotalMilliseconds;
        }

        public static string ToTimeStamp(this DateTime theDate)
        {
            return theDate.ToString("yyyyMMddHHmmssfff");
        }

        #region MONTH

        public static DateTime FirstDayOfMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }

        public static DateTime LastDayOfMonth(this DateTime dateTime)
        {
            var firstDayOfTheMonth = new DateTime(dateTime.Year, dateTime.Month, 1);
            return firstDayOfTheMonth.AddMonths(1).AddDays(-1);
        }
        public static int GetMonthlyWorkingDays(this DateTime current, List<DayOfWeek> holidays, List<DateTime> excludedDates)
        {
            var firstDate = current.FirstDayOfMonth();
            var lastDate = current.LastDayOfMonth();

            bool IsWorkingDay(int days)
            {
                var currentDate = firstDate.AddDays(days);
                var isNonWorkingDay = holidays.Contains(currentDate.DayOfWeek) || excludedDates.Exists(excludedDate => excludedDate.Date.Equals(currentDate.Date));
                return !isNonWorkingDay;
            }

            return Enumerable.Range(0, (lastDate - firstDate).Days).Count(IsWorkingDay);
        }

        #endregion

        #region WEEK

        public static DateTime StartOfWeek(this DateTime dateTime, DayOfWeek startOfWeek)
        {
            var diff = dateTime.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }

            return dateTime.AddDays(-1 * diff).Date;
        }

        public static DateTime LastDayOfWeek(this DateTime firstDayOfWeek, DateTime lastDayOfYear)
        {
            var lastDayOfWeek = firstDayOfWeek;
            switch (firstDayOfWeek.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    lastDayOfWeek = firstDayOfWeek.AddDays(6);
                    break;
                case DayOfWeek.Monday:
                    lastDayOfWeek = firstDayOfWeek.AddDays(5);
                    break;
                case DayOfWeek.Tuesday:
                    lastDayOfWeek = firstDayOfWeek.AddDays(4);
                    break;
                case DayOfWeek.Wednesday:
                    lastDayOfWeek = firstDayOfWeek.AddDays(3);
                    break;
                case DayOfWeek.Thursday:
                    lastDayOfWeek = firstDayOfWeek.AddDays(2);
                    break;
                case DayOfWeek.Friday:
                    lastDayOfWeek = firstDayOfWeek.AddDays(1);
                    break;
                case DayOfWeek.Saturday:
                    lastDayOfWeek = firstDayOfWeek;
                    break;
            }

            if (lastDayOfWeek > lastDayOfYear) { lastDayOfWeek = lastDayOfYear; }

            return lastDayOfWeek;
        }

        public static DateTime FirstDateOfWeek(this int weekNumber, int year)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            if (firstWeek <= 1)
            {
                weekNumber -= 1;
            }
            var result = firstThursday.AddDays(weekNumber * 7);
            return result.AddDays(-3);
        }

        #endregion

        public static int GetWorkingDays(this DateTime current, DateTime finishDateExclusive, List<DayOfWeek> holidays, List<DateTime> excludedDates)
        {
            bool IsWorkingDay(int days)
            {
                var currentDate = current.AddDays(days);
                var isNonWorkingDay = holidays.Contains(currentDate.DayOfWeek) || excludedDates.Exists(excludedDate => excludedDate.Date.Equals(currentDate.Date));
                return !isNonWorkingDay;
            }

            return Enumerable.Range(0, (finishDateExclusive - current).Days).Count(IsWorkingDay);
        }
    }
}
