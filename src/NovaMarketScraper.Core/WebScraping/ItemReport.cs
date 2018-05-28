namespace NovaMarketScraper.Core.WebScraping
{
    using System;
    using System.Collections.Generic;
    using HtmlAgilityPack;
    using NovaMarketScraper.Core.Data;
    using NovaMarketScraper.Core.Data.ItemListings;
    using NovaMarketScraper.Core.Utility;
    public class ItemReport
    {
        private HtmlDocument _doc;

        public ItemReport(Item item)
        {
            Item = item;
            var web = new HtmlWeb();
            _doc = web.Load($"https://www.novaragnarok.com/?module=vending&action=item&id={item.Id}");
        }

        public Item Item { get; private set; }

        public IEnumerable<int> WeeklyStatistics
        {
            get
            {
                var statistics = new List<int>();

                statistics.Add(WeeklyNumberSold);
                statistics.Add(WeeklyMin);
                statistics.Add(WeeklyMax);
                statistics.Add(WeeklyAverage);
                statistics.Add(WeeklyStdDeviation);

                return statistics;
            }
        }

        public int WeeklyNumberSold 
        { 
            get 
            {
                var valueNode = _doc.DocumentNode
                    .SelectSingleNode("//div/span[2]/table[1]/tbody/tr[1]/td[2]").InnerText;

                if (int.TryParse(valueNode.GetDigits(), out int value))
                    return value;

                return -1;
            }
        }
        public int WeeklyMin 
        { 
            get 
            {
                var valueNode = _doc.DocumentNode
                    .SelectSingleNode("//div/span[2]/table[1]/tbody/tr[1]/td[3]").InnerText;

                if (int.TryParse(valueNode.GetDigits(), out int value))
                    return value;

                return -1;
            }
        }
        public int WeeklyMax 
        { 
            get 
            {
                var valueNode = _doc.DocumentNode
                    .SelectSingleNode("//div/span[2]/table[1]/tbody/tr[1]/td[4]").InnerText;

                if (int.TryParse(valueNode.GetDigits(), out int value))
                    return value;

                return -1;
            }
        }
        public int WeeklyAverage 
        {
            get 
            {
                var valueNode = _doc.DocumentNode
                    .SelectSingleNode("//div/span[2]/table[1]/tbody/tr[1]/td[5]").InnerText;

                if (int.TryParse(valueNode.GetDigits(), out int value))
                    return value;

                return -1;
            }
        }
        public int WeeklyStdDeviation
        {
            get 
            {
                var valueNode = _doc.DocumentNode
                    .SelectSingleNode("//div/span[2]/table[1]/tbody/tr[1]/td[6]").InnerText;

                if (int.TryParse(valueNode.GetDigits(), out int value))
                    return value;

                return -1;
            }
        }

        public IEnumerable<int> MonthlyStatistics
        {
            get
            {
                var statistics = new List<int>();

                statistics.Add(MonthlyNumberSold);
                statistics.Add(MonthlyMin);
                statistics.Add(MonthlyMax);
                statistics.Add(MonthlyAverage);
                statistics.Add(MonthlyStdDeviation);

                return statistics;
            }
        }

        public int MonthlyNumberSold 
        { 
            get 
            {
                var valueNode = _doc.DocumentNode
                    .SelectSingleNode("//div/span[2]/table[1]/tbody/tr[2]/td[2]").InnerText;

                if (int.TryParse(valueNode.GetDigits(), out int value))
                    return value;

                return -1;
            }
        }
        public int MonthlyMin 
        { 
            get 
            {
                var valueNode = _doc.DocumentNode
                    .SelectSingleNode("//div/span[2]/table[1]/tbody/tr[2]/td[3]").InnerText;

                if (int.TryParse(valueNode.GetDigits(), out int value))
                    return value;

                return -1;
            }
        }
        public int MonthlyMax 
        { 
            get 
            {
                var valueNode = _doc.DocumentNode
                    .SelectSingleNode("//div/span[2]/table[1]/tbody/tr[2]/td[4]").InnerText;

                if (int.TryParse(valueNode.GetDigits(), out int value))
                    return value;

                return -1;
            }
        }
        public int MonthlyAverage 
        {
            get 
            {
                var valueNode = _doc.DocumentNode
                    .SelectSingleNode("//div/span[2]/table[1]/tbody/tr[2]/td[5]").InnerText;

                if (int.TryParse(valueNode.GetDigits(), out int value))
                    return value;

                return -1;
            }
        }
        public int MonthlyStdDeviation
        {
            get 
            {
                var valueNode = _doc.DocumentNode
                    .SelectSingleNode("//div/span[2]/table[1]/tbody/tr[2]/td[6]").InnerText;

                if (int.TryParse(valueNode.GetDigits(), out int value))
                    return value;

                return -1;
            }
        }

        public IEnumerable<IListing> CurrentListings
        {
            get
            {
                return ListingFactory.CreateListings(_doc.DocumentNode.SelectSingleNode("//div/span[2]/table[2]"), this.Item);
            }
        }
    }
}