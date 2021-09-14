using System;
using System.Collections.Generic;
using Shops.Entities;
using Shops.Tools;

namespace Shops.Service
{
    public class ShopManager
    {
        private int _shopId = 1;
        private int _productId = 1;

        public ShopManager()
        {
            Shops = new List<Shop>();
            Products = new List<Product>();
        }

        public List<Shop> Shops { get; }
        public List<Product> Products { get; }

        public Shop Create(string name, string address)
        {
            var shop = new Shop(name, _shopId++, address);
            Shops.Add(shop);
            return shop;
        }

        public Product RegisterProduct(string name)
        {
            var newProduct = new Product(name, _productId++);

            foreach (Product product in Products)
            {
                if (string.Equals(product.Name, newProduct.Name, StringComparison.CurrentCultureIgnoreCase))
                {
                    throw new ShopException(
                        $"Product {newProduct.Name} is already registered as {product.Name} with {product.Id} ID");
                }
            }

            Products.Add(newProduct);
            return newProduct;
        }
    }
}