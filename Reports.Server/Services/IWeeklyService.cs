using Reports.DAL.Models;

namespace Reports.Server.Services;

public interface IWeeklyService
{
    Task<Weekly> Create(string description);
}