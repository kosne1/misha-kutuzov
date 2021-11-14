using System;
using System.Collections.Generic;
using System.Linq;
using Banks.BankAccountCreator;
using Banks.BankAccounts;
using Banks.ClientBuilder;
using Banks.Db;
using Banks.Entities;
using Banks.UI.Services;

namespace Banks.UI.Entities
{
    public class ConsoleService
    {
        private readonly InputService _inputService;
        private int _clientsCounter;

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
                    CreateBankAccount(centralBank, clients);
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
            return new Client(_clientsCounter++, name, address, passport);
        }

        private Bank GetBank(CentralBank centralBank)
        {
            string name = _inputService.GetString("Name");
            double percent = _inputService.GetDouble("percent");
            double commission = _inputService.GetDouble("Commission");
            double moneyLimit = _inputService.GetDouble("Money Limit For Suspicious Operations");
            return centralBank.CreateBank(name, percent, commission, moneyLimit);
        }

        private void CreateBankAccount(CentralBank centralBank, List<Client> clients)
        {
            int bankId = _inputService.GetBankId(centralBank);
            Bank bank = centralBank.Banks.FirstOrDefault(b => b.Id == bankId);
            string name = _inputService.GetBankAccountType();
            double money = _inputService.GetDouble("Money");
            BankAccount bankAccount = null;
            switch (name)
            {
                case "Credit":
                    var creditAccountCreator = new CreditAccountCreator();
                    bankAccount = creditAccountCreator.CreateAccount(money, DateTime.Now, DateTime.Now.AddYears(1));
                    break;
                case "Debit":
                    var debitAccountCreator = new DebitAccountCreator();
                    bankAccount = debitAccountCreator.CreateAccount(money, DateTime.Now, DateTime.Now.AddYears(1));
                    break;
                case "Deposit":
                    var depositAccountCreator = new DepositAccountCreator();
                    bankAccount = depositAccountCreator.CreateAccount(money, DateTime.Now, DateTime.Now.AddYears(1));
                    break;
            }

            int clientId = _inputService.GetClientId(clients);
            Client client = clients.FirstOrDefault(c => c.Id == clientId);
            bank?.AddBankAccount(client, bankAccount);
        }
    }
}