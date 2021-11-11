using Banks.BankAccounts;

namespace Banks.Entities
{
    public class Transaction
    {
        public Transaction(string ev, double money, BankAccount bankAccount, BankAccount newBankAccount = null)
        {
            Ev = ev;
            Money = money;
            BankAccount = bankAccount;
            NewBankAccount = newBankAccount;
        }

        public BankAccount BankAccount { get; }
        public BankAccount NewBankAccount { get; }
        public string Ev { get; }
        public double Money { get; }

        public void Cancel()
        {
            if (Ev == "w")
            {
                BankAccount.AddMoney(Money);
            }
            else
            {
                BankAccount.AddMoney(Money);
                NewBankAccount.RemoveMoney(Money);
            }
        }
    }
}