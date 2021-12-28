using Reports.DAL.Models.Employees;
using Reports.DAL.Models.Tasks;
using Reports.Server.Database;

namespace Reports.Server.Services;

public class SupervisorService : EmployeeService
{
    public SupervisorService(ReportsDatabaseContext context) : base(context)
    {
    }

    public async Task<SupervisorModel> CreateSupervisor(string name)
    {
        var supervisor = new SupervisorModel(Guid.NewGuid(), name);
        await _context.Employees.AddAsync(supervisor);
        await _context.SaveChangesAsync();
        return supervisor;
    }

    public IReadOnlyCollection<TaskModel> GetSubordinatesTasks(Guid id)
    {
        var supervisorFromDb = _context.Supervisors.FirstOrDefault(i => i.Id == id);
        var tasks = new List<TaskModel>();
        foreach (var subordinate in supervisorFromDb.Subordinates)
        {
            tasks.AddRange(subordinate.Tasks);
        }

        return tasks;
    }

    public EmployeeModel AddSubordinate(Guid supervisorId, Guid employeeId)
    {
        var supervisorFromDb = _context.Supervisors.FirstOrDefault(i => i.Id == supervisorId);
        var employeeFromDb = FindById(employeeId);
        supervisorFromDb.Subordinates.Add(employeeFromDb);
        _context.SaveChangesAsync();
        return supervisorFromDb;
    }
}