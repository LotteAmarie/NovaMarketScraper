using NovaMarketScraper.Core.Data;

namespace NovaMarketScraper.Core.Utility
{
    internal static class ListingExtensions
    {
        public static bool IsBelowAverage(this IListing listing, int average, uint threshold)
        {
            var targetPrice = listing.DetermineTargetPrice(average, threshold);

            if (listing.Price <= targetPrice)
                return true;

            return false;
        }

        private static int DetermineTargetPrice(this IListing listing, int average, uint threshold)
        {
            int targetPrice = (int)(average * (1 - (threshold / 100)));

            if (listing is CardListing && listing.ItemOf.Id != ((CardListing)listing).ItemIn.Id)
            {
                targetPrice -= 2000000;
            }

            return targetPrice > 0 ? targetPrice : 0;
        }
    }
}