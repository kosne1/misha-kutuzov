namespace Banks.BankAccounts
{
    public class DebitBankAccount : BankAccount
    {
        public DebitBankAccount(double percent, double commission)
            : base(percent, commission) { }
    }
}