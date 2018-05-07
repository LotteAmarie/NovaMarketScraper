using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

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
            GenerateItems(driver);

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
            driver.Navigate().GoToUrl("https://www.novaragnarok.com/?module=vending");

            var btnSearch = driver.FindElement(By.XPath("//*[@id=\"mCSB_1_container\"]/div/div[3]/form/input[4]"));
            btnSearch.Click();

            return new List<Item>();
        }
    }
}
