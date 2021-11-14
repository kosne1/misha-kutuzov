using System;
using System.Collections.Generic;
using Banks.ClientBuilder;
using Banks.Transactions;
using Banks.UI.Services;

namespace Banks.BankAccounts
{
    public abstract class BankAccount
    {
        protected bool _isSuspicious;
        protected double _money;
        protected DateTime _closingTime;
        protected List<ITransaction> _transactions = new();
        protected double _moneyLimit;
        protected int _id;
        protected double _interestOnTheBalance;
        protected double _interestCash;

        public BankAccount(int id, double money, DateTime closingTime)
        {
            _id = id;
            _money = money;
            _closingTime = closingTime;
        }

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
            _money -= money;
        }

        public void AddMoneyFromTransactionCancellation(double money)
        {
            _money += money;
        }

        public void CheckForSuspicion(Client client)
        {
            _isSuspicious = client.IsSuspicious();
        }

        public void ChangeMoneyLimit(double moneyLimit)
        {
            _moneyLimit = moneyLimit;
        }

        public void DeductBankCommission(double commission)
        {
            _money -= commission;
        }

        public void ChargeInterest()
        {
            _money += _interestCash;
        }
    }
}