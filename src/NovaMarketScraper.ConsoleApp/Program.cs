using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HtmlAgilityPack;
using PennedObjects.RateLimiting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Newtonsoft.Json;
using NovaMarketScraper.Core.Data;

namespace NovaMarketScraper.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();
        }
        
        public static ItemReport CreateItemHistory(Item item)
        {
            // Fetch HTML
            var web = new HtmlWeb();
            var doc = web.Load(BuildMarketUrl(item));

            // Scrape weekly/monthly entries
            var weeklyEntries = new List<int>();
            var monthlyEntries = new List<int>();
            for (int i = 2; i < 7; i++)
            {
                var weeklyNode = doc.DocumentNode
                        .SelectSingleNode($"//div/span[2]/table[1]/tbody/tr[1]/td[{i}]");
                
                var monthlyNode = doc.DocumentNode
                        .SelectSingleNode($"//div/span[2]/table[1]/tbody/tr[2]/td[{i}]");

                if (int.TryParse(GetDigits(weeklyNode.InnerText), out int weeklyEntry))
                {
                    weeklyEntries.Add(weeklyEntry);
                }

                if (int.TryParse(GetDigits(monthlyNode.InnerText), out int monthlyEntry))
                {
                    monthlyEntries.Add(monthlyEntry);
                }
            }

            // TODO: Account for different current listing formats
            // Scrape Current Listings
            // var currentListings = new List<ItemListing>();
            // for (int i = 1; i <= 11; i++)
            // {
            //     var listingPrice = doc.DocumentNode
            //             .SelectSingleNode($"//div/span[2]/table[2]/tbody[1]/tr[{i}]/td[1]");
                
            //     var listingQty = doc.DocumentNode
            //             .SelectSingleNode($"//div/span[2]/table[2]/tbody[1]/tr[{i}]/td[2]");

            //     var listingLocation = doc.DocumentNode
            //             .SelectSingleNode($"//div/span[2]/table[2]/tbody[1]/tr[{i}]/td[2]");

            //     if (int.TryParse(GetDigits(listingPrice.InnerText), out int price) && 
            //         int.TryParse(GetDigits(listingQty.InnerText), out int qty))
            //     {
            //         var itemListing = new ItemListing()
            //         {
            //             Price = price,
            //             Quantity = qty,
            //             Location = listingLocation.InnerText
            //         };

            //         currentListings.Add(itemListing);
            //     }
            // }

            if(weeklyEntries.Count != monthlyEntries.Count) 
                throw new Exception("Parsing HTML returned an inequal amount of weekly and monthly price history entries.");

            var itemHistory = new ItemReport
            {
                Item = item,

                WeeklyNumberSold = weeklyEntries[0],
                WeeklyMin = weeklyEntries[1],
                WeeklyMax = weeklyEntries[2],
                WeeklyAverage = weeklyEntries[3],
                WeeklyStdDeviation = weeklyEntries[4],

                MonthlyNumberSold = monthlyEntries[0],
                MonthlyMin = monthlyEntries[1],
                MonthlyMax = monthlyEntries[2],
                MonthlyAverage = monthlyEntries[3],
                MonthlyStdDeviation = monthlyEntries[4],

                CurrentListings = null
            };

            return itemHistory;
        }

        public static string GetDigits(string input) =>
            new string(input.Where(char.IsDigit).ToArray());

        public static string BuildMarketUrl(Item item) =>
            $"https://www.novaragnarok.com/?module=vending&action=item&id={item.Id}";
    }
}