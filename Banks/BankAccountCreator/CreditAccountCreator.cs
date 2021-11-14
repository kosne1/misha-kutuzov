using System;
using Banks.BankAccounts;

namespace Banks.BankAccountCreator
{
    public class CreditAccountCreator : IAccountCreator
    {
        public BankAccount CreateAccount(double money, DateTime accountOpeningTime, DateTime accountClosingTime)
        {
            return new CreditBankAccount(money, accountOpeningTime, accountClosingTime);
        }
    }
}