using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using HtmlAgilityPack;

namespace NovaMarketScraper.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var items = LoadItems();

            string itemName = Console.ReadLine();

            var item = items.FirstOrDefault(x => x.Name.ToLower() == itemName.ToLower());

            using (WebClient client = new WebClient())
            {
                string html = client.DownloadString(BuildMarketUrl(item));

                var doc = new HtmlDocument();
                doc.LoadHtml(html);

                System.Console.WriteLine(html);

                // var query = from table in doc.DocumentNode.SelectNodes("//table").Cast<HtmlNode>()
                //             from row in table.SelectNodes("tr").Cast<HtmlNode>()
                //             from cell in row.SelectNodes("th|td").Cast<HtmlNode>()
                //             select new { Table = table.Id, CellText = cell.InnerHtml };
                
                // foreach (var cell in query)
                // {
                //     Console.WriteLine("{0}: {1}", cell.Table, cell.CellText);
                // }
            }

            Console.ReadLine();
        }

        public static string BuildMarketUrl(Item item) =>
            $"https://www.novaragnarok.com/?module=vending&action=item&id={item.Id}";

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