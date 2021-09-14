using Shops.Tools;

namespace Shops.Entities
{
    public class Person
    {
        public Person(string name, int balance)
        {
            if (!IsBalanceValid(balance)) throw new ShopException($"Couldn't create {name} with negative balance");
            Balance = balance;
            Name = name;
        }

        public string Name { get; }
        public int Balance { get; private set; }

        public void Buy(int cost)
        {
            Balance -= cost;
        }

        public bool CanBuyProduct(int cost)
        {
            return Balance > cost;
        }

        private static bool IsBalanceValid(int balance)
        {
            return balance >= 0;
        }
    }
}