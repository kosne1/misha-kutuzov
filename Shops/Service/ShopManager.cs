using System.Collections.Generic;
using System.Linq;
using Shops.Entities;
using Shops.Tools;

namespace Shops.Service
{
    public class ShopManager
    {
        private readonly List<Shop> _shops;
        private readonly List<Product> _products;

        public ShopManager()
        {
            _shops = new List<Shop>();
            _products = new List<Product>();
        }

        public void Create(Shop shop)
        {
            _shops.Add(shop);
        }

        public void RegisterProduct(Product product)
        {
            if (IsProductRegistered(product.Name))
                throw new ShopException($"Product {product.Name} is already registered!");

            _products.Add(product);
        }

        public List<Product> Products()
        {
            return _products;
        }

        public List<Shop> Shops()
        {
            return _shops;
        }

        private bool IsProductRegistered(string name)
        {
            Product foundProduct = _products.FirstOrDefault(p => p.Name == name);
            return foundProduct != null;
        }
    }
}