using System;
using Banks.BankAccounts;

namespace Banks.BankAccountCreator
{
    public abstract class AccountCreator
    {
        public abstract BankAccount FactoryMethod(double money, DateTime accountOpeningTime, DateTime accountClosingTime);

        public BankAccount CreateAccount(double money, DateTime accountOpeningTime, DateTime accountClosingTime)
        {
            BankAccount account = FactoryMethod(money, accountOpeningTime, accountClosingTime);

            return account;
        }
    }
}