using System;
using Banks.BankAccounts;

namespace Banks.BankAccountCreator
{
    public class DebitAccountCreator : IAccountCreator
    {
        public BankAccount CreateAccount(double money, DateTime accountOpeningTime, DateTime accountClosingTime)
        {
            return new DebitBankAccount(money, accountOpeningTime, accountClosingTime);
        }
    }
}