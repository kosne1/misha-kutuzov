using System;
using System.Collections.Generic;
using Banks.BankAccounts;
using Banks.ClientBuilder;

namespace Banks.Entities
{
    public class Bank
    {
        private readonly Dictionary<Client, BankAccount> _clients;

        public Bank(int id, double percent, double commission, double moneyLimitForSuspiciousClients)
        {
            Id = id;
            Percent = percent;
            Commission = commission;
            MoneyLimitForSuspiciousClients = moneyLimitForSuspiciousClients;
            _clients = new Dictionary<Client, BankAccount>();
        }

        public IReadOnlyDictionary<Client, BankAccount> Clients => _clients;
        public int Id { get; }

        public double Percent { get; }
        public double Commission { get; }
        public double MoneyLimitForSuspiciousClients { get; }

        public void AddBankAccount(Client client, BankAccount bankAccount)
        {
            bankAccount.Suspicious = client.IsSuspicious();
            _clients.Add(client, bankAccount);
        }

        public void SetPercentsForBankAccount(BankAccount bankAccount, Dictionary<int, double> percents)
        {
            bankAccount.Percents = percents;
        }

        public void ChargeAccountBalance(DateTime timeSpan)
        {
            foreach (BankAccount bankAccount in _clients.Values)
            {
                bankAccount.ChargeAccountBalance(timeSpan);
            }
        }

        public void DeductCommission(DateTime timeSpan)
        {
            foreach (BankAccount bankAccount in _clients.Values)
            {
                bankAccount.DeductCommission(timeSpan);
            }
        }
    }
}