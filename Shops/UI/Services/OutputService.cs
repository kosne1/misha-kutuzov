using System.Collections.ObjectModel;
using Shops.Entities;
using Shops.Models;
using Shops.Tools;
using Spectre.Console;

namespace Shops.UI.Services
{
    public class OutputService
    {
        public void PrintException(ShopException e)
        {
            AnsiConsole.MarkupLine(e.Message);
        }

        public void Clear()
        {
            AnsiConsole.Clear();
        }

        public void PrintShopInfo(Shop shop)
        {
            if (shop.Products.Count == 0)
            {
                PrintIfNothing("Shops");
            }

            foreach ((Product product, ProductProperties productProperties) in shop.Products)
            {
                AnsiConsole.Markup(product.Name);
                AnsiConsole.Markup(" Amount: " + productProperties.Amount);
                AnsiConsole.MarkupLine(" Price: " + productProperties.Price);
            }
        }

        public void PrintPersons(ReadOnlyCollection<Person> persons)
        {
            if (persons.Count == 0)
            {
                PrintIfNothing("Persons");
            }

            foreach (Person person in persons)
            {
                AnsiConsole.Markup(person.Name);
                AnsiConsole.MarkupLine(" Balance: " + person.Balance);
            }
        }

        public void PrintRegisteredProducts(ReadOnlyCollection<Product> products)
        {
            if (products.Count == 0)
            {
                PrintIfNothing("Products");
            }

            foreach (Product product in products)
            {
                AnsiConsole.MarkupLine(product.Name);
            }
        }

        private void PrintIfNothing(string value)
        {
            AnsiConsole.MarkupLine($"There are no {value} in system yet");
        }
    }
}