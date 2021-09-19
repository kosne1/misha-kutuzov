using Shops.Tools;

namespace Shops.Entities
{
    public class Person
    {
        public Person(string name, uint balance)
        {
            if (!IsNameValid(name)) throw new ShopException("Can't create person with empty or null name");
            Balance = balance;
            Name = name;
        }

        public string Name { get; }
        public uint Balance { get; private set; }

        public void Buy(uint cost)
        {
            if (!CanAfford(cost))
                throw new ShopException($"{Name} can't afford to buy products for {cost}. His balance is {Balance}");
            Balance -= cost;
        }

        private bool CanAfford(uint cost)
        {
            return Balance >= cost;
        }

        private bool IsNameValid(string name)
        {
            return !string.IsNullOrEmpty(name);
        }
    }
}