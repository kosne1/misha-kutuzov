using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Shops.Entities;
using Shops.Service;
using Shops.Tools;

namespace Shops.Tests
{
    public class ShopManagerTest
    {
        private ShopManager _shopManager;

        [SetUp]
        public void Setup()
        {
            _shopManager = new ShopManager();
        }

        [Test]
        [TestCase(500, 500, 10, 30)]
        public void CreateShopAddProducts_BuyProducts(int moneyBefore, int amountToAdd, int priceToAdd, int amountToBuy)
        {
            var person = new Person("Misha", moneyBefore);
            Shop shop = _shopManager.Create("Diksi", "Optikov street");
            Product snickers = _shopManager.RegisterProduct("Snickers");

            shop.AddProduct(snickers, amountToAdd, priceToAdd);
            shop.Buy(person, snickers, amountToBuy);

            Assert.AreEqual(moneyBefore - priceToAdd * amountToBuy, person.Balance);
            Assert.AreEqual(amountToAdd - amountToBuy, shop.GetProductInfo(snickers).Amount);
        }

        [Test]
        [TestCase(500, 45, 35)]
        public void SetupPriceChangePrice_PriceChanged(int amountToAdd, int priceToAdd, int newPrice)
        {
            Shop shop = _shopManager.Create("Diksi", "Optikov street");
            Product snickers = _shopManager.RegisterProduct("Snickers");

            shop.AddProduct(snickers, amountToAdd, priceToAdd);
            shop.ChangePrice(snickers, newPrice);

            Assert.AreEqual(newPrice, shop.GetProductInfo(snickers).Price);
        }

        [Test]
        [TestCase(500, 45)]
        public void AddProductsToShop_CanBuyProducts(int amountToAdd, int priceToAdd)
        {
            Shop shop = _shopManager.Create("Diksi", "Optikov street");
            Product snickers = _shopManager.RegisterProduct("Snickers");

            shop.AddProduct(snickers, amountToAdd, priceToAdd);

            Assert.Contains(snickers, shop.Products.Keys);
        }

        [Test]
        public void AddProductFindCheapestShop_FoundShop()
        {
            
        }
    }
}