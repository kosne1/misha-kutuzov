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

        public bool CanBuyProduct(int cost)
        {
            if (cost > Balance) return false;
            Buy(cost);
            return true;
        }

        private void Buy(int cost)
        {
            Balance -= cost;
        }

        private bool IsBalanceValid(int balance)
        {
            return balance >= 0;
        }
    }
}