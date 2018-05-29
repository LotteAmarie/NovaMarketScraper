using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using NovaMarketScraper.Core.Data;
using NovaMarketScraper.Core.WebScraping;
using NovaMarketScraper.Core.Utility;
using System.Text;
using System.Linq;
using NovaMarketScraper.Core.Data.ItemListings;
using NovaMarketScraper.Core;

namespace NovaMarketScraper.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var items = new ItemList();
            var itemsToLookUp = new List<Item>
            {
                items.FindItemById(4910),
                items.FindItemById(4913),
                items.FindItemById(4916),
                items.FindItemById(4919),
                items.FindItemById(4922),
                items.FindItemById(4925)
            };

            var watchList = new WatchList(itemsToLookUp);

            var results = watchList.CheckBelowMonthlyAverage(15);

            foreach (var result in results)
            {
                System.Console.WriteLine($"{result.listing.ItemOf.Name} found at {result.listing.Price:N0}, {result.percentage}% or more below the monthly average.");
            }

            Console.ReadLine();
        }

        private static void Benchmark()
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            var items = new ItemList();
            var itemsToLookUp = new List<Item>
            {
                items.FindItemById(4910),
                items.FindItemById(4913),
                items.FindItemById(4916),
                items.FindItemById(4919),
                items.FindItemById(4922),
                items.FindItemById(4925)
            };

            var reports = new ConcurrentBag<ItemReport>();
            Parallel.ForEach(itemsToLookUp, new ParallelOptions { MaxDegreeOfParallelism = 4 }, item =>
            {
                reports.Add(new ItemReport(item));
            });

            Console.WriteLine($"{itemsToLookUp.Count} item reports generated in {stopwatch.ElapsedMilliseconds} ms");

            stopwatch.Stop();

            Console.ReadLine();
        }
    }
}