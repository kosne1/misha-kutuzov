using System;
using Banks.BankAccounts;

namespace Banks.BankAccountCreator
{
    public class DepositAccountCreator : AccountCreator
    {
        public override BankAccount FactoryMethod(
            double money,
            DateTime accountOpeningTime,
            DateTime accountClosingTime)
        {
            return new DepositBankAccount(money, accountOpeningTime, accountClosingTime);
        }
    }
}