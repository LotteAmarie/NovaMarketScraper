using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.PhantomJS;

namespace NovaMarketScraper.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();
        }

        public static IEnumerable<Item> LoadItems()
        {
            var itemStrings = File.ReadLines(@"items.txt");

            var items = new List<Item>();
            foreach (var itemString in itemStrings)
            {
                var strings = itemString.Split(',');

                var item = new Item
                {
                    Id = Convert.ToInt32(strings[0]),
                    Name = strings[1],
                    Slots = Convert.ToInt32(strings[2])
                };

                items.Add(item);
            }

            return items;
        }
    }
}