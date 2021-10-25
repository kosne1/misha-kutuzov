using System.Collections.Generic;
using Banks.BankAccounts;

namespace Banks.Entities
{
    public class Bank
    {
        private Dictionary<Client, BankAccount> _clients;

        public Bank()
        {
            _clients = new Dictionary<Client, BankAccount>();
        }

        public double Percent { get; }
        public double Commission { get; }
    }
}