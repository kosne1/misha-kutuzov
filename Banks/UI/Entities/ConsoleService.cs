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
        private int _accountsCounter;

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
                    BankAccount bankAccount = CreateBankAccount(centralBank, clients);
                    db.BankAccounts.Add(bankAccount);
                    break;
                case "Quit":
                    return true;
            }

            db.SaveChanges();
            return false;
        }

        private Client GetClient()
        {
            var director = new Director();
            var builder = new Builder();
            string name = _inputService.GetString("Name");
            string address = _inputService.GetString("Address");
            string passport = _inputService.GetString("Passport");
            director.BuildFullFeaturedClient(name, address, passport);
            return builder.GetClient();
        }

        private Bank GetBank(CentralBank centralBank)
        {
            string name = _inputService.GetString("Name");
            double commission = _inputService.GetDouble("Commission");
            double moneyLimit = _inputService.GetDouble("Money Limit For Suspicious Operations");
            return centralBank.CreateBank(name, commission, moneyLimit);
        }

        private BankAccount CreateBankAccount(CentralBank centralBank, List<Client> clients)
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
                    bankAccount =
                        creditAccountCreator.CreateAccount(_accountsCounter++, money, DateTime.Now.AddYears(1));
                    break;
                case "Debit":
                    var debitAccountCreator = new DebitAccountCreator();
                    bankAccount =
                        debitAccountCreator.CreateAccount(_accountsCounter++, money, DateTime.Now.AddYears(1));
                    break;
                case "Deposit":
                    var depositAccountCreator = new DepositAccountCreator();
                    bankAccount =
                        depositAccountCreator.CreateAccount(_accountsCounter++, money, DateTime.Now.AddYears(1));
                    break;
            }

            int clientId = _inputService.GetClientId(clients);
            Client client = clients.FirstOrDefault(c => c.Id == clientId);
            bank?.AddBankAccount(client, bankAccount);
            return bankAccount;
        }
    }
}