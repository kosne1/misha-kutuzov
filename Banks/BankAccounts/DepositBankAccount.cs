namespace Banks.BankAccounts
{
    public class DepositBankAccount : BankAccount
    {
        public DepositBankAccount(double percent, double commission)
            : base(percent, commission)
        {
        }
    }
}