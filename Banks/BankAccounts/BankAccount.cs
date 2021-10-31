using System;
using System.Collections.Generic;
using Banks.Tools;

namespace Banks.BankAccounts
{
    public abstract class BankAccount
    {
        protected BankAccount(
            double money,
            int id,
            TimeSpan accountOpeningTime,
            TimeSpan accountClosingTime,
            double interestOnTheBalancePercent = 0,
            double commission = 0)
        {
            if (!IsMoneyValid(money)) throw new BankException("Money on bank account can't be negative");
            Money = money;
            Id = id;
            AccountOpeningTime = accountOpeningTime;
            AccountClosingTime = accountClosingTime;
            InterestOnTheBalancePercent = interestOnTheBalancePercent;
            Commission = commission;
        }

        public int Id { get; }
        public double Money { get; protected set; }
        public double Commission { get; }
        public double InterestOnTheBalancePercent { get; protected set; }
        public double InterestOnTheBalance { get; protected set; }
        public bool Suspicious { get; set; }
        public TimeSpan AccountOpeningTime { get; }
        public TimeSpan AccountClosingTime { get; }
        public Dictionary<int, double> Percents { get; set; }

        public abstract void AddMoney(double newMoney);

        public abstract void WithdrawMoney(double withdrawMoney, TimeSpan currentTime);

        public abstract void TransferMoney(double transferMoney, BankAccount bankAccount, TimeSpan currentTime);

        public void ChargeAccountBalance(TimeSpan currentTime)
        {
            TimeSpan interval = currentTime - AccountOpeningTime;
            int months = interval.Days / 28;
            Money += months * InterestOnTheBalancePercent * Money;
            interval -= TimeSpan.FromDays(28 * months);
            InterestOnTheBalance = interval.Days * Money * InterestOnTheBalancePercent;
        }

        public void DeductCommission(TimeSpan currentTime)
        {
            TimeSpan interval = currentTime - AccountOpeningTime;
            int months = interval.Days / 28;
            Money -= months * Commission;
        }

        protected bool IsMoneyValid(double money)
        {
            return money >= 0;
        }
    }
}