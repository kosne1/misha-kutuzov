using NUnit.Framework;
using Shops.Entities;
using Shops.Service;
using Shops.Tools;

namespace Shops.Tests
{
    public class ShopManagerTest
    {
        [Test]
        [TestCase(500u, 500u, 10u, 30u, 1u, 1u)]
        public void CreateShopAddProducts_BuyProducts(uint moneyBefore, uint amountToAdd, uint priceToAdd,
            uint amountToBuy, uint shopId, uint productId)
        {
            var person = new Person("Misha", moneyBefore);
            var shop = new Shop("Diksi", shopId, "Optikov street");
            var snickers = new Product("Snickers", productId);

            shop.AddProduct(snickers, amountToAdd, priceToAdd);
            shop.Buy(person, snickers, amountToBuy);

            Assert.AreEqual(moneyBefore - priceToAdd * amountToBuy, person.Balance);
            Assert.AreEqual(amountToAdd - amountToBuy, shop.GetProductInfo(snickers).Amount);
        }

        [Test]
        [TestCase(500u, 45u, 35u, 6u, 9u)]
        public void SetupPriceChangePrice_PriceChanged(uint amountToAdd, uint priceToAdd, uint newPrice, uint shopId,
            uint productId)
        {
            var shop = new Shop("Diksi", shopId, "Optikov street");
            var snickers = new Product("Snickers", productId);

            shop.AddProduct(snickers, amountToAdd, priceToAdd);
            shop.ChangePrice(snickers, newPrice);

            Assert.AreEqual(newPrice, shop.GetProductInfo(snickers).Price);
        }

        [Test]
        [TestCase(500u, 45u, 1u, 1u)]
        public void AddProductsToShop_CanBuyProducts(uint amountToAdd, uint priceToAdd, uint shopId, uint productId)
        {
            var shop = new Shop("Diksi", shopId, "Optikov street");
            var snickers = new Product("Snickers", productId);

            shop.AddProduct(snickers, amountToAdd, priceToAdd);

            Assert.Contains(snickers, shop.Products.Keys);
        }

        [Test]
        [TestCase(500u, 45u, 1u, 1u, 2u)]
        public void AddProductsToShopFindNotExistingProduct_ThrowException(uint amountToAdd, uint priceToAdd,
            uint shopId, uint product1Id, uint product2Id)
        {
            Assert.Catch<ShopException>(() =>
                {
                    var shop = new Shop("Diksi", shopId, "Optikov street");
                    var snickers = new Product("Snickers", product1Id);
                    var mars = new Product("Mars", product2Id);

                    shop.AddProduct(snickers, amountToAdd, priceToAdd);

                    shop.GetProductInfo(mars);
                }
            );
        }
    }
}