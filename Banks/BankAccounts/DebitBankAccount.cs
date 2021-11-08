using System;
using Banks.Tools;

namespace Banks.BankAccounts
{
    public class DebitBankAccount : BankAccount
    {
        public DebitBankAccount(
            double money,
            DateTime accountOpeningTime,
            DateTime accountClosingTime,
            double interestOnTheBalancePercent)
            : base(money, accountOpeningTime, accountClosingTime, interestOnTheBalancePercent)
        {
        }

        public override void AddMoney(double newMoney)
        {
            if (!IsMoneyValid(newMoney))
                throw new BankException("You can't add negative amount of money on bank account");
            Money += newMoney;
        }

        public override void WithdrawMoney(double withdrawMoney, DateTime currentTime)
        {
            if (!IsMoneyValid(withdrawMoney))
                throw new BankException("You can't withdraw negative amount of money from bank account");
            if (withdrawMoney > Money)
                throw new BankException($"You can't withdraw {withdrawMoney} with {Money} balance");
            Money -= withdrawMoney;
        }

        public override void TransferMoney(double transferMoney, BankAccount bankAccount, DateTime currentTime)
        {
            if (!IsMoneyValid(transferMoney))
                throw new BankException("You can't add negative amount of money on bank account");
            if (transferMoney > Money)
                throw new BankException($"You can't transfer {transferMoney} with {Money} balance");
            Money -= transferMoney;
            bankAccount.AddMoney(transferMoney);
        }
    }
}