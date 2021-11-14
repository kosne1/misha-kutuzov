using System;
using Banks.BankAccounts;

namespace Banks.BankAccountCreator
{
    public interface IAccountCreator
    {
        public BankAccount CreateAccount(double money, DateTime accountOpeningTime, DateTime accountClosingTime);
    }
}