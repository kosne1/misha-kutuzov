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

        public Bank CreateBank()
        {
            var bank = new Bank();
            _banks.Add(bank);
            return bank;
        }

        public void ChargeCashBalance()
        {
            foreach (Bank bank in _banks)
            {
            }
        }

        public void ChargeCommission()
        {
            foreach (Bank bank in _banks)
            {
            }
        }
    }
}