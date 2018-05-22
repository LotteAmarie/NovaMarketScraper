// using System;
// using System.Collections.Generic;
// using System.Linq;
// using HtmlAgilityPack;
// using NovaMarketScraper.Core.Data;

// namespace NovaMarketScraper.Core.WebScraping
// {
//     public class MarketScraper
//     {
//         public static ItemReport CreateItemReport(Item item)
//         {
//             // TODO: Account for different current listing formats
//             // Scrape Current Listings
//             // var currentListings = new List<ItemListing>();
//             // for (int i = 1; i <= 11; i++)
//             // {
//             //     var listingPrice = doc.DocumentNode
//             //             .SelectSingleNode($"//div/span[2]/table[2]/tbody[1]/tr[{i}]/td[1]");
                
//             //     var listingQty = doc.DocumentNode
//             //             .SelectSingleNode($"//div/span[2]/table[2]/tbody[1]/tr[{i}]/td[2]");

//             //     var listingLocation = doc.DocumentNode
//             //             .SelectSingleNode($"//div/span[2]/table[2]/tbody[1]/tr[{i}]/td[2]");

//             //     if (int.TryParse(GetDigits(listingPrice.InnerText), out int price) && 
//             //         int.TryParse(GetDigits(listingQty.InnerText), out int qty))
//             //     {
//             //         var itemListing = new ItemListing()
//             //         {
//             //             Price = price,
//             //             Quantity = qty,
//             //             Location = listingLocation.InnerText
//             //         };

//             //         currentListings.Add(itemListing);
//             //     }
//             // }
//         }
//     }
// }