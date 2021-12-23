using Microsoft.AspNetCore.Mvc;
using Reports.Server.Services;

namespace Reports.Server.Controllers;

[ApiController]
[Route("/teamleaders")]
public class TeamLeadController : SupervisorController
{
    public TeamLeadController(SupervisorService service) : base(service)
    {
    }
}