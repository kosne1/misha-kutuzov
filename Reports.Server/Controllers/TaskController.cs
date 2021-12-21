using System.Net;
using Microsoft.AspNetCore.Mvc;
using Reports.DAL.Models.TodoItems;
using Reports.Server.Services;

namespace Reports.Server.Controllers;

[ApiController]
[Route("/tasks")]
public class TaskController : ControllerBase
{
    private readonly ITaskService _service;

    public TaskController(ITaskService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<TaskModel> Create([FromQuery] string name)
    {
        return await _service.Create(name);
    }

    [HttpGet]
    [Route("id")]
    public IActionResult FindById([FromQuery] Guid id)
    {
        if (id != Guid.Empty)
        {
            TaskModel result = _service.FindById(id);
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
    public TaskModel Update([FromQuery] TaskModel entity)
    {
        return _service.Update(entity);
    }
}