﻿using System;
using Banks.BankAccounts;

namespace Banks.BankAccountCreator
{
    public class DepositAccountCreator : IAccountCreator
    {
        public BankAccount CreateAccount(int id, double money, DateTime accountClosingTime)
        {
            return new DepositAccount(id, money, accountClosingTime);
        }
    }
}