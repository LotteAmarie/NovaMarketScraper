namespace NovaMarketScraper.Core.Data
{
    public class ItemListing : IListing
    {
        internal ItemListing(Item itemOf, int price, int quantity, string location)
        {
            this.ItemOf = itemOf;
            this.Price = price;
            this.Quantity = quantity;
            this.Location = location;

        }

        public Item ItemOf { get; }

        public int Price { get; }
        public int Quantity { get; }
        public string Location { get; }
    }
}