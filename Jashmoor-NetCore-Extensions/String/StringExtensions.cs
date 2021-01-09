namespace Jashmoor_NetCore_Extensions
{
    using System;
    using System.Net;
    public static class StringExtensions
    {
        /// <summary>
        /// Test whether the given string is neither Null or an empty string
        /// </summary>
        /// <param name="value">String to test</param>
        /// <returns>true/false</returns>
        public static bool HasValue(this string value) => !string.IsNullOrEmpty(value);

        /// <summary>
        /// Truncates the given string to a given length ( default = 30 ) and appends a suffix to show truncation
        /// ( default = "..." )
        /// </summary>
        /// <param name="value">string to truncate</param>
        /// <param name="length">Optional => the character length to end the string</param>
        /// <param name="suffix">Optional => the string suffix to show truncation</param>
        /// <returns>truncated string</returns>
        public static string Truncate(this string value, int length = 30, string suffix = "...")
        {
            if (value.HasValue() || value.Length <= length)
                return value;

            return $"{value.Substring(0, Math.Max(0, length))}{suffix}";
        }

        /// <summary>
        /// Uses the length of the given string to estimate the total average human reading time
        /// </summary>
        /// <param name="value">string to test</param>
        /// <returns>human readable string showing the estimated reading time</returns>
        public static string EstimatedReadingTime(this string value)
        {
            if (!(value.Length > 0))
                return "";

            double result = Math.Round((double)value.Length / 200, 2);
            int minutes = (int)result;
            double rightpart = result - minutes;

            double seconds = Math.Round(rightpart * 0.6, 2) * 100;
            string secondResult = seconds > 0
                ? $"{(int)seconds} seconds"
                : "";

            return $"{(minutes == 1 ? $"{minutes} minute" : $"{minutes} minutes")} {secondResult}";
        }

        /// <summary>
        /// Counts the words ( non white-space ) , in the given string
        /// </summary>
        /// <param name="value">string to test</param>
        /// <returns>Integer</returns>
        public static int TrueWordCount(this string value)
        {
            int wordcount = 0;
            if (value is string content)
            {
                int contentLength = content.Length;
                int index = 0;

                while (index < contentLength && char.IsWhiteSpace(content[index]))
                    index++;

                while (index < contentLength)
                {
                    while (index < contentLength && !char.IsWhiteSpace(content[index]))
                        index++;

                    wordcount++;

                    while (index < contentLength && char.IsWhiteSpace(content[index]))
                        index++;
                }
            }
            return wordcount;
        }

        /// <summary>
        /// Attempts to convert given string to a double
        /// </summary>
        /// <param name="value">string to parse</param>
        /// <returns>Nullable Double</returns>
        public static double? TryParseDouble(this string value) => double.TryParse(value, out var outVal) ? (double?)outVal : null;

        /// <summary>
        /// Attempts to convert given string to a int 32
        /// </summary>
        /// <param name="value">string to parse</param>
        /// <returns>Nullable Int 32</returns>
        public static int? TryParseInt32(this string value) => int.TryParse(value, out var outVal) ? (int?)outVal : null;

        /// <summary>
        /// Attempts to convert given string to a long
        /// </summary>
        /// <param name="value">string to parse</param>
        /// <returns>Nullable Long</returns>
        public static long? TryParseInt64(this string value) => long.TryParse(value, out var outVal) ? (long?)outVal : null;

        /// <summary>
        /// Attempts to convert given string to a decimal
        /// </summary>
        /// <param name="value">string to parse</param>
        /// <returns>Nullable Decimal</returns>
        public static decimal? TryParseDecimal(this string value) => decimal.TryParse(value, out var outVal) ? (decimal?)outVal : null;

        /// <summary>
        /// Attempts to convert given string to a boolean
        /// </summary>
        /// <param name="value">string to parse</param>
        /// <returns>Nullable Boolean</returns>
        public static bool? TryParseBoolean(this string value) => bool.TryParse(value, out var outVal) ? (bool?)outVal : null;

        /// <summary>
        /// Attempts to convert given string to a datetime
        /// </summary>
        /// <param name="value">string to parse</param>
        /// <returns>Nullable DateTime</returns>
        public static DateTime? TryParseDateTime(this string value) => DateTime.TryParse(value, out var outVal) ? (DateTime?)outVal : null;

        /// <summary>
        /// Attempts to convert given string to a guid
        /// </summary>
        /// <param name="value">string to parse</param>
        /// <returns>Nullable Guid</returns>
        public static Guid? TryParseGuid(this string value) => Guid.TryParse(value, out var outVal) ? (Guid?)outVal : null;

        /// <summary>
        /// HtmlEncodes the given string
        /// </summary>
        /// <param name="value">string to encode</param>
        /// <returns>HtmlEncoded string</returns>
        public static string HtmlEncode(this string value) => value.HasValue() ? WebUtility.HtmlEncode(value) : value;

        /// <summary>
        /// HtmlDecodes the given string
        /// </summary>
        /// <param name="value">string to decode</param>
        /// <returns>HtmlDecoded string</returns>
        public static string HtmlDecode(this string value) => value.HasValue() ? WebUtility.HtmlDecode(value) : value;

        /// <summary>
        /// UrlEncodes the given string
        /// </summary>
        /// <param name="value">string to encode</param>
        /// <returns>UrlEncoded string</returns>
        public static string UrlEncode(this string value) => value.HasValue() ? WebUtility.UrlEncode(value) : value;

        /// <summary>
        /// UrlDecodes the given string
        /// </summary>
        /// <param name="value">string to decode</param>
        /// <returns>UrlDecoded string</returns>
        public static string UrlDecode(this string value) => value.HasValue() ? WebUtility.UrlDecode(value) : value;

        /// <summary>
        /// Test whether the given string is an Relative Url
        /// </summary>
        /// <param name="value">string to test</param>
        /// <returns>Boolean</returns>
        public static bool IsRelativeUrl(this string value) => Uri.TryCreate(value, UriKind.Relative, out var _);

        /// <summary>
        /// Test whether the given string is an Absolute Url
        /// </summary>
        /// <param name="value">string to test</param>
        /// <returns>Boolean</returns>
        public static bool IsAbsoluteUrl(this string value) => Uri.TryCreate(value, UriKind.Absolute, out var _);
    }
}
