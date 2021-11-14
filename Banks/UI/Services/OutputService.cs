using Banks.Tools;
using Spectre.Console;

namespace Banks.UI.Services
{
    public class OutputService : IOutput
    {
        public void PrintException(BankException e)
        {
            AnsiConsole.MarkupLine(e.Message);
        }

        public void Clear()
        {
            AnsiConsole.Clear();
        }

        public void Attach()
        {
            AnsiConsole.MarkupLine("Bank: Attached an observer.");
        }

        public void Detach()
        {
            AnsiConsole.MarkupLine("Bank: Detached an observer.");
        }

        public void Notify()
        {
            AnsiConsole.MarkupLine("Bank: Notifying observers ...");
        }

        public void ClientReact(string name)
        {
            AnsiConsole.MarkupLine($"Client {name}: Reacted to the event.");
        }

        private void PrintIfNothing(string value)
        {
            AnsiConsole.MarkupLine($"There are no {value} in system yet");
        }
    }
}