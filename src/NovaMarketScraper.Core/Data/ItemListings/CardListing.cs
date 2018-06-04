using NovaMarketScraper.Core.Data;

namespace NovaMarketScraper.Core.Data
{
    public class CardListing : IListing
    {
        public CardListing(Item itemOf, Item itemIn, int price, int refine, string additionalProperties, string location)
        {
            this.ItemOf = itemOf;
            this.ItemIn = itemIn;
            this.Price = price;
            this.Refine = refine;
            this.AdditionalProperties = additionalProperties;
            this.Location = location;
        }

        public Item ItemOf { get; }

        public Item ItemIn { get; }
        public int Price { get; }
        public int Refine { get; }
        public string AdditionalProperties { get; }
        public string Location { get; }
    }
}