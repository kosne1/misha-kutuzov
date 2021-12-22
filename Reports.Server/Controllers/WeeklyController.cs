using Microsoft.AspNetCore.Mvc;
using Reports.DAL.Models;
using Reports.Server.Services;

namespace Reports.Server.Controllers;

[ApiController]
[Route("/weekly")]
public class WeeklyController : ControllerBase
{
    private readonly IWeeklyService _service;

    public WeeklyController(IWeeklyService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<Weekly> Create([FromQuery] string description)
    {
        return await _service.Create(description);
    }

    [HttpGet]
    [Route("all")]
    public IActionResult GetAll()
    {
        return Ok(_service.GetAll());
    }

    [HttpPut]
    [Route("task")]
    public Weekly AddTask([FromQuery] Guid weeklyId, [FromQuery] Guid taskId)
    {
        return _service.AddTask(weeklyId, taskId);
    }

    [HttpPut]
    [Route("description")]
    public Weekly UpdateDescription([FromQuery] Guid id, [FromQuery] string description)
    {
        return _service.UpdateDescription(id, description);
    }
    
    [HttpPut]
    [Route("condition")]
    public Weekly UpdateCondition([FromQuery] Guid id, [FromQuery] bool condition)
    {
        return _service.UpdateCondition(id, condition);
    }
}