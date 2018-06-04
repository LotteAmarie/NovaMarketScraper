using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using NovaMarketScraper.Core.Utility;

namespace NovaMarketScraper.Core.Data.ItemListings
{
    public static class ListingFactory
    {
        public static IEnumerable<IListing> CreateListings(HtmlNode table, Item itemOf)
        {
            var listings = new List<IListing>();
            var listingCount = table.SelectNodes(".//tr").Count;

            if (table.SelectSingleNode(".//thead/tr/th[1]").InnerText.Equals("item", StringComparison.InvariantCultureIgnoreCase))
            {
                for (int i = 1; i < listingCount; i++)
                {
                    var items = new ItemList();

                    var itemInId = ParseItemIdFromImgHtml(table.SelectSingleNode($".//tr[{i}]/td[1]").OuterHtml);
                    Item itemIn;
                    var priceString = table.SelectSingleNode($".//tr[{i}]/td[2]").InnerText.GetDigits();
                    var refineString = table.SelectSingleNode($".//tr[{i}]/td[3]").InnerText.GetDigits();
                    var properties = table.SelectSingleNode($".//tr[{i}]/td[4]").InnerText.Trim();
                    var location = table.SelectSingleNode($".//tr[{i}]/td[5]").InnerText.Trim();

                    if (itemInId == -1) throw new InvalidOperationException(""); // TODO:
                    itemIn = items.FindItemById(itemInId);

                    if (int.TryParse(priceString, out int price) && int.TryParse(refineString, out int refine))
                        listings.Add(new CardListing(itemOf, itemIn, price, refine, properties, location));
                }
            }

            if (table.SelectSingleNode(".//thead/tr/th[2]").InnerText.Equals("refine", StringComparison.InvariantCultureIgnoreCase))
            {
                for (int i = 1; i < listingCount; i++)
                {
                    var priceString = table.SelectSingleNode($".//tr[{i}]/td[1]").InnerText.GetDigits();
                    var refineString = table.SelectSingleNode($".//tr[{i}]/td[2]").InnerText.GetDigits();
                    var properties = table.SelectSingleNode($".//tr[{i}]/td[3]").InnerText.Trim();
                    var location = table.SelectSingleNode($".//tr[{i}]/td[4]").InnerText.Trim();

                    if (int.TryParse(priceString, out int price) && int.TryParse(refineString, out int refine))
                        listings.Add(new EquipmentListing(itemOf, price, refine, properties, location));
                }
            }

            if (table.SelectSingleNode(".//thead/tr/th[2]").InnerText.Equals("qty", StringComparison.InvariantCultureIgnoreCase))
            {
                for (int i = 1; i < listingCount; i++)
                {
                    var priceString = table.SelectSingleNode($".//tr[{i}]/td[1]").InnerText.GetDigits();
                    var qtyString = table.SelectSingleNode($".//tr[{i}]/td[2]").InnerText.GetDigits();
                    var location = table.SelectSingleNode($".//tr[{i}]/td[3]").InnerText.Trim();

                    if (int.TryParse(priceString, out int price) && int.TryParse(qtyString, out int qty))
                        listings.Add(new ItemListing(itemOf, price, qty, location));
                }
            }

            return listings;
        }

        private static int ParseItemIdFromImgHtml(string link)
        {
            var ret = -1;
            var dataToolTipContent = link.Split(' ')[3];

            foreach (var item in link.Split(' '))
            {
                if (item.Contains("data-tooltip-content="))
                {
                    ret = Convert.ToInt32(item.Split('"', '"')[1].GetDigits());
                    break;
                }
            }

            return ret;
        } 
    }
}