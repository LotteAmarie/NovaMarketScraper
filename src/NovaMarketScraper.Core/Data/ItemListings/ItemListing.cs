namespace NovaMarketScraper.Core.Data
{
    public class ItemListing : IListing
    {
        public Item ItemOf { get; set; }

        public int Price { get; set; }
        public int Quantity { get; set; }
        public string Location { get; set; }
    }
}