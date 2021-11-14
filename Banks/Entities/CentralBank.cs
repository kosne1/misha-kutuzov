using System;
using System.Collections.Generic;

namespace Banks.Entities
{
    public class CentralBank
    {
        private readonly List<Bank> _banks;

        public CentralBank()
        {
            _banks = new List<Bank>();
        }

        public IReadOnlyCollection<Bank> Banks => _banks;
        public int BankCounter { get; private set; } = 0;

        public Bank CreateBank(string name, double percent, double commission, double moneyLimitForSuspiciousClients)
        {
            var bank = new Bank(name, BankCounter++, percent, commission, moneyLimitForSuspiciousClients);
            _banks.Add(bank);
            return bank;
        }

        public void ChargeAccountBalance(DateTime timeSpan)
        {
            foreach (Bank bank in _banks)
            {
                bank.ChargeAccountBalance(timeSpan);
            }
        }

        public void DeductCommission()
        {
            foreach (Bank bank in _banks)
            {
                bank.DeductCommission(DateTime.Now);
            }
        }
    }
}