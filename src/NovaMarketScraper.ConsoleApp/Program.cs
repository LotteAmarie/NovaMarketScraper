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
            var driver = new ChromeDriver(".");

            Console.ReadLine();
        }

        static void NovaLogin(IWebDriver driver, string username, string password)
        {
            driver.Navigate().GoToUrl("https://www.novaragnarok.com/?module=vending");

            var txtUsername = driver.FindElement(By.Name("username"));
            txtUsername.SendKeys(username);

            var txtPassword = driver.FindElement(By.Name("password"));
            txtPassword.SendKeys(password);
        }
    }
}
