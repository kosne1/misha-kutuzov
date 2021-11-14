using Banks.BankAccounts;

namespace Banks.Transactions
{
    public class WithdrawTransaction : ITransaction
    {
        private readonly double _money;
        private readonly BankAccount _bankAccount;

        public WithdrawTransaction(double money, BankAccount bankAccount)
        {
            _money = money;
            _bankAccount = bankAccount;
        }

        public void Cancel()
        {
            _bankAccount.AddMoneyFromTransactionCancellation(_money);
        }
    }
}