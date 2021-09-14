namespace Shops.Entities
{
    public class Product
    {
        public Product(string name, int id)
        {
            Name = name;
            Id = id;
        }

        public string Name { get; }
        public int Id { get; }
    }
}