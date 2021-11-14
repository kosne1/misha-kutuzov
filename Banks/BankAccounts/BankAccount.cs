using System;
using System.Collections.Generic;
using Banks.ClientBuilder;
using Banks.Tools;
using Banks.Transactions;

namespace Banks.BankAccounts
{
    public abstract class BankAccount
    {
        protected bool _isSuspicious;
        protected double _money;
        protected DateTime _openingTime;
        protected DateTime _closingTime;
        protected List<ITransaction> _transactions = new();
        protected double _moneyLimit;
        protected int _id;

        public BankAccount(int id, double money, DateTime openingTime, DateTime closingTime)
        {
            _id = id;
            _money = money;
            _openingTime = openingTime;
            _closingTime = closingTime;
        }

        // transactions
        public void TopUpMoney(double money, DateTime curTime)
        {
            if (money < 0) throw new BankException("You can't top up negative amount of money");
            if (_isSuspicious && money > _moneyLimit)
            {
                throw new BankException(
                    $"You can't top up this amount of money {money}, because account {_id} is suspicious");
            }

            _money += money;
            AddTransaction(new TopUpTransaction(money, this, curTime));
        }

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

        public void SetMoneyLimit(double moneyLimit)
        {
            _moneyLimit = moneyLimit;
        }
    }
}