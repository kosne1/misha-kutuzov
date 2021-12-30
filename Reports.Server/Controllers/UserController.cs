using Microsoft.AspNetCore.Mvc;
using Reports.DAL.Models.Employees;
using Reports.Server.Services;

namespace Reports.Server.Controllers;

[ApiController]
[Route("/users")]
public class UserController : ControllerBase
{
    protected readonly IEmployeeService _service;

    public UserController(IEmployeeService service)
    {
        _service = service;
    }

    [HttpGet("all")]
    public IActionResult GetAll()
    {
        return Ok(_service.GetAll());
    }


    [HttpGet("name")]
    public IActionResult FindByName([FromQuery] string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return BadRequest();
        EmployeeModel result = _service.FindByName(name);
        if (result != null)
        {
            return Ok(result);
        }

        return NotFound();
    }

    [HttpGet("id")]
    public IActionResult FindById([FromQuery] Guid id)
    {
        if (id == Guid.Empty) return BadRequest();
        EmployeeModel result = _service.FindById(id);
        if (result != null)
        {
            return Ok(result);
        }

        return NotFound();
    }

    [HttpPut]
    public EmployeeModel Update([FromQuery] EmployeeModel entity)
    {
        return _service.Update(entity);
    }
    
    [HttpDelete]
    public void Delete([FromQuery] Guid id)
    {
        _service.Delete(id);
    }
}