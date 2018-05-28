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

namespace NovaMarketScraper.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Benchmark();
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