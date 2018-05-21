using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using Newtonsoft.Json;
using NovaMarketScraper.Core.Data;
using OpenQA.Selenium;
using PennedObjects.RateLimiting;

namespace NovaMarketScraper.Core.WebScraping
{
    public class SeleniumWebScraping
    {
        /// <summary>
        /// Uses Selenium to scrape a Json list of all items on Nova Ragnarok Online
        /// </summary>
        /// <param name="driver">A Selenium WebDriver</param>
        /// <returns>A Json string containing all items in Nova RO's items index</returns>
        public static string ScrapeAllItemsToJson(IWebDriver driver)
        {
            NovaLogin(driver);
            return GenerateJsonFromItemsHtml(GetItemsHtml(driver));
        }

        /// <summary>
        /// Given a collection HTML source from Nova Ragnarok's item index, generates a new Items JSON.
        /// </summary>
        /// <param name="htmlPages">HTML source from Nova Ragnarok's Items Index</param>
        /// <returns>A Json string containing all items in Nova RO's items index</returns>
        private static string GenerateJsonFromItemsHtml(IEnumerable<string> htmlPages)
        {
            var items = new List<Item>();
            foreach (var htmlPage in htmlPages)
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(htmlPage);

                for (int i = 2; i < 12; i++)
                {
                    var itemId = doc.DocumentNode.SelectSingleNode($"//div/table[2]/tbody/tr[{i}]/td[1]").InnerText.Trim();
                    var itemName = doc.DocumentNode.SelectSingleNode($"//div/table[2]/tbody/tr[{i}]/td[3]").InnerText.Trim();

                    System.Console.WriteLine($"{itemId}, {itemName}");

                    var item = new Item
                    {
                        Id = Convert.ToInt32(itemId),
                        Name = itemName
                    };

                    items.Add(item);
                }
            }

            return JsonConvert.SerializeObject(items, Formatting.Indented);
        }

        /// <summary>
        /// Uses Selenium to retrieve the HTML source of each page of Nova Ragnarok's Item Index
        /// </summary>
        /// <param name="driver">A Selenium WebDriver</param>
        /// <returns>HTML source of Nova RO's Item Index</returns>
        private static IEnumerable<string> GetItemsHtml(IWebDriver driver)
        {
            var htmlPages = new List<string>();
            using (var rateGate = new RateGate(5, TimeSpan.FromSeconds(2)))
            {
                for (int i = 1; i <= 1120; i++) // TODO: hard-coded value
                {
                    rateGate.WaitToProceed();
                    driver.Navigate().GoToUrl($"https://www.novaragnarok.com/?module=item&action=index&p={i}");

                    htmlPages.Add(driver.PageSource);
                }
            }

            return htmlPages;
        }

        /// <summary>
        /// Used to login the given Selenium WebDriver to https://www.novaragnarok.com/
        /// </summary>
        /// <param name="driver">A Selenium WebDriver</param>
        private static void NovaLogin(IWebDriver driver)
        {
            System.Console.Write("Enter Username: ");
            var username = Console.ReadLine();

            System.Console.Write("Enter Password: ");
            var password = Console.ReadLine();

            driver.Navigate().GoToUrl(@"https://www.novaragnarok.com/");

            var txtUsername = driver.FindElement(By.CssSelector("#login > form:nth-child(1) > table:nth-child(2) > tbody:nth-child(1) > tr:nth-child(1) > td:nth-child(1) > input:nth-child(1)"));
            var txtPassword = driver.FindElement(By.CssSelector("#login > form:nth-child(1) > table:nth-child(2) > tbody:nth-child(1) > tr:nth-child(3) > td:nth-child(1) > input:nth-child(1)"));
            var btnLogin = driver.FindElement(By.CssSelector("#btnlogin"));

            txtUsername.SendKeys(username);
            txtPassword.SendKeys(password);
            btnLogin.Click();
        }
    }
}