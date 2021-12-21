using Reports.DAL.Entities;
using Reports.Server.Database;

namespace Reports.Server.Services;

public class EmployeeService : IEmployeeService
{
    private readonly ReportsDatabaseContext _context;

    public EmployeeService(ReportsDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Employee> Create(string name)
    {
        var employee = new Employee(Guid.NewGuid(), name);
        var employeeFromDb = await _context.Employees.AddAsync(employee);
        await _context.SaveChangesAsync();
        return employee;
    }

    public Employee FindByName(string name)
    {
        if (name.Equals("Aboba", StringComparison.InvariantCultureIgnoreCase))
        {
            return new Employee(Guid.NewGuid(), name);
        }

        return null;
    }

    public Employee FindById(Guid id)
    {
        Guid fakeGuid = Guid.Parse("ac8ac3ce-f738-4cd6-b131-1aa0e16eaadc");
        if (id == fakeGuid)
        {
            return new Employee(fakeGuid, "Abobus");
        }

        return null;
    }

    public void Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public Employee Update(Employee entity)
    {
        throw new NotImplementedException();
    }
}