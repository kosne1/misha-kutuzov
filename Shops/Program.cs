using Shops.UI;

namespace Shops
{
    internal class Program
    {
        private static void Main()
        {
            var userInterface = new UserInterface();

            while (true)
            {
                string action = userInterface.GetAction();
                switch (action)
                {
                    case "Add Person":
                        userInterface.AddPerson();
                        break;
                    case "Create Shop":
                        userInterface.CreateShop();
                        break;
                    case "Register Product":
                        userInterface.RegisterProduct();
                        break;
                    case "Add Products":
                        userInterface.AddProducts();
                        break;
                    case "Quit":
                        return;
                }
            }
        }
    }
}