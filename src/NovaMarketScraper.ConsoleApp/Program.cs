using System;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace NovaMarketScraper.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var webDriver = new ChromeDriver();

            webDriver.Navigate().GoToUrl("https://www.novaragnarok.com/?module=vending");

            Console.ReadLine();
        }
    }
}
