namespace Shops.Entities
{
    public class Product
    {
        public Product(string name)
        {
            Name = name;
        }

        public Product(string name, int amount, int price)
        {
            Name = name;
            Amount = amount;
            Price = price;
        }

        public string Name { get; }
        public int Amount { get; set; }
        public int Price { get; set; }
    }
}