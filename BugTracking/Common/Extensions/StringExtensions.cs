using Common.Util;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Test if string is contains only digits.
        /// </summary>
        /// <param name="str">String to test.</param>
        /// <returns>Returns <c>true</c>; otherwise <c>false</c>.</returns>
        public static bool ContainsDigitsOnly(this string str) => str.All(char.IsDigit);

        //
        // Summary:
        //     Returns a value indicating whether any of specified System.String objects occurs
        //     within this string.
        //
        // Parameters:
        //   values:
        //     The strings to seek.
        //
        // Returns:
        //     true if any of value parameters occurs within this string, or if value is the empty
        //     string (""); otherwise, false.
        public static bool ContainsAny(this string str, params string[] values) => str != null && values.Any(str.Contains);

        #region Format

        /// <summary>
        /// Formats a string with one literal placeholder.
        /// </summary>
        /// <param name="text">The extension text</param>
        /// <param name="arg0">Argument 0</param>
        /// <returns>The formatted string</returns>
        public static string FormatWith(this string text, object arg0) => string.Format(text, arg0);

        /// <summary>
        /// Formats a string with two literal placeholders.
        /// </summary>
        /// <param name="text">The extension text</param>
        /// <param name="arg0">Argument 0</param>
        /// <param name="arg1">Argument 1</param>
        /// <returns>The formatted string</returns>
        public static string FormatWith(this string text, object arg0, object arg1) => string.Format(text, arg0, arg1);

        /// <summary>
        /// Formats a string with tree literal placeholders.
        /// </summary>
        /// <param name="text">The extension text</param>
        /// <param name="arg0">Argument 0</param>
        /// <param name="arg1">Argument 1</param>
        /// <param name="arg2">Argument 2</param>
        /// <returns>The formatted string</returns>
        public static string FormatWith(this string text, object arg0, object arg1, object arg2) => string.Format(text, arg0, arg1, arg2);

        /// <summary>
        /// Formats a string with a list of literal placeholders.
        /// </summary>
        /// <param name="text">The extension text</param>
        /// <param name="args">The argument list</param>
        /// <returns>The formatted string</returns>
        public static string FormatWith(this string text, params object[] args) => string.Format(text, args);

        /// <summary>
        /// Formats a string with a list of literal placeholders.
        /// </summary>
        /// <param name="text">The extension text</param>
        /// <param name="provider">The format provider</param>
        /// <param name="args">The argument list</param>
        /// <returns>The formatted string</returns>
        public static string FormatWith(this string text, IFormatProvider provider, params object[] args) => string.Format(provider, text, args);

        public static string JoinValues(this string separator, params object[] values) => string.Join(separator, values);

        /// <summary>
        /// Wraps the specified string in double quotes.
        /// </summary>
        public static string DoubleQuote(this string str) => string.Format(CultureInfo.InvariantCulture, @"""{0}""", str);

        /// <summary>
        /// Wraps the specified string in single quotes.
        /// </summary>
        public static string SingleQuote(this string str) => string.Format(CultureInfo.InvariantCulture, "'{0}'", str);

        /// <summary>
        /// Retrieves a substring from this instance. If string length is less than argument return full string.
        /// </summary>
        public static string SafeSubstring(this string str, int length) => (str == null || str.Length <= length) ? str : str.Substring(0, length);

        public static string GetLast(this string source, int tail_length)
        {
            if (tail_length >= source.Length)
                return source;
            return source.Substring(source.Length - tail_length);
        }

        #endregion

        #region IsNullOrEmpty

        /// <summary>
        /// Indicates whether the specified System.String object is null or an System.String.Empty string.
        /// </summary>
        /// <param name="str">A System.String reference.</param>
        /// <returns>
        /// true if the value parameter is null or an empty string (""); otherwise, false.
        /// </returns>
        public static bool IsNullOrEmpty(this string str) => string.IsNullOrEmpty(str);

        public static bool IsEmpty(this string value) => string.IsNullOrEmpty(value);

        public static bool IsWhiteSpace(this string value) => string.IsNullOrWhiteSpace(value);

        public static bool IsNotEmpty(this string value) => !string.IsNullOrEmpty(value);

        public static bool IsNotWhiteSpace(this string value) => !string.IsNullOrWhiteSpace(value);

        #endregion

        #region ToEnum

        /// <summary>
        /// Parses a string into an Enum
        /// </summary>
        /// <typeparam name="T">The type of the Enum</typeparam>
        /// <param name="value">String value to parse</param>
        /// <returns>The Enum corresponding to the stringExtensions</returns>
        public static T ToEnum<T>(this string value) where T : struct => ToEnum<T>(value, false);

        /// <summary>
        /// Parses a string into an Enum
        /// </summary>
        /// <typeparam name="T">The type of the Enum</typeparam>
        /// <param name="value">String value to parse</param>
        /// <param name="ignorecase">Ignore the case of the string being parsed</param>
        /// <returns>The Enum corresponding to the stringExtensions</returns>
        public static T ToEnum<T>(this string value, bool ignorecase) where T : struct
        {
            ThrowIf.IsNull(value, "value");

            var t = typeof(T);
            if (!t.IsEnum)
                throw new ArgumentException("Type provided must be an Enum.", nameof(value));

            return (T)Enum.Parse(t, value, ignorecase);
        }

        public static T ParseEnum<T>(this string value, bool ignorecase = true) where T : struct
        {
            if (!typeof(T).IsEnum) return default(T);

            T response;
            return Enum.TryParse(value, ignorecase, out response) ? response : default(T);
        }

        public static T? ToEnumChecked<T>(this string value, bool ignorecase = true) where T : struct
        {
            if (value == null) return null;

            if (!typeof(T).IsEnum) return default(T);

            T response;
            return Enum.TryParse(value, ignorecase, out response) ? response : default(T);
        }

        #endregion

        #region ToNumeric

        public static byte ToByte(this string val) => byte.Parse(val);

        public static int ToInt32(this string val) => int.Parse(val);

        public static long ToInt64(this string val) => long.Parse(val);

        /// <summary>
        /// Converts string to <see cref="float"/> (<see cref="Single"/>) using "en-US" as default <see cref="CultureInfo"/>, but if another
        /// <seealso cref="CultureInfo"/> is specified, it will be used in conversion.
        /// </summary>
        /// <param name="val">value to convert</param>
        /// <param name="cultureInfo"><see cref="CultureInfo"/> used in conversion.</param>
        /// <returns>Returns converted value.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="OverflowException"></exception>
        public static float ToFloat(this string val, CultureInfo cultureInfo = null) => float.Parse(val, cultureInfo ?? CultureInfo.CreateSpecificCulture("en-US"));

        /// <summary>
        /// Converts string to <see cref="double"/> (<see cref="Double"/>) using "en-US" as default <see cref="CultureInfo"/>, but if another
        /// <seealso cref="CultureInfo"/> is specified, it will be used in conversion.
        /// </summary>
        /// <param name="val">value to convert</param>
        /// <param name="cultureInfo"><see cref="CultureInfo"/> used in conversion.</param>
        /// <returns>Returns converted value.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="OverflowException"></exception>
        public static double ToDouble(this string val, CultureInfo cultureInfo = null) => double.Parse(val, cultureInfo ?? CultureInfo.CreateSpecificCulture("en-US"));

        /// <summary>
        /// Converts string to <see cref="decimal"/> (<see cref="Decimal"/>) using "en-US" as default <see cref="CultureInfo"/>, but if another
        /// <seealso cref="CultureInfo"/> is specified, it will be used in conversion.
        /// </summary>
        /// <param name="val">value to convert</param>
        /// <param name="cultureInfo"><see cref="CultureInfo"/> used in conversion.</param>
        /// <returns>Returns converted value.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="OverflowException"></exception>
        public static decimal ToDecimal(this string val, CultureInfo cultureInfo = null) => decimal.Parse(val, cultureInfo ?? CultureInfo.CreateSpecificCulture("en-US"));

        #endregion

        /// <summary>
        /// Returns true if string is numeric and not empty or null or whitespace.
        /// Determines if string is numeric by parsing as Double
        /// </summary>
        /// <param name="str"></param>
        /// <param name="style">Optional style - defaults to NumberStyles.Number (leading and trailing whitespace, leading and trailing sign, decimal point and thousands separator) </param>
        /// <param name="culture">Optional CultureInfo - defaults to InvariantCulture</param>
        /// <returns><c>true</c> if string is numeric; otherwise <c>false</c></returns>
        public static bool IsNumeric(this string str, NumberStyles style = NumberStyles.Number,
            CultureInfo culture = null)
        {
            double num;
            if (culture == null) culture = CultureInfo.InvariantCulture;
            return double.TryParse(str, style, culture, out num) && !string.IsNullOrWhiteSpace(str);
        }

        public static string AddSuffixTimestamp(this string str, int limit = 0)
        {
            var timestamp = DateTime.Now.ToString("yyMMddHHmm");
            var newstr = $"{str.TrimEnd(' ', '_')}_{timestamp}";
            if (limit != 0 && newstr.Length > limit) return newstr.Substring(0, limit);
            return newstr;
        }
    }
}
