using System;
using Banks.BankAccounts;

namespace Banks.Transactions
{
    public class TopUpTransaction : ITransaction
    {
        private readonly double _money;
        private readonly BankAccount _bankAccount;
        private readonly DateTime _transactionTime;

        public TopUpTransaction(double money, BankAccount bankAccount, DateTime transactionTime)
        {
            _money = money;
            _bankAccount = bankAccount;
            _transactionTime = transactionTime;
        }

        public void Cancel()
        {
            _bankAccount.RemoveMoneyFromTransactionCancellation(_money);
        }
    }
}