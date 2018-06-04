namespace NovaMarketScraper.Core.Data
{
    public class EquipmentListing : IListing
    {
        public EquipmentListing(Item itemOf, int price, int refine, string additionalProperties, string location)
        {
            this.ItemOf = itemOf;
            this.Price = price;
            this.Refine = refine;
            this.AdditionalProperties = additionalProperties;
            this.Location = location;
        }
        
        public Item ItemOf { get; }

        public int Price { get; }
        public int Refine { get; }
        public string AdditionalProperties { get; }
        public string Location { get; }
    }
}