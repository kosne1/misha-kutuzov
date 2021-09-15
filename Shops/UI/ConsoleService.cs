using System.Collections.Generic;
using System.Linq;
using Shops.Entities;
using Shops.Models;
using Shops.Service;
using Spectre.Console;

namespace Shops.UI
{
    public class ConsoleService
    {
        public string AskForString(string value)
        {
            return AnsiConsole.Ask<string>($"What's [green]{value}[/]?");
        }

        public int AskForValidInt(string value)
        {
            return AnsiConsole.Prompt(new TextPrompt<int>($"What's the [green]{value}[/]?").Validate(requested =>
            {
                return requested switch
                {
                    < 0 => ValidationResult.Error($"[red]{value} can't be negative[/]"),
                    _ => ValidationResult.Success(),
                };
            }));
        }

        public string AskForAction()
        {
            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[green]Shop manager[/] v1.0")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more Actions)[/]")
                    .AddChoices(new[]
                    {
                        "Add Person", "Create Shop", "Register Product", "Add Products", "Buy Products",
                        "Get Shop Info", "Change Price",
                        "Quit",
                    }));
        }

        public bool AskToRepeat()
        {
            return AnsiConsole.Confirm("Repeat operation?");
        }

        public Shop AskForShop(ShopManager shopManager)
        {
            string shopName = AnsiConsole.Prompt(new SelectionPrompt<string>().Title($"Choose [green]shop[/]!")
                .PageSize(10)
                .MoreChoicesText($"[grey](Move up and down to reveal more shops)[/]")
                .AddChoices(shopManager.Shops.Select(arg => arg.Name)));

            return shopManager.Shops.Find(s => s.Name == shopName);
        }

        public Product AskForProduct(ShopManager shopManager)
        {
            string productName = AnsiConsole.Prompt(new SelectionPrompt<string>().Title($"Choose [green]product[/]!")
                .PageSize(10)
                .MoreChoicesText($"[grey](Move up and down to reveal more products)[/]")
                .AddChoices(shopManager.Products.Select(arg => arg.Name)));

            return shopManager.Products.Find(p => p.Name == productName);
        }

        public Person AskForPerson(List<Person> persons)
        {
            string personName = AnsiConsole.Prompt(new SelectionPrompt<string>().Title($"Choose [green]person[/]!")
                .PageSize(10)
                .MoreChoicesText($"[grey](Move up and down to reveal more persons)[/]")
                .AddChoices(persons.Select(arg => arg.Name)));

            return persons.Find(p => p.Name == personName);
        }

        public void PrintShopInfo(Shop shop)
        {
            if (shop.Products.Count == 0)
            {
                AnsiConsole.MarkupLine("There are no Products");
            }

            foreach ((Product product, ProductProperties productProperties) in shop.Products)
            {
                AnsiConsole.Markup(product.Name);
                AnsiConsole.Markup(" Amount: " + productProperties.Amount);
                AnsiConsole.Markup(" Price: " + productProperties.Price + "\n");
            }
        }
    }
}