using System.Linq;
using System.Globalization;

namespace Payment.Business
{
    public static class Converter
    {
        public static double ParseToDouble(this string inputString)
        {
            var ci = CultureInfo.InvariantCulture;
            var NumFormatInfo = (NumberFormatInfo)ci.NumberFormat.Clone();
            NumFormatInfo.NumberDecimalSeparator = ".";

            return double.Parse(Replace(inputString, NumFormatInfo), NumFormatInfo);
        }

        public static decimal ParseToDecimal(this string inputString)
        {
            var ci = CultureInfo.InvariantCulture;
            var NumFormatInfo = (NumberFormatInfo)ci.NumberFormat.Clone();
            NumFormatInfo.NumberDecimalSeparator = ".";

            return decimal.Parse(Replace(inputString, NumFormatInfo), NumFormatInfo);
        }

        private static string Replace(string inputString, NumberFormatInfo numFormatInfo)
        {
            foreach (var separator in separators.Where(inputString.Contains))
            {
                return inputString.Replace(separator, numFormatInfo.NumberDecimalSeparator);
            }
            return inputString;
        }

        private static readonly string[] separators = new [] { ".", "," };

    }
}
