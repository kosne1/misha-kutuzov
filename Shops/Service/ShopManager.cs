using System.Collections.Generic;
using Shops.Entities;

namespace Shops.Service
{
    public class ShopManager
    {
        public ShopManager()
        {
            Shops = new List<Shop>();
            Products = new List<Product>();
        }

        public List<Product> Products { get; }
        public List<Shop> Shops { get; }

        public void RegisterShop(Shop shop)
        {
            Shops.Add(shop);
        }

        public void RegisterProduct(Product product)
        {
            Products.Add(product);
        }
    }
}