using System.Collections.Generic;
using Shops.Tools;

namespace Shops.Entities
{
    public class Shop
    {
        private readonly string _name;
        private int _id;
        private string _address;

        public Shop(string name, int id, string address)
        {
            _name = name;
            _id = id;
            _address = address;
            Products = new List<Product>();
        }

        public List<Product> Products { get; }

        public void AddProducts(Product[] products, int[] amountsToAdd, int[] prices)
        {
            for (int i = 0; i < products.Length; i++)
            {
                Product found = FindProduct(products[i]);
                if (found != null)
                {
                    found.Amount += amountsToAdd[i];
                    found.Price = prices[i];
                }
                else
                {
                    Products.Add(new Product(products[i].Name, amountsToAdd[i], prices[i]));
                }
            }
        }

        public void Buy(Person person, Product product, int amountToBuy)
        {
            Product found = FindProduct(product);
            if (found != null)
            {
                if (amountToBuy > found.Amount)
                {
                    throw new ShopException($"In shop {_name} there is not enough {product.Name} for {person.Name}");
                }
                else
                {
                    int totalCost = amountToBuy * found.Price;
                    if (person.CanBuyProduct(totalCost))
                    {
                        found.Amount -= amountToBuy;
                    }
                    else
                    {
                        throw new ShopException(
                            $"{person.Name} can't afford to buy {product.Name} due to lack of balance");
                    }
                }
            }
            else
            {
                throw new ShopException($"In shop {_name} there is no {product.Name} for {person.Name}");
            }
        }

        public void ChangePrice(Product product, int newPrice)
        {
            Product found = FindProduct(product);
            if (found != null)
            {
                if (newPrice < 0)
                {
                    throw new ShopException($"Can't setup price for {found.Name} below zero");
                }
                else
                {
                    found.Price = newPrice;
                }
            }
            else
            {
                throw new ShopException($"In shop {_name} there is no {product.Name}");
            }
        }

        public Product GetProductInfo(Product product)
        {
            return FindProduct(product);
        }

        private Product FindProduct(Product product)
        {
            return Products.Find(found => found.Name == product.Name);
        }
    }
}