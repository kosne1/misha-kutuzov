using System.Net;
using System.Text;
using Newtonsoft.Json;
using Reports.DAL.Models.Employees;

internal static class Program
{
    static readonly HttpClient client = new();

    internal static void Main(string[] args)
    {
        CreateEmployee("aboba");
        FindEmployeeById("572939d3-2415-49a3-8aa0-00f95b4f95b3");
        FindEmployeeByName("aboba");
        FindEmployeeByName("kek");
    }

    private static async void CreateEmployee(string name)
    {
        // Запрос к серверу
        var response =
            await client.PostAsync($"https://localhost:7124/employees/?name={name}", new StringContent(name));

        // Чтение ответа
        var responseStream = await response.Content.ReadAsStreamAsync();
        using var readStream = new StreamReader(responseStream, Encoding.UTF8);
        var responseString = await readStream.ReadToEndAsync();

        // Десериализация (перевод JSON'a к C# классу)
        var employee = JsonConvert.DeserializeObject<EmployeeModel>(responseString);

        if (employee != null)
        {
            Console.WriteLine("Created employee:");
            Console.WriteLine($"Id: {employee.Id}");
            Console.WriteLine($"Name: {employee.Name}");
        }
        else
        {
            Console.WriteLine("Couldn't create employee");
        }
    }

    private static async void FindEmployeeById(string id)
    {
        // Запрос к серверу
        var request = await client.GetAsync($"https://localhost:7124/employees/?id={id}");

        try
        {
            // Чтение ответа
            var responseStream = await request.Content.ReadAsStreamAsync();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
            var responseString = await readStream.ReadToEndAsync();

            // Десериализация (перевод JSON'a к C# классу)
            var employee = JsonConvert.DeserializeObject<EmployeeModel>(responseString);

            Console.WriteLine("Found employee by id:");
            Console.WriteLine($"Id: {employee.Id}");
            Console.WriteLine($"Name: {employee.Name}");
        }
        catch (WebException e)
        {
            Console.WriteLine("Employee was not found");
            await Console.Error.WriteLineAsync(e.Message);
        }
    }

    private static async void FindEmployeeByName(string name)
    {
        // Запрос к серверу
        var request = await client.GetAsync($"https://localhost:7124/employees/?name={name}");
        try
        {
            // Чтение ответа
            var responseStream = await request.Content.ReadAsStreamAsync();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
            var responseString = await readStream.ReadToEndAsync();

            // Десериализация (перевод JSON'a к C# классу)
            var employee = JsonConvert.DeserializeObject<EmployeeModel>(responseString);

            Console.WriteLine("Found employee by name:");
            Console.WriteLine($"Id: {employee.Id}");
            Console.WriteLine($"Name: {employee.Name}");
        }
        catch (WebException e)
        {
            Console.WriteLine("Employee was not found");
            await Console.Error.WriteLineAsync(e.Message);
        }
    }
}