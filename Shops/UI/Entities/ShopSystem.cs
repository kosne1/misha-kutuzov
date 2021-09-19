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

        public bool MakeAction(ConsoleService consoleService, ShopManager shopManager, List<Person> persons)
        {
            string action = consoleService.AskForAction();

            /*
             * false - no quit
             * true - quit
            */

            switch (action)
            {
                case "Add Person":
                    AddPerson(consoleService, persons);
                    return false;
                case "Create Shop":
                    CreateShop(consoleService, shopManager);
                    return false;
                case "Register Product":
                    RegisterProduct(consoleService, shopManager);
                    return false;
                case "Add Products":
                    AddProducts(consoleService, shopManager);
                    return false;
                case "Change Price":
                    ChangePrice(consoleService, shopManager);
                    return false;
                case "Buy Products":
                    BuyProduct(consoleService, shopManager, persons);
                    return false;
                case "Show Shop Info":
                    ShowShopInfo(consoleService, shopManager);
                    return false;
                case "Show Persons Info":
                    ShowPersonsInfo(consoleService, persons);
                    return false;
                case "Show Registered Products":
                    ShowRegisteredProducts(consoleService, shopManager);
                    return false;
                case "Quit":
                    return true;
                default:
                    consoleService.PrintWrongChoice(action);
                    return false;
            }
        }

        public void AddPerson(ConsoleService consoleService, List<Person> persons)
        {
            string name = consoleService.AskForString("Name");
            uint balance = consoleService.AskForUInt("Balance");
            consoleService.Clear();
            persons.Add(new Person(name, balance));
        }

        public void CreateShop(ConsoleService consoleService, ShopManager shopManager)
        {
            string name = consoleService.AskForString("Name");
            string address = consoleService.AskForString("Address");
            consoleService.Clear();
            shopManager.Create(new Shop(name, _shopId++, address));
        }

        public void RegisterProduct(ConsoleService consoleService, ShopManager shopManager)
        {
            string name = consoleService.AskForString("Name");
            consoleService.Clear();
            shopManager.RegisterProduct(new Product(name, _productId++));
        }

        public void AddProducts(ConsoleService consoleService, ShopManager shopManager)
        {
            while (true)
            {
                Shop chosenShop = consoleService.AskForShop(shopManager);
                Product chosenProduct = consoleService.AskForProduct(shopManager);
                uint amount = consoleService.AskForUInt("Amount");
                uint price = consoleService.AskForUInt("Price");

                chosenShop?.AddProduct(chosenProduct, amount, price);

                if (consoleService.AskToRepeat())
                {
                    continue;
                }

                break;
            }
        }

        public void ChangePrice(ConsoleService consoleService, ShopManager shopManager)
        {
            while (true)
            {
                Shop chosenShop = consoleService.AskForShop(shopManager);
                Product chosenProduct = consoleService.AskForProduct(shopManager);
                uint newPrice = consoleService.AskForUInt("New Price");

                chosenShop?.ChangePrice(chosenProduct, newPrice);

                if (consoleService.AskToRepeat())
                {
                    continue;
                }

                break;
            }
        }

        public void BuyProduct(ConsoleService consoleService, ShopManager shopManager, List<Person> persons)
        {
            while (true)
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

                if (consoleService.AskToRepeat())
                {
                    continue;
                }

                break;
            }
        }

        public void ShowShopInfo(ConsoleService consoleService, ShopManager shopManager)
        {
            while (true)
            {
                Shop chosenShop = consoleService.AskForShop(shopManager);
                consoleService.PrintShopInfo(chosenShop);

                if (consoleService.AskToRepeat())
                {
                    continue;
                }

                break;
            }
        }

        public void ShowPersonsInfo(ConsoleService consoleService, List<Person> persons)
        {
            consoleService.PrintPersonsInfo(persons.AsReadOnly());
        }

        public void ShowRegisteredProducts(ConsoleService consoleService, ShopManager shopManager)
        {
            consoleService.PrintRegisteredProducts(shopManager.Products().AsReadOnly());
        }
    }
}