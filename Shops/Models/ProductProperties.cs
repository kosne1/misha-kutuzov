namespace Shops.Models
{
    public class ProductProperties
    {
        public ProductProperties(uint amount, uint price)
        {
            Amount = amount;
            Price = price;
        }

        public uint Amount { get; set; }
        public uint Price { get; set; }
    }
}