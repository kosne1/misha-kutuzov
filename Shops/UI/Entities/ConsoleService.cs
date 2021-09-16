using System.Collections.Generic;
using Shops.Entities;
using Shops.Service;
using Shops.Tools;
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

        public string AskForString(string value)
        {
            return _inputService.GetString(value);
        }

        public int AskForInt(string value)
        {
            return _inputService.GetInt(value);
        }

        public string AskForAction()
        {
            return _inputService.GetAction();
        }

        public bool AskToRepeat()
        {
            return _inputService.GetConfirm();
        }

        public bool AskToContinue()
        {
            return _inputService.GetContinue();
        }

        public void PrintException(ShopException e)
        {
            _outputService.PrintException(e);
        }

        public Shop AskForShop(ShopManager shopManager)
        {
            string shopName = _inputService.GetShopName(shopManager);

            return shopManager.Shops.Find(s => s.Name == shopName);
        }

        public Product AskForProduct(ShopManager shopManager)
        {
            string productName = _inputService.GetProductName(shopManager);

            return shopManager.Products.Find(p => p.Name == productName);
        }

        public Person AskForPerson(List<Person> persons)
        {
            string personName = _inputService.GetPersonName(persons);

            return persons.Find(p => p.Name == personName);
        }

        public void PrintShopInfo(Shop shop)
        {
            _outputService.PrintShopInfo(shop);
        }

        public void PrintPersonsInfo(List<Person> persons)
        {
            _outputService.PrintPersonsInfo(persons);
        }

        public void PrintRegisteredProducts(List<Product> products)
        {
            _outputService.PrintProducts(products);
        }
    }
}