using Banks.Tools;

namespace Banks.BankAccounts
{
    public abstract class BankAccount
    {
        private readonly int _id;

        protected BankAccount(double money, int id, double interestOnTheBalance = 0, double commission = 0)
        {
            if (!IsMoneyValid(money)) throw new BankException("Money on bank account can't be negative");
            Money = money;
            _id = id;
            InterestOnTheBalance = interestOnTheBalance;
            Commission = commission;
        }

        public double Money { get; private set; }
        public double InterestOnTheBalance { get; protected set; }
        public double Commission { get; protected set; }

        public void AddMoney(double newMoney)
        {
            if (!IsMoneyValid(newMoney))
                throw new BankException("You can't add negative amount of money on bank account");
            Money += newMoney;
        }

        public void WithdrawMoney(double withdrawMoney)
        {
            if (!IsMoneyValid(withdrawMoney))
                throw new BankException("You can't withdraw negative amount of money from bank account");
            if (withdrawMoney > Money)
                throw new BankException($"You can't withdraw {withdrawMoney} from Bank {_id} with {Money} balance");
            Money -= withdrawMoney;
        }

        public void TransferMoney(double transferMoney, BankAccount bankAccount)
        {
            if (!IsMoneyValid(transferMoney))
                throw new BankException("You can't add negative amount of money on bank account");
            if (transferMoney > Money)
                throw new BankException($"You can't transfer {transferMoney} from Bank {_id} with {Money} balance");
            Money -= transferMoney;
            bankAccount.AddMoney(transferMoney);
        }

        private bool IsMoneyValid(double money)
        {
            return money >= 0;
        }
    }
}