using System.Collections.Generic;
using Shops.Entities;
using Shops.Service;

namespace Shops.UI.Entities
{
    public class ShopSystem
    {
        private uint _shopId;
        private uint _productId;

        public void AddPerson(string name, uint balance, List<Person> persons)
        {
            persons.Add(new Person(name, balance));
        }

        public void CreateShop(string name, string address, ShopManager shopManager)
        {
            shopManager.RegisterShop(new Shop(name, _shopId++, address));
        }

        public void RegisterProduct(string name, ShopManager shopManager)
        {
            shopManager.RegisterProduct(new Product(name, _productId++));
        }

        public void AddProducts(Shop chosenShop, Product chosenProduct, uint amount, uint price)
        {
            chosenShop.AddProduct(chosenProduct, amount, price);
        }

        public void ChangePrice(Shop chosenShop, Product chosenProduct, uint newPrice)
        {
            chosenShop.ChangePrice(chosenProduct, newPrice);
        }

        public void BuyProduct(Shop chosenShop, Person chosenPerson, Product chosenProduct, uint amountToBuy)
        {
            chosenShop.Buy(chosenPerson, chosenProduct, amountToBuy);
        }
    }
}