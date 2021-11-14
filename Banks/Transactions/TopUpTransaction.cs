using Banks.BankAccounts;

namespace Banks.Transactions
{
    public class TopUpTransaction : ITransaction
    {
        private readonly double _money;
        private readonly BankAccount _bankAccount;

        public TopUpTransaction(double money, BankAccount bankAccount)
        {
            _money = money;
            _bankAccount = bankAccount;
        }

        public void Cancel()
        {
            _bankAccount.RemoveMoneyFromTransactionCancellation(_money);
        }
    }
}