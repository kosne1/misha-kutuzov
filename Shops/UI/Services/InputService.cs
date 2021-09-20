using System;
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
                    .Title("[green]Shop manager[/] v3.0")
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
                        "Show Persons",
                        "Show Registered Products",
                        "Quit"));
        }

        public bool GetConfirm()
        {
            return AnsiConsole.Confirm("Repeat operation?");
        }

        public uint GetShopId(ShopManager shopManager)
        {
            string shopInfo = AnsiConsole.Prompt(new SelectionPrompt<string>().Title($"Choose [green]shop[/]!")
                .PageSize(10)
                .MoreChoicesText($"[grey](Move up and down to reveal more shops)[/]")
                .AddChoices(shopManager.Shops.Select(arg => arg.Id + " " + arg.Name)));
            return Convert.ToUInt32(shopInfo.Split(' ')[0]);
        }

        public uint GetProductId(ShopManager shopManager)
        {
            string productInfo = AnsiConsole.Prompt(new SelectionPrompt<string>().Title($"Choose [green]product[/]!")
                .PageSize(10)
                .MoreChoicesText($"[grey](Move up and down to reveal more products)[/]")
                .AddChoices(shopManager.Products.Select(arg => arg.Id + " " + arg.Name)));
            return Convert.ToUInt32(productInfo.Split(' ')[0]);
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