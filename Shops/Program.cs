using Shops.UI.Entities;

namespace Shops
{
    internal class Program
    {
        private static void Main()
        {
            var shopSystem = new ShopSystem();

            while (true)
            {
                string action = shopSystem.GetAction();
                switch (action)
                {
                    case "Add Person":
                        shopSystem.AddPerson();
                        break;
                    case "Create Shop":
                        shopSystem.CreateShop();
                        break;
                    case "Register Product":
                        shopSystem.RegisterProduct();
                        break;
                    case "Add Products":
                        shopSystem.AddProducts();
                        break;
                    case "Change Price":
                        shopSystem.ChangePrice();
                        break;
                    case "Buy Products":
                        shopSystem.BuyProduct();
                        break;
                    case "Show Shop Info":
                        shopSystem.ShowShopInfo();
                        break;
                    case "Show Persons Info":
                        shopSystem.ShowPersonsInfo();
                        break;
                    case "Quit":
                        return;
                }
            }
        }
    }
}