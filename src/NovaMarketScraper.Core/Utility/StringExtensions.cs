namespace NovaMarketScraper.Core.Utility
{
    using System;
    using System.Linq;
    public static class StringExtensions
    {
        public static string GetDigits(this string value) =>
            new string(value.Where(char.IsDigit).ToArray());

        public static string RemoveWhitespace(this string input) =>
            new string(input
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray());
    }
}