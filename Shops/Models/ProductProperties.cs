using Shops.Tools;

namespace Shops.Models
{
    public class ProductProperties
    {
        public ProductProperties(int amount, int price)
        {
            Amount = amount;
            Price = price;
        }

        public int Amount { get; set; }
        public int Price { get; set; }
    }
}