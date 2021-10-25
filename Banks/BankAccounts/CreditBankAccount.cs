namespace Banks.BankAccounts
{
    public class CreditBankAccount : BankAccount
    {
        public CreditBankAccount(double percent, double commission)
            : base(percent, commission)
        {
        }
    }
}