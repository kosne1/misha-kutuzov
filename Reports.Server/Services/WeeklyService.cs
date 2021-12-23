using Reports.DAL.Models;
using Reports.Server.Database;

namespace Reports.Server.Services;

public class WeeklyService : IWeeklyService
{
    private readonly ReportsDatabaseContext _context;

    public WeeklyService(ReportsDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Weekly> Create(string description)
    {
        var weekly = new Weekly(Guid.NewGuid(), description);
        await _context.Weeklies.AddAsync(weekly);
        await _context.SaveChangesAsync();
        return weekly;
    }

    public IReadOnlyCollection<Weekly> GetAll()
    {
        return _context.Weeklies.ToList();
    }

    public IReadOnlyCollection<Weekly> GetSubordinatesWeeklies(Guid supervisorId)
    {
        var supervisorFromDb = _context.Supervisors.FirstOrDefault(i => i.Id == supervisorId);

        return supervisorFromDb.Subordinates.Select(subordinate => subordinate.Weekly).ToList();
    }

    public Weekly AddTask(Guid weeklyId, Guid taskId)
    {
        var weeklyFromDb = _context.Weeklies.FirstOrDefault(i => i.Id == weeklyId);
        var taskFromDb = _context.Tasks.FirstOrDefault(i => i.Id == taskId);

        weeklyFromDb.Tasks.Add(taskFromDb);
        _context.SaveChangesAsync();
        return weeklyFromDb;
    }

    public Weekly UpdateDescription(Guid id, string description)
    {
        var weeklyFromDb = _context.Weeklies.FirstOrDefault(i => i.Id == id);
        weeklyFromDb.Description = description;
        _context.SaveChangesAsync();
        return weeklyFromDb;
    }

    public Weekly UpdateCondition(Guid id, bool condition)
    {
        var weeklyFromDb = _context.Weeklies.FirstOrDefault(i => i.Id == id);
        weeklyFromDb.Condition = condition;
        _context.SaveChangesAsync();
        return weeklyFromDb;
    }
}