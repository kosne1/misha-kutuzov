using System.Net;
using Microsoft.AspNetCore.Mvc;
using Reports.DAL.Models;
using Reports.DAL.Models.Employees;
using Reports.Server.Services;

namespace Reports.Server.Controllers;

[ApiController]
[Route("/employees")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _service;

    public EmployeeController(IEmployeeService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<EmployeeModel> Create([FromQuery] string name)
    {
        return await _service.Create(name);
    }

    [HttpGet]
    [Route("name")]
    public IActionResult FindByName([FromQuery] string name)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            EmployeeModel result = _service.FindByName(name);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        return StatusCode((int) HttpStatusCode.BadRequest);
    }

    [HttpGet]
    [Route("id")]
    public IActionResult FindById([FromQuery] Guid id)
    {
        if (id != Guid.Empty)
        {
            EmployeeModel result = _service.FindById(id);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        return StatusCode((int) HttpStatusCode.BadRequest);
    }

    [HttpGet]
    [Route("all")]
    public IActionResult GetAll()
    {
        return Ok(_service.GetAll());
    }

    [HttpDelete]
    public void Delete([FromQuery] Guid id)
    {
        _service.Delete(id);
    }

    [HttpPut]
    public EmployeeModel Update([FromQuery] EmployeeModel entity)
    {
        return _service.Update(entity);
    }
}