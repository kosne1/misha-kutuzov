using System;
using Banks.BankAccounts;

namespace Banks.Transactions
{
    public class WithdrawTransaction : ITransaction
    {
        private readonly double _money;
        private readonly BankAccount _bankAccount;
        private readonly DateTime _transactionTime;

        public WithdrawTransaction(double money, BankAccount bankAccount, DateTime transactionTime)
        {
            _money = money;
            _bankAccount = bankAccount;
            _transactionTime = transactionTime;
        }

        public void Cancel()
        {
            _bankAccount.AddMoneyFromTransactionCancellation(_money);
        }
    }
}