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
        public void CreateShopAddProducts_BuyProducts()
        {
            const int moneyBefore = 500;
            const int productToBuyCount = 3;

            var person = new Person("Misha", moneyBefore);
            Shop shop = _shopManager.Create("Diksi", "Optikov street");
            Product snickers = _shopManager.RegisterProduct("Snickers");
            Product lambert = _shopManager.RegisterProduct("Lambert");

            Product[] products = {snickers, lambert};
            int[] productsCount = {10, 10};
            int[] productsPrices = {30, 150};

            shop.AddProducts(products, productsCount, productsPrices);
            shop.Buy(person, snickers, productToBuyCount);

            Assert.AreEqual(moneyBefore - productsPrices[0] * productToBuyCount, person.Balance);
            Assert.AreEqual(productsCount[0] - productToBuyCount, shop.GetProductInfo(snickers).Amount);
        }

        [Test]
        public void SetupPriceChangePrice_PriceChanged()
        {
            const int newPrice = 100;

            Shop shop = _shopManager.Create("Diksi", "Optikov street");
            Product snickers = _shopManager.RegisterProduct("Snickers");

            Product[] products = {snickers};
            int[] productsCount = {10};
            int[] productsPrices = {30};

            shop.AddProducts(products, productsCount, productsPrices);
            shop.ChangePrice(snickers, newPrice);

            Assert.AreEqual(newPrice, snickers.Price);
        }

        [Test]
        public void AddProductsToShop_CanBuyProducts()
        {
            Shop shop = _shopManager.Create("Diksi", "Optikov street");
            Product snickers = _shopManager.RegisterProduct("Snickers");
            Product lambert = _shopManager.RegisterProduct("Lambert");

            Product[] products = {snickers, lambert};
            int[] productsCount = {10, 10};
            int[] productsPrices = {30, 150};

            shop.AddProducts(products, productsCount, productsPrices);

            Assert.Contains(snickers, shop.Products);
        }

        [Test]
        public void AddProductFindCheapestShop_FoundShop()
        {
            const int snickersToBuyCount = 25;
            const int lambertToBuyCount = 125;
            const int moneyBefore = 900;

            var person = new Person("Misha", moneyBefore);

            Shop diksi = _shopManager.Create("Diksi", "Optikov street");
            Shop lenta = _shopManager.Create("Lenta", "Turistskaya street");

            Product snickers = _shopManager.RegisterProduct("Snickers");
            Product lambert = _shopManager.RegisterProduct("Lambert");

            Product[] products = {snickers, lambert};

            int[] diksiProductsCount = {10, 10};
            int[] diksiProductsPrices = {20, 5};

            int[] lentaProductsCount = {35, 20};
            int[] lentaProductsPrices = {15, 30};

            diksi.AddProducts(products, diksiProductsCount, diksiProductsPrices);
            lenta.AddProducts(products, lentaProductsCount, lentaProductsPrices);

            int cheapestSnickers = _shopManager.Shops.Select(shop => shop.GetProductInfo(snickers).Price)
                .Prepend(int.MaxValue).Min();
            int cheapestLambert = _shopManager.Shops.Select(shop => shop.GetProductInfo(lambert).Price)
                .Prepend(int.MaxValue).Min();

            foreach (Shop shop in _shopManager.Shops.Where(shop =>
                shop.GetProductInfo(snickers).Price == cheapestSnickers))
            {
                shop.Buy(person, snickers, snickersToBuyCount);
            }

            Assert.AreEqual(moneyBefore - cheapestSnickers * snickersToBuyCount, person.Balance);

            Assert.Catch<ShopException>(() =>
            {
                foreach (Shop shop in _shopManager.Shops.Where(shop =>
                    shop.GetProductInfo(lambert).Price == cheapestLambert))
                {
                    shop.Buy(person, lambert, lambertToBuyCount);
                }
            });
        }
    }
}