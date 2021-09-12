using System.Collections.Generic;
using Shops.Entities;
using Spectre.Console;

namespace Shops.Service
{
    public class ShopManager
    {
        private int _shopId = 1;

        public ShopManager()
        {
            Shops = new List<Shop>();
        }

        public List<Shop> Shops { get; }

        public Shop Create(string name, string address)
        {
            var shop = new Shop(name, _shopId++, address);
            Shops.Add(shop);
            return shop;
        }

        public Product RegisterProduct(string name)
        {
            return new Product(name);
        }
    }
}