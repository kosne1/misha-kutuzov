﻿using System;
using Banks.BankAccounts;

namespace Banks.BankAccountCreator
{
    public class DebitAccountCreator : IAccountCreator
    {
        public BankAccount CreateAccount(int id, double money, DateTime accountClosingTime)
        {
            return new DebitAccount(id, money, accountClosingTime);
        }
    }
}