using Microsoft.AspNetCore.Mvc;
using Reports.DAL.Models.Employees;
using Reports.Server.Services;

namespace Reports.Server.Controllers;

[ApiController]
[Route("/employees")]
public class EmployeeController : UserController
{
    public EmployeeController(IEmployeeService service) : base(service)
    {
    }

    [HttpPost]
    public async Task<EmployeeModel> CreateEmployee([FromQuery] string name)
    {
        return await _service.Create(name);
    }
}