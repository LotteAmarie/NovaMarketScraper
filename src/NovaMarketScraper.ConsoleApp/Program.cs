using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using NovaMarketScraper.Core.Data;
using NovaMarketScraper.Core.WebScraping;

namespace NovaMarketScraper.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
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
            Parallel.ForEach(itemsToLookUp, new ParallelOptions { MaxDegreeOfParallelism = 4 }, item => {
                reports.Add(new ItemReport(item));
            });

            Console.WriteLine($"{itemsToLookUp.Count} item reports generated in {stopwatch.ElapsedMilliseconds} ms");

            stopwatch.Stop();

            Console.ReadLine();
        }
    }
}