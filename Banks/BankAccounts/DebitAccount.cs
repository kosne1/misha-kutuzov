using System;
using Banks.Tools;
using Banks.Transactions;

namespace Banks.BankAccounts
{
    public class DebitAccount : BankAccount
    {
        public DebitAccount(int id, double money, DateTime closingTime)
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
                    $"You can't withdraw this amount of money {money}, because account {Id} is suspicious");
            }

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

            Money -= money;
            InterestCash += Money * InterestOnTheBalance;
            AddTransaction(new TransferTransaction(money, this, newBankAccount, curTime));
        }
    }
}