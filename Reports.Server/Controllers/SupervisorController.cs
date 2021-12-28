using Microsoft.AspNetCore.Mvc;
using Reports.DAL.Models.Employees;
using Reports.Server.Services;

namespace Reports.Server.Controllers;

[ApiController]
[Route("/supervisors")]
public class SupervisorController : UserController
{
    private readonly SupervisorService _supervisorService;

    public SupervisorController(SupervisorService supervisorService) : base(supervisorService)
    {
        _supervisorService = supervisorService;
    }

    [HttpPost]
    public async Task<SupervisorModel> CreateSupervisor([FromQuery] string name)
    {
        return await _supervisorService.CreateSupervisor(name);
    }

    [HttpGet("subordinatesTasks")]
    public IActionResult GetSubordinatesTasks(Guid id)
    {
        return Ok(_supervisorService.GetSubordinatesTasks(id));
    }

    [HttpPut("subordinate")]
    public EmployeeModel AddSubordinate([FromQuery] Guid supervisorId, [FromQuery] Guid employeeId)
    {
        return _supervisorService.AddSubordinate(supervisorId, employeeId);
    }
}