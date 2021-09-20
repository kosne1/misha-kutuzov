using System.Collections.Generic;
using Shops.Entities;
using Shops.Service;
using Shops.Tools;

namespace Shops.UI.Entities
{
    public class ShopSystem
    {
        private uint _shopId = 1;
        private uint _productId = 1;

        public void Run(ConsoleService consoleService, ShopManager shopManager, List<Person> persons)
        {
            while (true)
            {
                bool shouldQuit = MakeAction(consoleService, shopManager, persons);
                if (shouldQuit)
                    return;
            }
        }

        private bool MakeAction(ConsoleService consoleService, ShopManager shopManager, List<Person> persons)
        {
            string action = consoleService.AskForAction();

            switch (action)
            {
                case "Add Person":
                    AddPerson(consoleService, persons);
                    break;
                case "Create Shop":
                    CreateShop(consoleService, shopManager);
                    break;
                case "Register Product":
                    RegisterProduct(consoleService, shopManager);
                    break;
                case "Add Products":
                    AddProducts(consoleService, shopManager);
                    break;
                case "Change Price":
                    ChangePrice(consoleService, shopManager);
                    break;
                case "Buy Products":
                    BuyProduct(consoleService, shopManager, persons);
                    break;
                case "Show Shop Info":
                    ShowShopInfo(consoleService, shopManager);
                    break;
                case "Show Persons Info":
                    ShowPersonsInfo(consoleService, persons);
                    break;
                case "Show Registered Products":
                    ShowRegisteredProducts(consoleService, shopManager);
                    break;
                case "Quit":
                    return true;
            }

            return false;
        }

        private void AddPerson(ConsoleService consoleService, List<Person> persons)
        {
            string name = consoleService.AskForString("Name");
            uint balance = consoleService.AskForUInt("Balance");
            consoleService.Clear();
            persons.Add(new Person(name, balance));
        }

        private void CreateShop(ConsoleService consoleService, ShopManager shopManager)
        {
            string name = consoleService.AskForString("Name");
            string address = consoleService.AskForString("Address");
            consoleService.Clear();
            shopManager.Create(new Shop(name, _shopId++, address));
        }

        private void RegisterProduct(ConsoleService consoleService, ShopManager shopManager)
        {
            string name = consoleService.AskForString("Name");
            consoleService.Clear();
            shopManager.RegisterProduct(new Product(name, _productId++));
        }

        private void AddProducts(ConsoleService consoleService, ShopManager shopManager)
        {
            Shop chosenShop = consoleService.AskForShop(shopManager);
            Product chosenProduct = consoleService.AskForProduct(shopManager);
            uint amount = consoleService.AskForUInt("Amount");
            uint price = consoleService.AskForUInt("Price");

            chosenShop?.AddProduct(chosenProduct, amount, price);

            do
            {
                AddProducts(consoleService, shopManager);
            }
            while (consoleService.AskToRepeat());
        }

        private void ChangePrice(ConsoleService consoleService, ShopManager shopManager)
        {
            Shop chosenShop = consoleService.AskForShop(shopManager);
            Product chosenProduct = consoleService.AskForProduct(shopManager);
            uint newPrice = consoleService.AskForUInt("New Price");

            chosenShop?.ChangePrice(chosenProduct, newPrice);

            do
            {
                ChangePrice(consoleService, shopManager);
            }
            while (consoleService.AskToRepeat());
        }

        private void BuyProduct(ConsoleService consoleService, ShopManager shopManager, List<Person> persons)
        {
            Shop chosenShop = consoleService.AskForShop(shopManager);
            Person chosenPerson = consoleService.AskForPerson(persons);
            Product chosenProduct = consoleService.AskForProduct(shopManager);
            uint amountToBuy = consoleService.AskForUInt("Amount to Buy");

            try
            {
                chosenShop?.Buy(chosenPerson, chosenProduct, amountToBuy);
            }
            catch (ShopException e)
            {
                consoleService.PrintException(e);
            }

            do
            {
                BuyProduct(consoleService, shopManager, persons);
            }
            while (consoleService.AskToRepeat());
        }

        private void ShowShopInfo(ConsoleService consoleService, ShopManager shopManager)
        {
            Shop chosenShop = consoleService.AskForShop(shopManager);
            consoleService.PrintShopInfo(chosenShop);

            do
            {
                AddProducts(consoleService, shopManager);
            }
            while (consoleService.AskToRepeat());
        }

        private void ShowPersonsInfo(ConsoleService consoleService, List<Person> persons)
        {
            consoleService.PrintPersonsInfo(persons.AsReadOnly());
        }

        private void ShowRegisteredProducts(ConsoleService consoleService, ShopManager shopManager)
        {
            consoleService.PrintRegisteredProducts(shopManager.Products.AsReadOnly());
        }
    }
}