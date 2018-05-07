using System;
using Newtonsoft.Json;

namespace NovaMarketScraper.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Item item = new Item 
            {
                Id = 210,
                Name = "Test Object"
            };

            string json = JsonConvert.SerializeObject(item, Formatting.Indented);

            System.Console.WriteLine(json);
        }
    }
}
