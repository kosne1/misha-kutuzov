using System.Collections.Generic;
using System.Linq;
using Shops.Models;
using Shops.Tools;

namespace Shops.Entities
{
    public class Shop
    {
        private readonly string _address;

        public Shop(string name, uint id, string address)
        {
            Name = name;
            Id = id;
            _address = address;
            Products = new Dictionary<Product, ProductProperties>();
        }

        public Dictionary<Product, ProductProperties> Products { get; }
        public string Name { get; }
        public uint Id { get; }

        public void AddProduct(Product product, uint amount, uint price)
        {
            ProductProperties foundProductInfo =
                Products.FirstOrDefault(p => p.Key.Id == product.Id).Value;

            if (foundProductInfo == null)
            {
                var productProperties = new ProductProperties(amount, price);
                Products.Add(product, productProperties);
            }
            else
            {
                foundProductInfo.Amount += amount;
                foundProductInfo.Price = price;
            }
        }

        public void Buy(Person person, Product product, uint amountToBuy)
        {
            ProductProperties foundProductInfo = GetProductInfo(product);

            if (amountToBuy > foundProductInfo.Amount)
                throw new ShopException($"There is not enough {product.Name} in {Name} for {person.Name}");

            uint totalCost = foundProductInfo.Price * amountToBuy;
            person.Buy(totalCost);
            foundProductInfo.Amount -= amountToBuy;
        }

        public void ChangePrice(Product product, uint newPrice)
        {
            ProductProperties foundProductInfo = GetProductInfo(product);

            foundProductInfo.Price = newPrice;
        }

        public ProductProperties GetProductInfo(Product product)
        {
            ProductProperties foundProductInfo =
                Products.FirstOrDefault(p => p.Key.Id == product.Id).Value;

            if (foundProductInfo == null) throw new ShopException($"There is no {product.Name} in {Name}");

            return foundProductInfo;
        }
    }
}