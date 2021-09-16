using System.Collections.Generic;
using System.Linq;
using Shops.Entities;
using Shops.Service;
using Spectre.Console;

namespace Shops.UI.Services
{
    public class InputService
    {
        public string GetString(string value)
        {
            return AnsiConsole.Ask<string>($"What's [green]{value}[/]?");
        }

        public int GetInt(string value)
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

        public string GetAction()
        {
            AnsiConsole.Clear();
            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[green]Shop manager[/] v1.0")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more Actions)[/]")
                    .AddChoices(new[]
                    {
                        "Add Person", "Create Shop", "Register Product", "Add Products", "Buy Products",
                        "Change Price", "Show Shop Info", "Show Persons Info", "Show Registered Products",
                        "Quit",
                    }));
        }

        public bool GetConfirm()
        {
            return AnsiConsole.Confirm("Repeat operation?");
        }

        public bool GetContinue()
        {
            return AnsiConsole.Confirm("Continue?");
        }

        public string GetShopName(ShopManager shopManager)
        {
            return AnsiConsole.Prompt(new SelectionPrompt<string>().Title($"Choose [green]shop[/]!")
                .PageSize(10)
                .MoreChoicesText($"[grey](Move up and down to reveal more shops)[/]")
                .AddChoices(shopManager.Shops.Select(arg => arg.Name)));
        }

        public string GetProductName(ShopManager shopManager)
        {
            return AnsiConsole.Prompt(new SelectionPrompt<string>().Title($"Choose [green]product[/]!")
                .PageSize(10)
                .MoreChoicesText($"[grey](Move up and down to reveal more products)[/]")
                .AddChoices(shopManager.Products.Select(arg => arg.Name)));
        }

        public string GetPersonName(List<Person> persons)
        {
            return AnsiConsole.Prompt(new SelectionPrompt<string>().Title($"Choose [green]person[/]!")
                .PageSize(10)
                .MoreChoicesText($"[grey](Move up and down to reveal more persons)[/]")
                .AddChoices(persons.Select(arg => arg.Name)));
        }
    }
}