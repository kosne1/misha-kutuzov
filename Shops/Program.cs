using Shops.Entities;
using Shops.Service;
using Shops.UI;

namespace Shops
{
    internal class Program
    {
        private static void Main()
        {
            var shopManager = new ShopManager();
            const int moneyBefore = 500;
            const int productToBuyCount = 3;

            var person = new Person("Misha", moneyBefore);
            Shop shop = shopManager.Create("Diksi", "Optikov street");
            Product snickers = shopManager.RegisterProduct("Snickers");
            Product lambert = shopManager.RegisterProduct("Lambert");

            Product[] products = { snickers, lambert };
            int[] productsCount = { 10, 10 };
            int[] productsPrices = { 30, 150 };

            shop.AddProducts(products, productsCount, productsPrices);
            shop.Buy(person, snickers, productToBuyCount);
        }
    }
}