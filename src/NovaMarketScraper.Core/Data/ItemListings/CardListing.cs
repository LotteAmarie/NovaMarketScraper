namespace NovaMarketScraper.Core.Data
{
    public class CardListing
    {
        public Item ItemOf { get; set; }

        public Item ItemIn { get; set; }
        public int Price { get; set; }
        public int Refine { get; set; }
        public string AdditionalProperties { get; set; }
        public string Location { get; set; }
    }
}