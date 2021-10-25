using Banks.Tools;

namespace Banks.BankAccounts
{
    public abstract class BankAccount
    {
        private readonly int _id;
        private double _money;

        protected BankAccount(double money, int id)
        {
            if (!IsMoneyValid(money)) throw new BankException("Money on bank account can't be negative");
            _money = money;
            _id = id;
        }

        public void AddMoney(double newMoney)
        {
            if (!IsMoneyValid(newMoney))
                throw new BankException("You can't add negative amount of money on bank account");
            _money += newMoney;
        }

        public void WithdrawMoney(double withdrawMoney)
        {
            if (!IsMoneyValid(withdrawMoney))
                throw new BankException("You can't withdraw negative amount of money from bank account");
            if (withdrawMoney > _money)
                throw new BankException($"You can't withdraw {withdrawMoney} from Bank {_id} with {_money} balance");
            _money -= withdrawMoney;
        }

        public void TransferMoney(double transferMoney)
        {
            if (!IsMoneyValid(transferMoney))
                throw new BankException("You can't add negative amount of money on bank account");
            _money -= transferMoney;
        }

        private bool IsMoneyValid(double money)
        {
            return money >= 0;
        }
    }
}