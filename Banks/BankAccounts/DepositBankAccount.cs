namespace Banks.BankAccounts
{
    public class DepositBankAccount : BankAccount
    {
        public DepositBankAccount(double money, int id)
            : base(money, id)
        {
            InterestOnTheBalance = money switch
            {
                < 50000 => 0.03,
                < 100000 => 0.035,
                _ => 0.04
            };
        }
    }
}