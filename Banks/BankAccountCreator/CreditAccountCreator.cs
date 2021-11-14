using System;
using Banks.BankAccounts;

namespace Banks.BankAccountCreator
{
    public class CreditAccountCreator : IAccountCreator
    {
        public BankAccount CreateAccount(int id, double money, DateTime accountOpeningTime, DateTime accountClosingTime)
        {
            return new CreditAccount(id, money, accountOpeningTime, accountClosingTime);
        }
    }
}