namespace NovaMarketScraper.Core.Data
{
    public interface IListing
    {
        Item ItemOf { get; set; }
        int Price { get; set; }
        string Location { get; set; }
    }
}