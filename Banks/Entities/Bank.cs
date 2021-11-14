using System.Collections.Generic;
using System.Linq;
using Banks.BankAccounts;
using Banks.ClientBuilder;
using Banks.Notification;
using Banks.UI.Services;

namespace Banks.Entities
{
    public class Bank : ISubject
    {
        private List<IObserver> _observers = new();
        private IOutput _output;
        private Dictionary<Client, List<BankAccount>> _clients = new();
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

        public void RemoveBankAccount(Client client, BankAccount bankAccount)
        {
            _clients[client].Remove(bankAccount);
        }

        public void RemoveClient(Client client)
        {
            _clients.Remove(client);
        }

        public void ChangeMoneyLimit(double moneyLimit)
        {
            _output.Notify();
            _moneyLimitForSuspiciousAccounts = moneyLimit;

            foreach (IObserver observer in _observers)
            {
                observer.Update(this);
            }
        }

        public void SetDepositPercents(DepositAccount depositAccount, Dictionary<double, double> percents)
        {
            depositAccount.SetPercents(percents);
        }

        public void SetMoneyLimits(BankAccount bankAccount)
        {
            bankAccount.ChangeMoneyLimit(_moneyLimitForSuspiciousAccounts);
        }

        public void DeductBankCommission()
        {
            foreach (BankAccount bankAccount in _clients.Values.SelectMany(bankAccounts => bankAccounts))
            {
                bankAccount.DeductBankCommission(Commission);
            }
        }

        public void ChargeInterest()
        {
            foreach (BankAccount bankAccount in _clients.Values.SelectMany(bankAccounts => bankAccounts))
            {
                bankAccount.ChargeInterest();
            }
        }

        public void SetOutput(IOutput output)
        {
            _output = output;
        }

        public void Attach(IObserver observer)
        {
            _output.Attach();
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _output.Detach();
            _observers.Remove(observer);
        }

        public void Notify()
        {
            _output.Notify();
            foreach (IObserver observer in _observers)
            {
                observer.Update(this);
            }
        }
    }
}