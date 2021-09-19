using System.Collections.Generic;
using System.Linq;
using Shops.Models;
using Shops.Tools;

namespace Shops.Entities
{
    public class Shop
    {
        private readonly uint _id;
        private readonly string _address;
        private readonly Dictionary<Product, ProductProperties> _products;
        private readonly string _name;

        public Shop(string name, uint id, string address)
        {
            _name = name;
            _id = id;
            _address = address;
            _products = new Dictionary<Product, ProductProperties>();
        }

        public string Name()
        {
            return _name;
        }

        public Dictionary<Product, ProductProperties> Products()
        {
            return _products;
        }

        public void AddProduct(Product product, uint amount, uint price)
        {
            ProductProperties foundProductInfo = GetProductProperties(product);

            if (foundProductInfo != null)
            {
                foundProductInfo.Amount += amount;
                foundProductInfo.Price = price;
            }
            else
            {
                var productProperties = new ProductProperties(amount, price);
                _products.Add(product, productProperties);
            }
        }

        public void Buy(Person person, Product product, uint amountToBuy)
        {
            ProductProperties foundProductInfo = GetProductProperties(product);

            if (foundProductInfo == null)
                throw new ShopException($"There is no {product.Name} in {_name} for {person.Name}");

            if (amountToBuy > foundProductInfo.Amount)
                throw new ShopException($"There is not enough {product.Name} in {_name} for {person.Name}");

            uint totalCost = foundProductInfo.Price * amountToBuy;
            person.Buy(totalCost);
            foundProductInfo.Amount -= amountToBuy;
        }

        public void ChangePrice(Product product, uint newPrice)
        {
            ProductProperties foundProductInfo = GetProductProperties(product);

            if (foundProductInfo == null) throw new ShopException($"There is no {product.Name} in {_name}");

            foundProductInfo.Price = newPrice;
        }

        public ProductProperties GetProductInfo(Product product)
        {
            ProductProperties foundProductInfo = GetProductProperties(product);

            if (foundProductInfo == null) throw new ShopException($"There is no {product.Name} in {_name}");

            return foundProductInfo;
        }

        private ProductProperties GetProductProperties(Product product)
        {
            return _products.FirstOrDefault(p => p.Key.Id == product.Id).Value;
        }
    }
}