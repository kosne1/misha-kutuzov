using System.Collections.Generic;

namespace Banks.Entities
{
    public class CentralBank
    {
        private readonly List<Bank> _banks = new List<Bank>();
        private int _bankCounter;

        public IReadOnlyCollection<Bank> Banks => _banks;

        public Bank CreateBank(string name, double commission, double moneyLimitForSuspiciousClients)
        {
            var bank = new Bank(name, _bankCounter++, commission, moneyLimitForSuspiciousClients);
            _banks.Add(bank);
            return bank;
        }

        public void DeductBankCommission()
        {
            foreach (Bank bank in _banks)
            {
                bank.DeductBankCommission();
            }
        }

        public void ChargeInterest()
        {
            foreach (Bank bank in _banks)
            {
                bank.ChargeInterest();
            }
        }
    }
}