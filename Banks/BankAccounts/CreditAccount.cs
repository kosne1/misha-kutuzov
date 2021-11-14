using System;
using Banks.Tools;
using Banks.Transactions;

namespace Banks.BankAccounts
{
    public class CreditAccount : BankAccount
    {
        public CreditAccount(int id, double money, DateTime openingTime, DateTime closingTime)
            : base(id, money, openingTime, closingTime)
        {
        }

        public override void WithdrawMoney(double money, DateTime curTime)
        {
            if (money < 0) throw new BankException("You can't withdraw negative amount of money");
            if (_isSuspicious && money > _moneyLimit)
            {
                throw new BankException(
                    $"You can't withdraw this amount of money {money}, because account {_id} is suspicious");
            }

            _money -= money;
            AddTransaction(new WithdrawTransaction(money, this, curTime));
        }

        public override void TransferMoney(double money, BankAccount newBankAccount, DateTime curTime)
        {
            if (money < 0) throw new BankException("You can't transfer negative amount of money");
            if (_isSuspicious && money > _moneyLimit)
            {
                throw new BankException(
                    $"You can't withdraw this amount of money {money}, because account {_id} is suspicious");
            }

            _money -= money;
            AddTransaction(new WithdrawTransaction(money, this, curTime));
        }
    }
}