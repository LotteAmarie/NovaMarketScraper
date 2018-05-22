namespace NovaMarketScraper.Core.Data
{
    public class ItemListing
    {
        public Item Item { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public int Refine { get; set; }
        public string AdditionalProperties { get; set; }
        public string Location { get; set; }
    }
}