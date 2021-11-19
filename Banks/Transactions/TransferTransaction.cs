using System;
using Banks.BankAccounts;

namespace Banks.Transactions
{
    public class TransferTransaction : ITransaction
    {
        private readonly double _money;
        private readonly BankAccount _bankAccount;
        private readonly BankAccount _newBankAccount;
        private readonly DateTime _transactionTime;

        public TransferTransaction(double money, BankAccount bankAccount, BankAccount newBankAccount, DateTime transactionTime)
        {
            _money = money;
            _bankAccount = bankAccount;
            _newBankAccount = newBankAccount;
            _transactionTime = transactionTime;
        }

        public void Cancel()
        {
            _bankAccount.AddMoneyFromTransactionCancellation(_money);
            _newBankAccount.RemoveMoneyFromTransactionCancellation(_money);
        }
    }
}