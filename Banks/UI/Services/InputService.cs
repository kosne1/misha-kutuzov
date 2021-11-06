using Spectre.Console;

namespace Banks.UI.Services
{
    public class InputService
    {
        public string GetString(string value)
        {
            return AnsiConsole.Ask<string>($"What's [green]{value}[/]?");
        }

        public int GetInt(string value)
        {
            return AnsiConsole.Ask<int>($"What's [green]{value}[/]?");
        }

        public double GetDouble(string value)
        {
            return AnsiConsole.Ask<double>($"What's [green]{value}[/]?");
        }

        public bool GetConfirm()
        {
            return AnsiConsole.Confirm("Repeat operation?");
        }

        public string GetAction()
        {
            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[green]Bank manager[/] v3.0")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more Actions)[/]")
                    .AddChoices(
                        "Add Client",
                        "Create Bank",
                        "Register Bank Account",
                        "Quit"));
        }

        public string GetBankAccountType()
        {
            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more Actions)[/]")
                    .AddChoices(
                        "Credit",
                        "Debit",
                        "Deposit"));
        }
    }
}