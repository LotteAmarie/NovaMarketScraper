namespace NovaMarketScraper.Core.Data
{
    using System.Collections.Generic;
    public class ItemHistory
    {
        public Item Item { get; set; }

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

        public int WeeklyNumberSold { get; set; }
        public int WeeklyMin { get; set; }
        public int WeeklyMax { get; set; }
        public int WeeklyAverage { get; set; }
        public int WeeklyStdDeviation { get; set; }

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
        
        public int MonthlyNumberSold { get; set; }
        public int MonthlyMin { get; set; }
        public int MonthlyMax { get; set; }
        public int MonthlyAverage { get; set; }
        public int MonthlyStdDeviation { get; set; }

        public List<ItemListing> CurrentListings { get; set; }
    }
}