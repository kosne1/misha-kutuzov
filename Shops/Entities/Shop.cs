using System.Collections.Generic;
using System.Linq;
using Shops.Models;
using Shops.Tools;

namespace Shops.Entities
{
    public class Shop
    {
        private int _id;
        private string _address;
        private string _name;

        public Shop(string name, int id, string address)
        {
            _name = name;
            _id = id;
            _address = address;
            Products = new Dictionary<Product, ProductProperties>();
        }

        public Dictionary<Product, ProductProperties> Products { get; }

        public void AddProduct(Product product, int amount, int price)
        {
            ProductProperties found = GetProductInfo(product);

            if (found != null)
            {
                found.Amount += amount;
                found.Price = price;
            }
            else
            {
                var productProperties = new ProductProperties(amount, price);
                Products.Add(product, productProperties);
            }
        }

        public void Buy(Person person, Product product, int amountToBuy)
        {
            ProductProperties found = GetProductInfo(product);

            if (found == null) throw new ShopException($"There is no {product.Name} in {_name} for {person.Name}");

            if (amountToBuy > found.Amount)
                throw new ShopException($"There is not enough {product.Name} in {_name} for {person.Name}");

            int totalCost = found.Price * amountToBuy;
            if (person.CanBuyProduct(totalCost))
            {
                person.Buy(totalCost);
                found.Amount -= amountToBuy;
            }
            else
            {
                throw new ShopException($"{person.Name} can't afford {product.Name} in {_name}");
            }
        }

        public void ChangePrice(Product product, int newPrice)
        {
            ProductProperties found = GetProductInfo(product);

            if (found == null) throw new ShopException($"There is no {product.Name} in {_name}");
            found.Price = newPrice;
        }

        public ProductProperties GetProductInfo(Product product)
        {
            return Products.Where(e => e.Key.Id == product.Id)
                .Select(p => p.Value)
                .FirstOrDefault();
        }
    }
}