using System.Collections.Generic;
using Shops.Entities;
using Shops.Service;
using Shops.UI.Entities;

namespace Shops
{
    internal class Program
    {
        private static void Main()
        {
            var consoleService = new ConsoleService();

            var shopSystem = new ShopSystem();
            var shopManager = new ShopManager();
            var persons = new List<Person>();

            consoleService.Run(shopSystem, shopManager, persons);
        }
    }
}