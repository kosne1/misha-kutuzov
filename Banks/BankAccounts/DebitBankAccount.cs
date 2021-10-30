namespace Banks.BankAccounts
{
    public class DebitBankAccount : BankAccount
    {
        public DebitBankAccount(double money, int id, double interestOnTheBalance)
            : base(money, id, interestOnTheBalance)
        {
        }
    }
}