using Reports.DAL.Models;
using Reports.DAL.Models.Employees;
using Reports.Server.Database;

namespace Reports.Server.Services;

public class EmployeeService : IEmployeeService
{
    protected readonly ReportsDatabaseContext _context;

    public EmployeeService(ReportsDatabaseContext context)
    {
        _context = context;
    }

    public IReadOnlyCollection<EmployeeModel> GetAll()
    {
        return _context.Employees.ToList();
    }

    public async Task<EmployeeModel> Create(string name)
    {
        var employee = new EmployeeModel(Guid.NewGuid(), name);
        await _context.Employees.AddAsync(employee);
        await _context.SaveChangesAsync();
        return employee;
    }

    public EmployeeModel FindByName(string name)
    {
        var employeeFromDb = _context.Employees.FirstOrDefault(i => i.Name == name);
        return employeeFromDb;
    }

    public EmployeeModel FindById(Guid id)
    {
        var employeeFromDb = _context.Employees.FirstOrDefault(i => i.Id == id);
        return employeeFromDb;
    }

    public void Delete(Guid id)
    {
        var employeeFromDb = FindById(id);
        _context.Employees.Remove(employeeFromDb);
        _context.SaveChangesAsync();
    }

    public EmployeeModel Update(EmployeeModel entity)
    {
        var employeeFromDb = FindById(entity.Id);
        employeeFromDb.Name = entity.Name;
        _context.SaveChangesAsync();
        return employeeFromDb;
    }
}