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
        [TestCase(500, 500, 10, 30, 1, 1)]
        public void CreateShopAddProducts_BuyProducts(uint moneyBefore, uint amountToAdd, uint priceToAdd,
            uint amountToBuy, uint shopId, uint productId)
        {
            var person = new Person("Misha", moneyBefore);
            var shop = new Shop("Diksi", shopId, "Optikov street");
            _shopManager.Create(shop);
            var snickers = new Product("Snickers", productId);
            _shopManager.RegisterProduct(snickers);

            shop.AddProduct(snickers, amountToAdd, priceToAdd);
            shop.Buy(person, snickers, amountToBuy);

            Assert.AreEqual(moneyBefore - priceToAdd * amountToBuy, person.Balance);
            Assert.AreEqual(amountToAdd - amountToBuy, shop.GetProductInfo(snickers).Amount);
        }

        [Test]
        [TestCase(500, 45, 35, 6, 9)]
        public void SetupPriceChangePrice_PriceChanged(uint amountToAdd, uint priceToAdd, uint newPrice, uint shopId,
            uint productId)
        {
            var shop = new Shop("Diksi", shopId, "Optikov street");
            _shopManager.Create(shop);
            var snickers = new Product("Snickers", productId);
            _shopManager.RegisterProduct(snickers);

            shop.AddProduct(snickers, amountToAdd, priceToAdd);
            shop.ChangePrice(snickers, newPrice);

            Assert.AreEqual(newPrice, shop.GetProductInfo(snickers).Price);
        }

        [Test]
        [TestCase(500, 45, 1, 1)]
        public void AddProductsToShop_CanBuyProducts(uint amountToAdd, uint priceToAdd, uint shopId, uint productId)
        {
            var shop = new Shop("Diksi", shopId, "Optikov street");
            _shopManager.Create(shop);
            var snickers = new Product("Snickers", productId);
            _shopManager.RegisterProduct(snickers);

            shop.AddProduct(snickers, amountToAdd, priceToAdd);

            Assert.Contains(snickers, shop.Products().Keys);
        }

        [Test]
        [TestCase(500, 45, 1, 1, 2)]
        public void AddProductsToShopFindNotExistingProduct_ThrowException(uint amountToAdd, uint priceToAdd,
            uint shopId, uint product1Id, uint product2Id)
        {
            Assert.Catch<ShopException>(() =>
                {
                    var shop = new Shop("Diksi", shopId, "Optikov street");
                    _shopManager.Create(shop);
                    var snickers = new Product("Snickers", product1Id);
                    _shopManager.RegisterProduct(snickers);
                    var mars = new Product("Mars", product2Id);
                    _shopManager.RegisterProduct(mars);

                    shop.AddProduct(snickers, amountToAdd, priceToAdd);

                    shop.GetProductInfo(snickers);
                }
            );
        }
    }
}