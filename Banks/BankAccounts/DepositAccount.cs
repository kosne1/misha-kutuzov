using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Tools;
using Banks.Transactions;

namespace Banks.BankAccounts
{
    public class DepositAccount : BankAccount
    {
        private Dictionary<double, double> _percents = new Dictionary<double, double>();

        public DepositAccount(int id, double money, DateTime closingTime)
            : base(id, money, closingTime)
        {
        }

        public override void TopUpMoney(double money, DateTime curTime)
        {
            if (money < 0) throw new BankException("You can't top up negative amount of money");
            if (IsSuspicious && money > MoneyLimit)
            {
                throw new BankException(
                    $"You can't top up this amount of money {money}, because account {Id} is suspicious");
            }

            Money += money;
            InterestCash += Money * InterestOnTheBalance;
            AddTransaction(new TopUpTransaction(money, this, curTime));
        }

        public override void WithdrawMoney(double money, DateTime curTime)
        {
            if (money < 0) throw new BankException("You can't withdraw negative amount of money");
            if (money > Money) throw new BankException("You can't withdraw money, because you are low on balance");
            if (IsSuspicious && money > MoneyLimit)
            {
                throw new BankException(
                    $"You can't withdraw this amount of money, because account {Id} is suspicious");
            }

            if (curTime < ClosingTime)
                throw new BankException($"You can't withdraw money from {Id}, because it is not closed yet");
            Money -= money;
            InterestCash += Money * InterestOnTheBalance;
            AddTransaction(new WithdrawTransaction(money, this, curTime));
        }

        public override void TransferMoney(double money, BankAccount newBankAccount, DateTime curTime)
        {
            if (money < 0) throw new BankException("You can't transfer negative amount of money");
            if (money > Money) throw new BankException("You can't transfer money, because you are low on balance");
            if (IsSuspicious && money > MoneyLimit)
            {
                throw new BankException(
                    $"You can't transfer this amount of money {money}, because account {Id} is suspicious");
            }

            if (curTime < ClosingTime)
                throw new BankException($"You can't transfer money from {Id}, because it is not closed yet");
            Money -= money;
            InterestCash += Money * InterestOnTheBalance;
            AddTransaction(new TransferTransaction(money, this, newBankAccount, curTime));
        }

        public void SetPercents(Dictionary<double, double> percents)
        {
            _percents = percents;
            foreach (double moneyBorder in percents.Keys.Where(moneyBorder => Money < moneyBorder))
            {
                InterestOnTheBalance = percents[moneyBorder];
            }
        }
    }
}