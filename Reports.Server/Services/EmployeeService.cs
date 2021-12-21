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
        await _context.Employees.AddAsync(employee);
        await _context.SaveChangesAsync();
        return employee;
    }

    public Employee FindByName(string name)
    {
        var employeeFromDb = _context.Employees.FirstOrDefault(i => i.Name == name);
        return employeeFromDb ?? null;
    }

    public Employee FindById(Guid id)
    {
        var employeeFromDb = _context.Employees.FirstOrDefault(i => i.Id == id);
        return employeeFromDb ?? null;
    }

    public void Delete(Guid id)
    {
        var employeeFromDb = FindById(id);
        _context.Employees.Remove(employeeFromDb);
        _context.SaveChangesAsync();
    }

    public Employee Update(Employee entity)
    {
        var employeeFromDb = FindById(entity.Id);
        employeeFromDb.Name = entity.Name;
        _context.SaveChangesAsync();
        return employeeFromDb;
    }
}