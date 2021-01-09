namespace Jashmoor_NetCore_Extensions
{
    using System;

    public static class DateTimeExtensions
    {
        internal static readonly DateTime TIMESTART;
        static DateTimeExtensions() => TIMESTART = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Tests whether the given Date falls on a Weekend
        /// </summary>
        /// <param name="date">date to test</param>
        /// <returns>Boolean</returns>
        public static bool IsWeekend(this DateTime date)
            => date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday;

        internal static class DateTimeFormat
        {
            public static string SecondAgo => "{0} second ago";
            public static string SecondsAgo => "{0} seconds ago";
            public static string MinuteAgo => "{0} minute ago";
            public static string MinutesAgo => "{0} minutes ago";
            public static string HourAgo => "{0} hour ago";
            public static string HoursAgo => "{0} hours ago";
            public static string DayAgo => "{0} day ago";
            public static string DaysAgo => "{0} days ago";
            public static string MonthAgo => "{0} month ago";
            public static string MonthsAgo => "{0} months ago";
            public static string YearAgo => "{0} year ago";
            public static string YearsAgo => "{0} years ago";
        }

        /// <summary>
        /// Returns a pre-formatted string showing how long ago the given DateTime was
        /// </summary>
        /// <param name="date">date to test</param>
        /// <returns>String formatted to show how long ago a date occured</returns>
        public static string TimeAgo(this DateTime date)
        {
            TimeSpan span = DateTime.UtcNow - date;
            if (span.Days > 365)
            {
                int years = (span.Days / 365);
                return string.Format(years == 1 ? DateTimeFormat.YearAgo : DateTimeFormat.YearsAgo, years);
            }
            if (span.Days > 30)
            {
                int months = (span.Days / 30);
                return string.Format(months == 1 ? DateTimeFormat.MonthAgo : DateTimeFormat.MonthsAgo, months);
            }
            if (span.Days > 0)
                return string.Format(span.Days == 1 ? DateTimeFormat.DayAgo : DateTimeFormat.DaysAgo, span.Days);
            if (span.Hours > 0)
                return string.Format(span.Hours == 1 ? DateTimeFormat.HourAgo : DateTimeFormat.HoursAgo, span.Hours);
            if (span.Minutes > 0)
                return string.Format(span.Minutes == 1 ? DateTimeFormat.MinuteAgo : DateTimeFormat.MinutesAgo, span.Minutes);
            if (span.Seconds > 1 || span.Seconds == 0)
                return string.Format(DateTimeFormat.SecondsAgo, span.Seconds);
            if (span.Seconds == 1)
                return string.Format(DateTimeFormat.SecondAgo, span.Seconds);
            return "some time ago";
        }
    }
}
