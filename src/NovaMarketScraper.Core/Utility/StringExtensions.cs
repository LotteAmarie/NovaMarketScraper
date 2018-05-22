namespace NovaMarketScraper.Core.Utility
{
    using System.Linq;
    public static class StringExtensions
    {
        public static string GetDigits(this string value) =>
            new string(value.Where(char.IsDigit).ToArray());
    }
}