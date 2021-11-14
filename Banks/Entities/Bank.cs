using System.Collections.Generic;
using Banks.BankAccounts;
using Banks.ClientBuilder;

namespace Banks.Entities
{
    public class Bank
    {
        private readonly Dictionary<Client, List<BankAccount>> _clients = new();
        private double _moneyLimitForSuspiciousAccounts;

        public Bank(string name, int id, double commission, double moneyLimitForSuspiciousAccounts)
        {
            Name = name;
            Id = id;
            _moneyLimitForSuspiciousAccounts = moneyLimitForSuspiciousAccounts;
            Commission = commission;
        }

        public IReadOnlyDictionary<Client, List<BankAccount>> Clients => _clients;
        public int Id { get; }
        public string Name { get; }
        public double Commission { get; }

        public void AddBankAccount(Client client, BankAccount bankAccount)
        {
            if (_clients[client] == null)
            {
                _clients.Add(client, new List<BankAccount>());
            }

            _clients[client].Add(bankAccount);
        }

        public void SetDepositPercents(DepositAccount depositAccount, Dictionary<double, double> percents)
        {
            depositAccount.SetPercents(percents);
        }

        public void SetMoneyLimits(BankAccount bankAccount)
        {
            bankAccount.SetMoneyLimit(_moneyLimitForSuspiciousAccounts);
        }
    }
}