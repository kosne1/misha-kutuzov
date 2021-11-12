using System;
using Banks.BankAccounts;

namespace Banks.BankAccountCreator
{
    public class DebitAccountCreator : AccountCreator
    {
        public override BankAccount FactoryMethod(
            double money,
            DateTime accountOpeningTime,
            DateTime accountClosingTime)
        {
            return new DebitBankAccount(money, accountOpeningTime, accountClosingTime);
        }
    }
}