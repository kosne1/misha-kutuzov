using Microsoft.AspNetCore.Mvc;
using Reports.DAL.Models.Employees;
using Reports.Server.Services;

namespace Reports.Server.Controllers;

[ApiController]
[Route("/supervisors")]
public class SupervisorController : EmployeeController
{
    private SupervisorService _service;
    
    public SupervisorController(SupervisorService service) : base(service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("subordinatesTasks")]
    public IActionResult GetAll(Guid id)
    {
        return Ok(_service.GetSubordinatesTasks(id));
    }
    
    [HttpPut]
    [Route("subordinate")]
    public EmployeeModel AddSubordinate([FromQuery] Guid supervisorId, [FromQuery] Guid employeeId)
    {
        return _service.AddSubordinate(supervisorId, employeeId);
    }
}