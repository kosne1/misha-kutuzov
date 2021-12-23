using System.Net;
using System.Text;
using Newtonsoft.Json;
using Reports.DAL.Models;
using Reports.DAL.Models.Employees;

internal static class Program
{
    internal static void Main(string[] args)
    {
        CreateEmployee("aboba");
        FindEmployeeById("572939d3-2415-49a3-8aa0-00f95b4f95b3");
        FindEmployeeByName("aboba");
        FindEmployeeByName("kek");
    }

    private static void CreateEmployee(string name)
    {
        // Запрос к серверу
        var request = HttpWebRequest.Create($"https://localhost:7124/employees/?name={name}");
        request.Method = WebRequestMethods.Http.Post;
        var response = request.GetResponse();

        // Чтение ответа
        var responseStream = response.GetResponseStream();
        using var readStream = new StreamReader(responseStream, Encoding.UTF8);
        var responseString = readStream.ReadToEnd();

        // Десериализация (перевод JSON'a к C# классу)
        var employee = JsonConvert.DeserializeObject<EmployeeModel>(responseString);

        Console.WriteLine("Created employee:");
        Console.WriteLine($"Id: {employee.Id}");
        Console.WriteLine($"Name: {employee.Name}");
    }

    private static void FindEmployeeById(string id)
    {
        // Запрос к серверу
        var request = HttpWebRequest.Create($"https://localhost:7124/employees/?id={id}");
        request.Method = WebRequestMethods.Http.Get;

        try
        {
            var response = request.GetResponse();

            // Чтение ответа
            var responseStream = response.GetResponseStream();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
            var responseString = readStream.ReadToEnd();

            // Десериализация (перевод JSON'a к C# классу)
            var employee = JsonConvert.DeserializeObject<EmployeeModel>(responseString);

            Console.WriteLine("Found employee by id:");
            Console.WriteLine($"Id: {employee.Id}");
            Console.WriteLine($"Name: {employee.Name}");
        }
        catch (WebException e)
        {
            Console.WriteLine("Employee was not found");
            Console.Error.WriteLine(e.Message);
        }
    }

    private static void FindEmployeeByName(string name)
    {
        // Запрос к серверу
        var request = HttpWebRequest.Create($"https://localhost:7124/employees/?name={name}");
        request.Method = WebRequestMethods.Http.Get;
        try
        {
            var response = request.GetResponse();

            // Чтение ответа
            var responseStream = response.GetResponseStream();
            using var readStream = new StreamReader(responseStream, Encoding.UTF8);
            var responseString = readStream.ReadToEnd();

            // Десериализация (перевод JSON'a к C# классу)
            var employee = JsonConvert.DeserializeObject<EmployeeModel>(responseString);

            Console.WriteLine("Found employee by name:");
            Console.WriteLine($"Id: {employee.Id}");
            Console.WriteLine($"Name: {employee.Name}");
        }
        catch (WebException e)
        {
            Console.WriteLine("Employee was not found");
            Console.Error.WriteLine(e.Message);
        }
    }
}