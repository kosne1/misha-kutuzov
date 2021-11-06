using System.Collections.ObjectModel;
using Banks.Tools;
using Spectre.Console;

namespace Banks.UI.Services
{
    public class OutputService
    {
        public void PrintException(BankException e)
        {
            AnsiConsole.MarkupLine(e.Message);
        }

        public void Clear()
        {
            AnsiConsole.Clear();
        }

        private void PrintIfNothing(string value)
        {
            AnsiConsole.MarkupLine($"There are no {value} in system yet");
        }
    }
}