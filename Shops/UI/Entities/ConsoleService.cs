using System.Collections.Generic;
using Shops.Entities;
using Shops.Service;
using Shops.UI.Services;

namespace Shops.UI.Entities
{
    public class ConsoleService
    {
        private readonly InputService _inputService;
        private readonly OutputService _outputService;

        public ConsoleService()
        {
            _inputService = new InputService();
            _outputService = new OutputService();
        }

        public void Run(ShopSystem shopSystem, ShopManager shopManager, List<Person> persons)
        {
            while (true)
            {
                bool shouldQuit = MakeAction(shopSystem, shopManager, persons);
                if (shouldQuit)
                    return;
            }
        }

        private bool MakeAction(ShopSystem shopSystem, ShopManager shopManager, List<Person> persons)
        {
            string action = _inputService.GetAction();

            switch (action)
            {
                case "Add Person":
                    (string personName, uint balance) = GetPersonInfo();
                    shopSystem.AddPerson(personName, balance, persons);
                    break;
                case "Create Shop":
                    (string shopName, string address) = GetShopInfo();
                    shopSystem.CreateShop(shopName, address, shopManager);
                    break;
                case "Register Product":
                    string productName = GetProductInfo();
                    shopSystem.RegisterProduct(productName, shopManager);
                    break;
                case "Add Products":
                    shopSystem.AddProducts(GetShop(shopManager), GetProduct(shopManager), GetAmount(), GetPrice());
                    break;
                case "Change Price":
                    shopSystem.ChangePrice(GetShop(shopManager), GetProduct(shopManager), GetNewPrice());
                    break;
                case "Buy Products":
                    shopSystem.BuyProduct(GetShop(shopManager), GetPerson(persons), GetProduct(shopManager), GetAmount());
                    break;
                case "Show Shop Info":
                    ShowShopInfo(GetShop(shopManager));
                    break;
                case "Show Persons":
                    ShowPersons(persons);
                    break;
                case "Show Registered Products":
                    ShowRegisteredProducts(shopManager);
                    break;
                case "Quit":
                    return true;
            }

            return false;
        }

        private (string, uint) GetPersonInfo()
        {
            string name = _inputService.GetString("Name");
            uint balance = _inputService.GetUInt("Balance");
            return (name, balance);
        }

        private (string, string) GetShopInfo()
        {
            string name = _inputService.GetString("Name");
            string address = _inputService.GetString("Address");
            return (name, address);
        }

        private string GetProductInfo()
        {
            string name = _inputService.GetString("Name");
            return name;
        }

        private uint GetNewPrice()
        {
            uint newPrice = _inputService.GetUInt("New Price");
            return newPrice;
        }

        private uint GetPrice()
        {
            uint price = _inputService.GetUInt("Price");
            return price;
        }

        private uint GetAmount()
        {
            uint amount = _inputService.GetUInt("Amount");
            return amount;
        }

        private Shop GetShop(ShopManager shopManager)
        {
            return shopManager.Shops.Find(s => s.Id == _inputService.GetShopId(shopManager));
        }

        private Product GetProduct(ShopManager shopManager)
        {
            return shopManager.Products.Find(s => s.Id == _inputService.GetProductId(shopManager));
        }

        private Person GetPerson(List<Person> persons)
        {
            return persons.Find(s => s.Name == _inputService.GetPersonName(persons));
        }

        private void ShowPersons(List<Person> persons)
        {
            _outputService.PrintPersons(persons.AsReadOnly());
        }

        private void ShowRegisteredProducts(ShopManager shopManager)
        {
            _outputService.PrintRegisteredProducts(shopManager.Products.AsReadOnly());
        }

        private void ShowShopInfo(Shop shop)
        {
            _outputService.PrintShopInfo(shop);
        }
    }
}