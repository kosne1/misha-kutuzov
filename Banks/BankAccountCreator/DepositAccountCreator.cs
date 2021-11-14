using System;
using Banks.BankAccounts;

namespace Banks.BankAccountCreator
{
    public class DepositAccountCreator : IAccountCreator
    {
        public BankAccount CreateAccount(double money, DateTime accountOpeningTime, DateTime accountClosingTime)
        {
            return new DepositBankAccount(money, accountOpeningTime, accountClosingTime);
        }
    }
}