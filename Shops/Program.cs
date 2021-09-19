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
            var shopSystem = new ShopSystem();
            var consoleService = new ConsoleService();
            var shopManager = new ShopManager();
            var persons = new List<Person>();

            while (true)
            {
                if (shopSystem.MakeAction(consoleService, shopManager, persons))
                    break;
            }
        }
    }
}