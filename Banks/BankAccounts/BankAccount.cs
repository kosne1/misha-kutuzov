using System;
using System.Collections.Generic;
using Banks.ClientBuilder;
using Banks.Transactions;
using Banks.UI.Services;

namespace Banks.BankAccounts
{
    public abstract class BankAccount
    {
        private List<ITransaction> _transactions = new List<ITransaction>();

        public BankAccount(int id, double money, DateTime closingTime)
        {
            Id = id;
            Money = money;
            ClosingTime = closingTime;
        }

        public bool IsSuspicious { get; private set; }
        public double Money { get; protected set; }
        public DateTime ClosingTime { get; }
        public IReadOnlyCollection<ITransaction> Transactions => _transactions;
        public double MoneyLimit { get; private set; }
        public int Id { get; }
        public double InterestOnTheBalance { get; protected set; }
        public double InterestCash { get; protected set; }

        // transactions
        public abstract void TopUpMoney(double money, DateTime curTime);

        public abstract void WithdrawMoney(double money, DateTime curTime);
        public abstract void TransferMoney(double money, BankAccount newBankAccount, DateTime curTime);

        public void AddTransaction(ITransaction transaction)
        {
            _transactions.Add(transaction);
        }

        public void RemoveMoneyFromTransactionCancellation(double money)
        {
            Money -= money;
        }

        public void AddMoneyFromTransactionCancellation(double money)
        {
            Money += money;
        }

        public void CheckForSuspicion(Client client)
        {
            IsSuspicious = client.IsSuspicious();
        }

        public void ChangeMoneyLimit(double moneyLimit)
        {
            MoneyLimit = moneyLimit;
        }

        public void DeductBankCommission(double commission)
        {
            Money -= commission;
        }

        public void ChargeInterest()
        {
            Money += InterestCash;
        }
    }
}