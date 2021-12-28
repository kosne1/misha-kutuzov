using Reports.DAL.Models;
using Reports.DAL.Models.Employees;

namespace Reports.Server.Services;

public interface IEmployeeService
{
    IReadOnlyCollection<EmployeeModel> GetAll();
    Task<EmployeeModel> Create(string name);
    EmployeeModel FindByName(string name);
    EmployeeModel FindById(Guid id);
    void Delete(Guid id);
    EmployeeModel Update(EmployeeModel entity);
}