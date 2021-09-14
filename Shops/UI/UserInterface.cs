using System.Collections.Generic;
using System.Linq;
using Shops.Entities;
using Shops.Service;
using Spectre.Console;

namespace Shops.UI
{
    public class UserInterface
    {
        private ShopManager _shopManager;
        private List<Person> _persons;

        public UserInterface()
        {
            _shopManager = new ShopManager();
            _persons = new List<Person>();
        }

        public string GetAction()
        {
            AnsiConsole.Clear();
            string action = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Welcome to [green]shop manager[/]!")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more Actions)[/]")
                    .AddChoices(new[] { "Add Person", "Create Shop", "Register Product", "Add Products", "Quit", }));

            return action;
        }

        public void AddPerson()
        {
            string name = AnsiConsole.Ask<string>("What's [green]name[/]?");
            int balance = AnsiConsole.Prompt(
                new TextPrompt<int>("What's the [green]balance[/]?")
                    .Validate(age =>
                    {
                        return age switch
                        {
                            < 0 => ValidationResult.Error("[red]Balance can't be negative[/]"),
                            _ => ValidationResult.Success(),
                        };
                    }));

            _persons.Add(new Person(name, balance));
        }

        public void CreateShop()
        {
            string name = AnsiConsole.Ask<string>("What's [green]name[/]?");
            string address = AnsiConsole.Ask<string>("What's [green]address[/]?");
            _shopManager.Create(name, address);
        }

        public void RegisterProduct()
        {
            string name = AnsiConsole.Ask<string>("What's [green]name[/]?");
            _shopManager.RegisterProduct(name);
        }

        public void AddProducts()
        {
            while (true)
            {
                string shop = AnsiConsole.Prompt(new SelectionPrompt<string>().Title("Choose [green]shop[/]!")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more shops)[/]")
                    .AddChoices(_shopManager.Shops.Select(s => s.Name)));

                string product = AnsiConsole.Prompt(new SelectionPrompt<string>().Title("Choose [green]product[/]!")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more products)[/]")
                    .AddChoices(_shopManager.Products.Select(p => p.Name)));

                int amount = AnsiConsole.Prompt(new TextPrompt<int>("What's the [green]amount[/]?").Validate(amount =>
                {
                    return amount switch
                    {
                        < 0 => ValidationResult.Error("[red]Amount can't be negative[/]"),
                        _ => ValidationResult.Success(),
                    };
                }));
                int price = AnsiConsole.Prompt(new TextPrompt<int>("What's the [green]price[/]?").Validate(price =>
                {
                    return price switch
                    {
                        < 0 => ValidationResult.Error("[red]Price can't be negative[/]"),
                        _ => ValidationResult.Success(),
                    };
                }));

                Shop chosenShop = _shopManager.Shops.Find(s => s.Name == shop);
                Product chosenProduct = _shopManager.Products.Find(p => p.Name == product);
                chosenShop.AddProduct(chosenProduct, amount, price);
                if (AnsiConsole.Confirm("Add another product?"))
                {
                    continue;
                }

                break;
            }
        }
    }
}