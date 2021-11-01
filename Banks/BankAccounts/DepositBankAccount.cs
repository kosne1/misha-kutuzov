using System;
using Banks.Entities;
using Banks.Tools;

namespace Banks.BankAccounts
{
    public class DepositBankAccount : BankAccount
    {
        public DepositBankAccount(double money, int id, TimeSpan accountOpeningTime, TimeSpan accountClosingTime)
            : base(money, id, accountOpeningTime, accountClosingTime)
        {
        }

        public override void AddMoney(double newMoney)
        {
            if (!IsMoneyValid(newMoney))
                throw new BankException("You can't add negative amount of money on bank account");
            Money += newMoney;
        }

        public override void WithdrawMoney(double withdrawMoney, TimeSpan currentTime)
        {
            if (currentTime < AccountClosingTime)
                throw new BankException($"You can't withdraw money from {Id} account balance before it's closing time");
            if (!IsMoneyValid(withdrawMoney))
                throw new BankException("You can't withdraw negative amount of money from bank account");
            if (withdrawMoney > Money)
                throw new BankException($"You can't withdraw {withdrawMoney} from Bank {Id} with {Money} balance");
            Money -= withdrawMoney;
            Transactions.Add(new Transaction("w", withdrawMoney, this));
        }

        public override void TransferMoney(double transferMoney, BankAccount bankAccount, TimeSpan currentTime)
        {
            if (currentTime < AccountClosingTime)
                throw new BankException($"You can't transfer money from {Id} account balance before it's closing time");
            if (!IsMoneyValid(transferMoney))
                throw new BankException("You can't add negative amount of money on bank account");
            if (transferMoney > Money)
                throw new BankException($"You can't transfer {transferMoney} from Bank {Id} with {Money} balance");
            Money -= transferMoney;
            bankAccount.AddMoney(transferMoney);
            Transactions.Add(new Transaction("w", transferMoney, this, bankAccount));
        }
    }
}