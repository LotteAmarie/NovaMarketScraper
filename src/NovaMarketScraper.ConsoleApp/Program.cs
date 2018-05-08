using System;
using System.Collections.Generic;
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
            var driver = new ChromeDriver(".");
            var username = Console.ReadLine();
            var password = Console.ReadLine();

            NovaLogin(driver, username, password);
            var items = GenerateItems(driver);

            string json = JsonConvert.SerializeObject(items, Formatting.Indented);
            System.Console.WriteLine(json);

            Console.ReadLine();
        }

        private static void NovaLogin(IWebDriver driver, string username, string password)
        {
            driver.Navigate().GoToUrl("https://www.novaragnarok.com/?module=vending");

            var txtUsername = driver.FindElement(By.Name("username"));
            txtUsername.SendKeys(username);

            var txtPassword = driver.FindElement(By.Name("password"));
            txtPassword.SendKeys(password);

            var btnLogin = driver.FindElement(By.XPath("//*[@id=\"btnlogin\"]"));
            btnLogin.Click();
        }

        private static List<Item> GenerateItems(IWebDriver driver)
        {
            var items = new List<Item>();
            for (int i = 1; i <= 3; i++)
            {
                driver.Navigate().GoToUrl($"https://www.novaragnarok.com/?module=item&action=index&name=&type=-1&p={i}");

                var table = driver.FindElement(By.XPath("/html/body/div[2]/div[2]/div[2]/div[1]/div/div/table[2]"));
                var rowCount = table.FindElements(By.TagName("tr")).Count;

                for (int j = 2; j < rowCount + 1; j++)
                {
                    var id = table.FindElement(By.XPath($"/html/body/div[2]/div[2]/div[2]/div[1]/div/div/table[2]/tbody/tr[{j}]/td[1]")).Text;
                    var name = table.FindElement(By.XPath($"/html/body/div[2]/div[2]/div[2]/div[1]/div/div/table[2]/tbody/tr[{j}]/td[3]/a")).Text;

                    var item = new Item
                    {
                        Id = Convert.ToInt32(id),
                        Name = name
                    };

                    System.Console.WriteLine($"{id} {name}");
                    items.Add(item);
                }
            }

            return items;
        }
    }
}