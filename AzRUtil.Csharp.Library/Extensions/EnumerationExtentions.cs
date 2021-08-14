using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AzRUtil.Csharp.Library.Enumerations;
using AzRUtil.Csharp.Library.Models;
using DayOfWeek = System.DayOfWeek;

namespace AzRUtil.Csharp.Library.Extensions
{
    public static class EnumerationExtentions
    {
        public static T As<T>(this Enum c) where T : struct
        {
            return (T)System.Enum.Parse(typeof(T), c.ToString(), false);
        }

        public static DateTime FirstDayOfMonth(this MonthOfYear month, int year)
        {
            return new DateTime(year, (int)month, 1);
        }

        public static DateTime LastDayOfMonth(this MonthOfYear month, int year)
        {
            var firstDayOfTheMonth = new DateTime(year, (int)month, 1);
            return firstDayOfTheMonth.AddMonths(1).AddDays(-1);
        }
        public static int DaysOfMonth(this MonthOfYear month, int year)
        {
            var firstDate = month.FirstDayOfMonth(year);
            var lastDate = month.LastDayOfMonth(year);

            return (lastDate - firstDate).Days;
        }
        public static int HolidaysOfMonth(this MonthOfYear month, int year, List<DayOfWeek> holidays, List<DateTime> excludedDates = null)
        {
            var firstDate = month.FirstDayOfMonth(year);
            var lastDate = month.LastDayOfMonth(year);

            bool IsWorkingDay(int days)
            {
                var currentDate = firstDate.AddDays(days);
                var isNonWorkingDay = holidays.Contains(currentDate.DayOfWeek) || (excludedDates != null && excludedDates.Exists(excludedDate => excludedDate.Date.Equals(currentDate.Date)));
                return isNonWorkingDay;
            }

            return Enumerable.Range(0, (lastDate - firstDate).Days).Count(IsWorkingDay);
        }
        public static int WorkingDaysOfMonth(this MonthOfYear month, int year, List<DayOfWeek> holidays, List<DateTime> excludedDates = null)
        {
            var firstDate = month.FirstDayOfMonth(year);
            var lastDate = month.LastDayOfMonth(year);

            bool IsWorkingDay(int days)
            {
                var currentDate = firstDate.AddDays(days);
                var isNonWorkingDay = holidays.Contains(currentDate.DayOfWeek) || (excludedDates != null && excludedDates.Exists(excludedDate => excludedDate.Date.Equals(currentDate.Date)));
                return !isNonWorkingDay;
            }

            return Enumerable.Range(0, (lastDate - firstDate).Days).Count(IsWorkingDay);
        }

        public static string GetEnumDescription<TEnum>(this TEnum value)
        {
            var fi = value.GetType().GetField(value.ToString());

            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
        }
        public static T ToEnum<T>(this string value)
        {
            if (value.Contains(" "))
            {
                value = value.Replace(" ", "_");
            }
            return (T)Enum.Parse(typeof(T), value, true);
        }
        public static string EnumToString(this Enum value)
        {
            string result = value.ToString();
            if (result.Contains("_"))
            {
                result = result.Replace("_", " ");
            }
            return result;
        }
        /// <summary>
        /// Gets the name in <see cref="DisplayAttribute"/> of the Enum.
        /// </summary>
        /// <param name="enumeration">A <see cref="Enum"/> that the method is extended to.</param>
        /// <returns>A name string in the <see cref="DisplayAttribute"/> of the Enum.</returns>

        public static string GetDisplayName(this Enum enumeration)
        {

            var enumType = Nullable.GetUnderlyingType(enumeration.GetType()) ?? enumeration.GetType();
            var enumName = Enum.GetName(enumType, enumeration);
            var displayName = enumName;
            try
            {
                var member = enumType.GetMember(enumName)[0];
                var attributes = member.GetCustomAttributes(typeof(DisplayAttribute), false);
                var attribute = (DisplayAttribute)attributes[0];
                displayName = attribute.Name;

                if (attribute.ResourceType != null)
                {
                    displayName = attribute.GetName();
                }
                return displayName;
            }
            catch
            {
                return displayName;
            }

        }

        public static List<DropDownItem> ToDropdown<TEnum>()
        {
            var type = typeof(TEnum);
            if (!type.IsEnum)
            {
                throw new ArgumentException(
                    @"type must be an enum",
                    nameof(type)
                );
            }

            var result = Enum
                    .GetValues(type)
                    .Cast<TEnum>()
                    .Select(v => new DropDownItem(
                            v.ToString(),
                            v.ToString()
                        )
                    )
                    .ToList();

            return result;
        }


    }
}
