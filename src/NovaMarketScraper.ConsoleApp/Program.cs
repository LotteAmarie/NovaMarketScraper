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
using System.IO;

namespace NovaMarketScraper.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var items = new ItemList();
            var itemsToLookUp = new List<Item>
            {
                items.FindItemById(6226),
                items.FindItemById(12432),
                items.FindItemById(607),
                items.FindItemById(12433),
                items.FindItemById(6672),
                items.FindItemById(6962),
                items.FindItemById(6607),
                items.FindItemById(6380),
                items.FindItemById(984),
                items.FindItemById(7620),
                items.FindItemById(7619),
                items.FindItemById(6755),
                items.FindItemById(6608),
                items.FindItemById(12103),
                items.FindItemById(671),
                items.FindItemById(6671)
            };

            // var watchList = new WatchList(itemsToLookUp);

            // var results = watchList.GetBelowWeeklyAverage(15);

            // foreach (var result in results)
            // {
            //     System.Console.WriteLine($"{result.listing.ItemOf.Name} found at {result.listing.Price:N0}, {result.percentage}% or more below the monthly average.");
            // }

            Console.ReadLine();
        }
    }
}