using Shops.Tools;

namespace Shops.Entities
{
    public class Product
    {
        public Product(string name, uint id)
        {
            if (!IsNameValid(name)) throw new ShopException("Can't create product with empty or null name");
            Name = name;
            Id = id;
        }

        public string Name { get; }
        public uint Id { get; }

        private bool IsNameValid(string name)
        {
            return !string.IsNullOrEmpty(name);
        }
    }
}