using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using NovaMarketScraper.Core.Data;
using NovaMarketScraper.Core.Utility;
using NovaMarketScraper.Core.WebScraping;
using PennedObjects.RateLimiting;

namespace NovaMarketScraper.Core
{
    public class WatchList
    {
        private IEnumerable<Item> _watched;

        public WatchList(IEnumerable<Item> watchedItems)
        {
            _watched = watchedItems;
        }
        public IEnumerable<(IListing listing, int percentage)> GetBelowWeeklyAverage(uint threshold)
        {
            if (threshold > 100) throw new ArgumentException("Threshold is a percentage value and must range between 0-100.");

            var reports = GenerateReports();
            var listings = new ConcurrentBag<(IListing listing, int percentage)>();
            foreach (var report in reports)
            {
                Parallel.ForEach(report.CurrentListings, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, listing => {
                    if (listing.IsBelowAverage(report.WeeklyAverage, threshold))
                        listings.Add((listing, PercentChange(listing.Price, report.WeeklyAverage)));
                });
            }

            return listings;
        }

        public IEnumerable<(IListing listing, int percentage)> GetBelowMonthlyAverage(uint threshold)
        {
            if (threshold > 100) throw new ArgumentException("Threshold is a percentage value and must range between 0-100.");

            var reports = GenerateReports();
            var listings = new ConcurrentBag<(IListing listing, int percentage)>();
            foreach (var report in reports)
            {
                Parallel.ForEach(report.CurrentListings, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, listing => {
                    if (listing.IsBelowAverage(report.MonthlyAverage, threshold))
                        listings.Add((listing, PercentChange(listing.Price, report.MonthlyAverage)));
                });
            }

            return listings;
        }


        private IEnumerable<ItemReport> GenerateReports()
        {
            var reports = new ConcurrentBag<ItemReport>();
            using (var rateGate = new RateGate(3, TimeSpan.FromSeconds(1)))
            {
                Parallel.ForEach(_watched, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, item => {
                    rateGate.WaitToProceed();
                    reports.Add(new ItemReport(item));
                });
            }

            return reports;
        }

        private int PercentChange(int listingPrice, int average) 
            => (int)(((double)(average - listingPrice) / (double)average) * 100.0);
    }
}