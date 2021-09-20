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

        public uint GetUInt(string value)
        {
            return AnsiConsole.Ask<uint>($"What's [green]{value}[/]?");
        }

        public string GetAction()
        {
            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[green]Shop manager[/] v2.0")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more Actions)[/]")
                    .AddChoices(
                        "Add Person",
                        "Create Shop",
                        "Register Product",
                        "Add Products",
                        "Buy Products",
                        "Change Price",
                        "Show Shop Info",
                        "Show Persons Info",
                        "Show Registered Products",
                        "Quit"));
        }

        public bool GetConfirm()
        {
            return AnsiConsole.Confirm("Repeat operation?");
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