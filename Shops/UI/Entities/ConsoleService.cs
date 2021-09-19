using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public uint AskForUInt(string value)
        {
            return _inputService.GetUInt(value);
        }

        public string AskForAction()
        {
            return _inputService.GetAction();
        }

        public bool AskToRepeat()
        {
            return _inputService.GetConfirm();
        }

        public void Clear()
        {
            _outputService.Clear();
        }

        public void PrintException(ShopException e)
        {
            _outputService.PrintException(e);
        }

        public Shop AskForShop(ShopManager shopManager)
        {
            string shopName = _inputService.GetShopName(shopManager);

            return shopManager.Shops().Find(s => s.Name() == shopName);
        }

        public Product AskForProduct(ShopManager shopManager)
        {
            string productName = _inputService.GetProductName(shopManager);

            return shopManager.Products().Find(p => p.Name == productName);
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

        public void PrintPersonsInfo(ReadOnlyCollection<Person> persons)
        {
            _outputService.PrintPersonsInfo(persons);
        }

        public void PrintRegisteredProducts(ReadOnlyCollection<Product> products)
        {
            _outputService.PrintProducts(products);
        }

        public void PrintWrongChoice(string action)
        {
            _outputService.PrintWrongChoice(action);
        }
    }
}