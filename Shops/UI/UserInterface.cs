using System.Collections.Generic;
using Shops.Entities;
using Shops.Service;
using Shops.Tools;
using Spectre.Console;

namespace Shops.UI
{
    public class UserInterface
    {
        private readonly ShopManager _shopManager;
        private readonly List<Person> _persons;
        private readonly ConsoleService _consoleService;

        public UserInterface()
        {
            _shopManager = new ShopManager();
            _persons = new List<Person>();
            _consoleService = new ConsoleService();
        }

        public string GetAction()
        {
            AnsiConsole.Clear();
            string action = _consoleService.AskForAction();
            return action;
        }

        public void AddPerson()
        {
            string name = _consoleService.AskForString("Name");
            int balance = _consoleService.AskForValidInt("Balance");
            _persons.Add(new Person(name, balance));
        }

        public void CreateShop()
        {
            string name = _consoleService.AskForString("Name");
            string address = _consoleService.AskForString("Address");
            _shopManager.Create(name, address);
        }

        public void RegisterProduct()
        {
            string name = _consoleService.AskForString("Name");
            _shopManager.RegisterProduct(name);
        }

        public void AddProducts()
        {
            Shop chosenShop = _consoleService.AskForShop(_shopManager);
            Product chosenProduct = _consoleService.AskForProduct(_shopManager);
            int amount = _consoleService.AskForValidInt("Amount");
            int price = _consoleService.AskForValidInt("Price");

            chosenShop?.AddProduct(chosenProduct, amount, price);

            if (_consoleService.AskToRepeat())
            {
                AddProducts();
            }
        }

        public void ChangePrice()
        {
            Shop chosenShop = _consoleService.AskForShop(_shopManager);
            Product chosenProduct = _consoleService.AskForProduct(_shopManager);
            int newPrice = _consoleService.AskForValidInt("New Price");

            chosenShop?.ChangePrice(chosenProduct, newPrice);

            if (_consoleService.AskToRepeat())
            {
                ChangePrice();
            }
        }

        public void BuyProduct()
        {
            Shop chosenShop = _consoleService.AskForShop(_shopManager);
            Person chosenPerson = _consoleService.AskForPerson(_persons);
            Product chosenProduct = _consoleService.AskForProduct(_shopManager);
            int amountToBuy = _consoleService.AskForValidInt("Amount to Buy");

            try
            {
                chosenShop?.Buy(chosenPerson, chosenProduct, amountToBuy);
            }
            catch (ShopException e)
            {
                _consoleService.PrintException(e);
            }

            if (_consoleService.AskToRepeat())
            {
                BuyProduct();
            }
        }

        public void GetShopInfo()
        {
            Shop chosenShop = _consoleService.AskForShop(_shopManager);
            _consoleService.PrintShopInfo(chosenShop);

            if (_consoleService.AskToRepeat())
            {
                GetShopInfo();
            }
        }
    }
}