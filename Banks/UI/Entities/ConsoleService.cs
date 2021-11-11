using System;
using System.Collections.Generic;
using Banks.BankAccounts;
using Banks.Db;
using Banks.Entities;
using Banks.UI.Services;

namespace Banks.UI.Entities
{
    public class ConsoleService
    {
        private readonly InputService _inputService;

        public ConsoleService()
        {
            _inputService = new InputService();
        }

        public void Run(ApplicationContext db, CentralBank centralBank, List<Client> clients)
        {
            while (true)
            {
                bool shouldQuit = MakeAction(db, centralBank, clients);
                if (shouldQuit)
                    return;
            }
        }

        private bool MakeAction(ApplicationContext db, CentralBank centralBank, List<Client> clients)
        {
            string action = _inputService.GetAction();
            switch (action)
            {
                case "Add Client":
                    Client client = GetClient();
                    db.Clients.Add(client);
                    break;
                case "Create Bank":
                    Bank bank = GetBank(centralBank);
                    db.Banks.Add(bank);
                    break;
                case "Register Bank Account":
                    BankAccount bankAccount = GetBankAccount();
                    break;
                case "Quit":
                    return true;
            }

            db.SaveChanges();
            return false;
        }

        private Client GetClient()
        {
            string name = _inputService.GetString("Name");
            string address = _inputService.GetString("Address");
            string passport = _inputService.GetString("Passport");
            return new Client(name, address, passport);
        }

        private Bank GetBank(CentralBank centralBank)
        {
            double percent = _inputService.GetDouble("percent");
            double commission = _inputService.GetDouble("Commission");
            double moneyLimit = _inputService.GetDouble("Money Limit For Suspicious Operations");
            return centralBank.CreateBank(percent, commission, moneyLimit);
        }

        private BankAccount GetBankAccount()
        {
            string name = _inputService.GetBankAccountType();
            double money = _inputService.GetDouble("Money");
            BankAccount bankAccount = null;
            switch (name)
            {
                case "Credit":
                    bankAccount = new CreditBankAccount(money, DateTime.Now, DateTime.Now.AddYears(1));
                    break;
                case "Debit":
                    double interest = _inputService.GetDouble("Interest on the Balance");
                    bankAccount = new DebitBankAccount(money, DateTime.Now, DateTime.Now.AddYears(1), interest);
                    break;
                case "Deposit":
                    bankAccount = new DepositBankAccount(money, DateTime.Now, DateTime.Now.AddYears(1));
                    break;
            }

            return bankAccount;
        }

        private int GetNewPrice()
        {
            int newPrice = _inputService.GetInt("New Price");
            return newPrice;
        }

        private int GetPrice()
        {
            int price = _inputService.GetInt("Price");
            return price;
        }

        private int GetAmount()
        {
            int amount = _inputService.GetInt("Amount");
            return amount;
        }
    }
}