using System;
using Banks.Tools;
using Banks.Transactions;

namespace Banks.BankAccounts
{
    public class CreditAccount : BankAccount
    {
        private double _minusCommission;
        private double _creditLimit;

        public CreditAccount(int id, double money, DateTime closingTime)
            : base(id, money, closingTime)
        {
        }

        public override void TopUpMoney(double money, DateTime curTime)
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

        public override void WithdrawMoney(double money, DateTime curTime)
        {
            if (money < 0) throw new BankException("You can't withdraw negative amount of money");
            if (_isSuspicious && money > _moneyLimit)
            {
                throw new BankException(
                    $"You can't withdraw this amount of money {money}, because account {_id} is suspicious");
            }

            if (_money < _creditLimit)
                _money -= _minusCommission;

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

            if (_money < _creditLimit)
                _money -= _minusCommission;

            _money -= money;
            AddTransaction(new WithdrawTransaction(money, this, curTime));
        }

        public void SetCreditLimit(double creditLimit)
        {
            _creditLimit = creditLimit;
        }

        public void SetMinusCommission(double minusCommission)
        {
            if (minusCommission < 0) throw new BankException("You can't set commission negative");
            _minusCommission = minusCommission;
        }
    }
}