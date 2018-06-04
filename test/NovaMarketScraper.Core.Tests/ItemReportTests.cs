namespace NovaMarketScraper.Core.Tests
{
    using Xunit;
    using HtmlAgilityPack;
    using System.IO;
    using System.Diagnostics;
    using System;
    using NovaMarketScraper.Core.WebScraping;
    using NovaMarketScraper.Core.Data;
    using System.Collections.Generic;
    using System.Linq;

    public class ItemReportTests
    {
        private HtmlDocument _doc;

        public ItemReportTests()
        {
            _doc = new HtmlDocument();
        }

        [Fact]
        public void Item_ScrapesCorrectly()
        {
            //Given
            _doc.Load("./Data/EoEStr3.html");
            var report = new ItemReport(_doc);
            var expected = new Item
            {
                Name = "Essence Of Evil STR 3",
                Id = 4910,
                Slots = 0
            };

            //When
            var actual = report.Item;

            //Then
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Slots, actual.Slots);
        }

#region WeeklyStatistics

        [Fact]
        public void WeeklyNumberSold_ScrapesCorrectly()
        {
            _doc.Load("./Data/oridecon.html");
            //Given
            var report = new ItemReport(_doc);
            var expected = 27122;

            //When
            var actual = report.WeeklyNumberSold;

            //Then
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WeeklyMin_ScrapesCorrectly()
        {
            //Given
            _doc.Load("./Data/oridecon.html");
            var report = new ItemReport(_doc);
            var expected = 18400;

            //When
            var actual = report.WeeklyMin;

            //Then
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WeeklyMax_ScrapesCorrectly()
        {
            //Given
            _doc.Load("./Data/oridecon.html");
            var report = new ItemReport(_doc);
            var expected = 25000;

            //When
            var actual = report.WeeklyMax;

            //Then
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WeeklyAverage_ScrapesCorrectly()
        {
            //Given
            _doc.Load("./Data/oridecon.html");
            var report = new ItemReport(_doc);
            var expected = 21308;

            //When
            var actual = report.WeeklyAverage;

            //Then
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void WeeklyStdDeviation_ScrapesCorrectly()
        {
            //Given
            _doc.Load("./Data/oridecon.html");
            var report = new ItemReport(_doc);
            var expected = 1830;

            //When
            var actual = report.WeeklyStdDeviation;

            //Then
            Assert.Equal(expected, actual);
        }

#endregion

#region Montly Statistics

        [Fact]
        public void MonthlyNumberSold_ScrapesCorrectly()
        {
            _doc.Load("./Data/oridecon.html");
            //Given
            var report = new ItemReport(_doc);
            var expected = 101210;

            //When
            var actual = report.MonthlyNumberSold;

            //Then
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MonthlyMin_ScrapesCorrectly()
        {
            //Given
            _doc.Load("./Data/oridecon.html");
            var report = new ItemReport(_doc);
            var expected = 18500;

            //When
            var actual = report.MonthlyMin;

            //Then
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MonthlyMax_ScrapesCorrectly()
        {
            //Given
            _doc.Load("./Data/oridecon.html");
            var report = new ItemReport(_doc);
            var expected = 25000;

            //When
            var actual = report.MonthlyMax;

            //Then
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MonthlyAverage_ScrapesCorrectly()
        {
            //Given
            _doc.Load("./Data/oridecon.html");
            var report = new ItemReport(_doc);
            var expected = 21629;

            //When
            var actual = report.MonthlyAverage;

            //Then
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MonthlyStdDeviation_ScrapesCorrectly()
        {
            //Given
            _doc.Load("./Data/oridecon.html");
            var report = new ItemReport(_doc);
            var expected = 1570;

            //When
            var actual = report.MonthlyStdDeviation;

            //Then
            Assert.Equal(expected, actual);
        }

#endregion

        [Fact]
        public void ItemListings_ScrapedCorrectly()
        {
            //Given
            _doc.Load("./Data/oridecon.html");
            var report = new ItemReport(_doc);

            var itemOf = new Item
            {
                Name = "Oridecon",
                Id = 984,
                Slots = 0
            };

            var expected = new List<ItemListing>
            {
                new ItemListing(itemOf, 19000, 43,  "newvending,103,219"),
                new ItemListing(itemOf, 19999, 203, "newvending,62,145"),
                new ItemListing(itemOf, 20000, 28,  "newvending,86,10"),
                new ItemListing(itemOf, 20000, 40,  "newvending,74,132"),
                new ItemListing(itemOf, 20000, 200, "newvending,62,186"),
                new ItemListing(itemOf, 20000, 274, "newvending,127,100"),
                new ItemListing(itemOf, 20000, 300, "newvending,107,148"),
                new ItemListing(itemOf, 20000, 305, "pay_arche,64,138"),
                new ItemListing(itemOf, 20000, 400, "newvending,135,213"),
                new ItemListing(itemOf, 21000, 400, "newvending,70,60"),
                new ItemListing(itemOf, 21450, 148, "gonryun,146,111")
            };

            //When
            var actual = report.CurrentListings.ToList();

            //Then
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].ItemOf.Id, actual[i].ItemOf.Id);
                Assert.Equal(expected[i].Location, actual[i].Location);
                Assert.Equal(expected[i].Price, actual[i].Price);
                Assert.Equal(expected[i].Quantity, (actual[i] as ItemListing).Quantity);
            }
        }

        [Fact]
        public void CardListings_ScrapedCorrectly()
        {
            //Given
            _doc.Load("./Data/EoEStr3.html");
            var report = new ItemReport(_doc);

            var itemOf = new Item
            {
                Name = "Essence Of Evil STR 3",
                Id = 4910,
                Slots = 0
            };

            var expected = new List<CardListing>
            {
                new CardListing(itemOf, itemOf, 7900000, 0, "None", "newvending,66,154"),
                new CardListing(itemOf, itemOf, 8000000, 0, "None", "aldebaran,124,121"),
                new CardListing(itemOf, itemOf, 8000000, 0, "None", "newvending,86,41"),
                new CardListing(itemOf, itemOf, 8000000, 0, "None", "niflheim,182,177"),
                new CardListing(itemOf, itemOf, 8000000, 0, "None", "gonryun,146,113"),
                new CardListing(itemOf, itemOf, 8000000, 0, "None", "splendide,211,127"),
                new CardListing(itemOf, itemOf, 10000000, 0, "None", "newvending,66,145"),
                new CardListing(itemOf, new Item { Name = "Rideword Hat [1]", Id = 5208, Slots = 0 }, 10000000, 4, "Essence Of Evil STR 3", "newvending,90,126"),
                new CardListing(itemOf, new Item { Name = "Rideword Hat [1]", Id = 5208, Slots = 0 }, 12000000, 4, "Essence Of Evil STR 3", "newvending,62,207"),
                new CardListing(itemOf, new Item { Name = "Rideword Hat [1]", Id = 5208, Slots = 0 }, 14000000, 4, "Essence Of Evil STR 3", "newvending,103,66"),
                new CardListing(itemOf, new Item { Name = "Ship Captain's Hat [1]", Id = 5359, Slots = 0 }, 28500000, 0,"Essence Of Evil STR 3", "newvending,111,60")
            };

            //When
            var actual = report.CurrentListings.ToList();

            //Then
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].ItemOf.Id, actual[i].ItemOf.Id);
                Assert.Equal(expected[i].Location, actual[i].Location);
                Assert.Equal(expected[i].Price, actual[i].Price);

                Assert.Equal(expected[i].ItemIn.Id, (actual[i] as CardListing).ItemIn.Id);
                Assert.Equal(expected[i].Refine, (actual[i] as CardListing).Refine);
                Assert.Equal(expected[i].AdditionalProperties, (actual[i] as CardListing).AdditionalProperties);
            }
        }
    }
}