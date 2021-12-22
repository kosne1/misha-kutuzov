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
}