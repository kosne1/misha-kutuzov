using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Entities;
using Banks.Tools;

namespace Banks.BankAccounts
{
    public delegate void CreditLimitChangedHandler(BankAccount bankAccount, double creditLimit);

    public abstract class BankAccount
    {
        protected BankAccount(
            double money,
            DateTime accountOpeningTime,
            DateTime accountClosingTime,
            double commission = 0)
        {
            if (!IsMoneyValid(money)) throw new BankException("Money on bank account can't be negative");
            Money = money;
            AccountOpeningTime = accountOpeningTime;
            AccountClosingTime = accountClosingTime;
            Commission = commission;
            Transactions = new List<Transaction>();
        }

        public event CreditLimitChangedHandler CreditLimitChanged;
        public bool Suspicious { get; set; }
        public Dictionary<int, double> Percents { get; set; }
        protected double Money { get; set; }
        protected List<Transaction> Transactions { get; }
        protected DateTime AccountClosingTime { get; }
        private double CreditLimit { get; set; }
        private double Commission { get; }
        private double InterestOnTheBalance { get; set; }
        private DateTime AccountOpeningTime { get; }

        public abstract void AddMoney(double newMoney);

        public abstract void WithdrawMoney(double withdrawMoney, DateTime currentTime);

        public abstract void TransferMoney(double transferMoney, BankAccount bankAccount, DateTime currentTime);

        public void ChargeAccountBalance(double interestOnTheBalancePercent, DateTime currentTime)
        {
            TimeSpan interval = currentTime - AccountOpeningTime;
            int months = interval.Days / 28;
            Money += months * interestOnTheBalancePercent * Money;
            interval -= TimeSpan.FromDays(28 * months);
            InterestOnTheBalance = interval.Days * Money * interestOnTheBalancePercent;
        }

        public void DeductCommission(DateTime currentTime)
        {
            TimeSpan interval = currentTime - AccountOpeningTime;
            int months = interval.Days / 28;
            Money -= months * Commission;
        }

        public void CancelTransaction()
        {
            Transaction transaction = Transactions.Last();
            transaction.Cancel();
            Transactions.Remove(transaction);
        }

        public void ChangeCreditLimit(double limit)
        {
            CreditLimit = limit;
            CreditLimitChanged?.Invoke(this, limit);
        }

        public void RemoveMoney(double money)
        {
            Money -= money;
        }

        protected bool IsMoneyValid(double money)
        {
            return money >= 0;
        }
    }
}