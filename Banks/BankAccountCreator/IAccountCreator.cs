using System;
using Banks.BankAccounts;

namespace Banks.BankAccountCreator
{
    public interface IAccountCreator
    {
        public BankAccount CreateAccount(int id, double money, DateTime accountOpeningTime, DateTime accountClosingTime);
    }
}