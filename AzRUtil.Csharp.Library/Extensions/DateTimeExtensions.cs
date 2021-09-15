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

        #region STRING DATETIME


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
        public static string ToFriendlyDateTime(this DateTime value)
        {
            if (value <= DateTime.UtcNow) return "Just now";

            var dateTime = DateTime.ParseExact(value.ToString(), "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
            var localDateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc).ConvertLocalTime();
            var span = DateTime.UtcNow - localDateTime;

            if (span > TimeSpan.FromHours(24))
            {
                return dateTime.ToString("MMM dd");
            }
            if (span > TimeSpan.FromMinutes(60))
            {
                return $"{span.Hours}h";
            }
            return span > TimeSpan.FromSeconds(60) ? $"{span.Minutes}m ago" : "Just now";
        }





        #endregion

        #region CUSTOM LONG DATETIME

        #region DATE

        public static DateTime LongToDateTime(this long value)
        {
            var dateTime = DateTime.ParseExact(value.ToString(), "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
            var localDateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc).ConvertLocalTime();
            return localDateTime;
        }

        public static DateTime LongToDateTimeUtc(this long value)
        {
            var dateTime = DateTime.ParseExact(value.ToString(), "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
            return dateTime;
        }

        public static long ToLong(this DateTime dateTime)
        {
            return Convert.ToInt64(dateTime.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture));
        }

        public static long ToDateTimeLong(this string value)
        {

            var time = new DateTime();
            var matchingCulture =
                CultureInfo.GetCultures(CultureTypes.AllCultures)
                    .FirstOrDefault(ci => DateTime.TryParse(value, ci, DateTimeStyles.None, out time));
            var longDate = Convert.ToInt64(time.ToString("yyyyMMddHHmmss", matchingCulture));//issue 

            return longDate;
        }

        public static long ToDateLong(this string value)
        {

            var time = new DateTime();
            var matchingCulture =
                CultureInfo.GetCultures(CultureTypes.AllCultures)
                    .FirstOrDefault(ci => DateTime.TryParse(value, ci, DateTimeStyles.None, out time));
            var longDate = Convert.ToInt64(time.Date.ToString("yyyyMMddHHmmss", matchingCulture));//issue 

            return longDate;
        }

        public static string ToDateTimeString(this long value, string format = "MMM dd, yyyy h:mm tt")
        {
            if (value < 1)
            {
                return string.Empty;
            }
            var dateTimeStr = value.ToString().PadRight(14, '0');
            var dateTime =
                DateTime.ParseExact(dateTimeStr, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
            var localDateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc).ConvertLocalTime();
            var returnValue = localDateTime.ToString(format);
            return returnValue;
        }

        public static string ToDateString(this long value, string format = "MMM dd, yyyy ")
        {
            if (value < 1)
            {
                return string.Empty;
            }
            var dateTimeStr = value.ToString().PadRight(14, '0');
            var dateTime =
                DateTime.ParseExact(dateTimeStr, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
            var localDateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc).ConvertLocalTime();
            var returnValue = localDateTime.ToString(format);
            return returnValue;
        }

        public static string ToUtcDateTimeString(this long value, string format = "MMM dd, yyyy h:mm tt")
        {
            var dateTimeStr = value.ToString().PadRight(14, '0');
            var span =
                DateTime.ParseExact(dateTimeStr, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
            var returnValue = span.ToString(format);
            return returnValue;
        }

        public static string ToFriendlyDateTime(this long value)
        {
            if (value <= 0) return "Just now";

            var dateTime = DateTime.ParseExact(value.ToString(), "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
            var localDateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc).ConvertLocalTime();
            var span = DateTime.UtcNow - localDateTime;

            if (span > TimeSpan.FromHours(24))
            {
                return dateTime.ToString("MMM dd");
            }
            if (span > TimeSpan.FromMinutes(60))
            {
                return $"{span.Hours}h";
            }
            return span > TimeSpan.FromSeconds(60) ? $"{span.Minutes}m ago" : "Just now";
        }

        #endregion
        #region TIME

        public static string TimeTickToString(this long timeTick)
        {
            var dateTime = new DateTime(timeTick);
            var result = dateTime.ToString("hh:mm tt", CultureInfo.InvariantCulture);
            return result;
        }

        public static string ToTimeString(this int value, string format = "h:mm tt")
        {
            var intTime = value.ToString();
            var timeStr = intTime.Length <= 6 ? intTime.PadLeft(6, '0') : intTime.Substring(0, 6);//value.IntToTimeString()
            var dateTime =
                DateTime.ParseExact(timeStr, "HHmmss", CultureInfo.InvariantCulture);
            var localDateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc).ConvertLocalTime();
            return localDateTime.ToString(format);
        }

        public static string ToLocalTimeString(this string timeStr, string format = "h:mm tt")
        {
            timeStr = timeStr.Length <= 6 ? timeStr.PadLeft(6, '0') : timeStr.Substring(0, 6);//value.IntToTimeString()
            var dateTime =
                DateTime.ParseExact(timeStr, "HHmmss", CultureInfo.InvariantCulture);
            var localDateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc).ConvertLocalTime();
            return localDateTime.ToString(format);
        }

        public static DateTime ToLocalTime(this string timeStr)
        {
            timeStr = timeStr.PadLeft(6, '0');
            var dateTime =
                DateTime.ParseExact(timeStr, "HHmmss", CultureInfo.InvariantCulture);
            var localDateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc).ConvertLocalTime();
            return localDateTime;
        }

        public static DateTime ToLocalTime(this int time)
        {
            return time.ToString().ToLocalTime();
        }

        public static TimeSpan GetCurrentTimeStamp()
        {
            var currentTime = DateTimeExtensions.ToLocalNow().ToString("h:mm tt").ToTimeOfDayFrom12Time();
            return currentTime;
        }

        public static TimeSpan ToTimeOfDayFrom12Time(this string stringTime)
        {
            TimeSpan time = DateTime.Parse(stringTime, CultureInfo.InvariantCulture).TimeOfDay;
            return time;
        }

        public static int ToUtc24TimeInt(this string stringTime)//Create UTC Time  int from time string
        {
            var utcTime = DateTimeOffset.Parse(stringTime).UtcDateTime.ToString("HHmmss", CultureInfo.InvariantCulture);
            return Convert.ToInt32(utcTime);
        }


        public static int To24TimeInt(this string stringTime)//Create LOCAL Time  int from time string
        {
            var utcTime = DateTimeOffset.Parse(stringTime).DateTime.ToString("HHmmss", CultureInfo.InvariantCulture);
            return Convert.ToInt32(utcTime);
        }

        public static int ToIntTime(this DateTime dateTime)
        {
            return Convert.ToInt32(dateTime.ToString("HHmmss", CultureInfo.InvariantCulture));
        }

        #endregion
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
