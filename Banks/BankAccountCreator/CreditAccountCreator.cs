using System;
using Banks.BankAccounts;

namespace Banks.BankAccountCreator
{
    public class CreditAccountCreator : AccountCreator
    {
        public override BankAccount FactoryMethod(double money, DateTime accountOpeningTime, DateTime accountClosingTime)
        {
            return new CreditBankAccount(money, accountOpeningTime, accountClosingTime);
        }
    }
}