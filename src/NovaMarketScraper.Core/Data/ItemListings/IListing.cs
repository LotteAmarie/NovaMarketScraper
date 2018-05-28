namespace NovaMarketScraper.Core.Data
{
    public interface IListing
    {
        Item ItemOf { get; }
        int Price { get; }
        string Location { get; }
    }
}